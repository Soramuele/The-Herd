using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Core.Events;
using Core.Shared.StateMachine;
using Core.AI.Sheep.Config;
using UnityEngine.AI;
using NUnit.Framework;
using UnityEngine.Rendering;

namespace Core.AI.Sheep
{
    /// <summary>
    /// Sheep state manager
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class SheepStateManager : CharacterStateManager<IState>
    {
        private const float DEFAULT_FOLLOW_DISTANCE = 1.8f;


        [Header("Data")]
        [SerializeField]
        [Tooltip("Movement and herding config")]
        private SheepConfig _config;

        [SerializeField][Tooltip("Sheep's archetype")]
        private SheepArchetype _archetype;

        [Header("Neighbours")]
        [SerializeField]
        [Tooltip("All herd members")]
        private List<Transform> _neighbours = new List<Transform>();


        //Private
        private NavMeshAgent _agent;
        private Coroutine _tickCoroutine;
        private IState _currentState;
        private SheepFollowState _followState;
        private SheepGrazeState _grazeState;

        private Vector3 _playerCenter;
        private Vector3 _playerHalfExtents;


        /// <summary>
        /// Exposed NavMeshAgent for state machine
        /// </summary>
        public NavMeshAgent Agent => _agent;

        /// <summary>
        /// Exposed config and archetype
        /// </summary>
        public SheepConfig Config => _config;
        public SheepArchetype Archetype => _archetype;

        /// <summary>
        /// Read-only list of neighbouring sheep
        /// </summary>
        public IReadOnlyList<Transform> Neighbours => _neighbours;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            if(_config != null)
            {
                _agent.speed = _config.BaseSpeed;
            }

            _followState = new SheepFollowState(this);
            _grazeState = new SheepGrazeState(this);
        }

        private void OnEnable()
        {
            EventManager.AddListener<PlayerSquareChangedEvent>(OnPlayerSquareChanged);
            EventManager.AddListener<PlayerSquareTickEvent>(OnPlayerSquareTick);

            SwitchState(_grazeState);

            float interval = _config != null ? _config.Tick : 0.15f;
            _tickCoroutine = StartCoroutine(TickCoroutine(interval));
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<PlayerSquareChangedEvent>(OnPlayerSquareChanged);
            EventManager.RemoveListener<PlayerSquareTickEvent>(OnPlayerSquareTick);

            if(_tickCoroutine != null)
            {
                StopCoroutine(_tickCoroutine);
                _tickCoroutine = null;
            }

            _currentState?.OnStop();
        }

        protected override void InitializeStatesMap()
        {
            StatesMap = new Dictionary<Type, IState>
            {
                {typeof(SheepFollowState), _followState},
                {typeof(SheepGrazeState), _grazeState}
            };

            SetState<SheepGrazeState>();
        }

        /// <summary>
        /// Fill list with neighbours
        /// </summary>
        public void SetNeighbours(List<Transform> neighbours)
        {
            _neighbours = neighbours ?? new List<Transform>();
        }


        private void OnPlayerSquareChanged(PlayerSquareChangedEvent e)
        {
            _playerCenter = e.Center;
            _playerHalfExtents = e.HalfExtents;
        }


        private void OnPlayerSquareTick(PlayerSquareTickEvent e)
        {
            _playerCenter = e.Center;
            _playerHalfExtents = e.HalfExtents;

            //Decide on state
            bool outside = FlockingUtility.IsOutSquare(transform.position, _playerCenter, _playerHalfExtents);
            var targetState = outside ? (IState)_followState : _grazeState;

            if(!ReferenceEquals(_currentState, targetState))
            {
                SwitchState(targetState);
            }
        }


        private IEnumerator TickCoroutine(float interval)
        {
            var wait = new WaitForSeconds(interval);
            while (true)
            {
                _currentState?.OnUpdate();
                yield return wait;
            }
        }


        private void SwitchState(IState next)
        {
            _currentState?.OnStop();
            _currentState = next;
            _currentState?.OnStart();
        }


        // ======== Helpers for states ========
        
        /// <summary>
        /// Set destination with herding nudge
        /// </summary>
        public void SetDestinationWithHerding(Vector3 destination)
        {
            Vector3 desired = destination - transform.position;
            desired.y = 0f;

            float sepDist = _config?.SeparationDistance ?? 0.8f;
            float sepW = _config?.SeparationWeight ?? 1.2f;
            float alignW = _config?.AlignmentWeight ?? 0.6f;
            float clamp = _config?.SteerClamp ?? 2.5f;

            Vector3 steer = FlockingUtility.Steering(
                transform,
                _neighbours,
                sepDist,
                sepW,
                alignW,
                clamp
            );

            Vector3 final = transform.position + desired + steer;
            _agent?.SetDestination( final );
        }

        /// <summary>
        /// Makes a destination near the player
        /// </summary>
        public Vector3 GetTargetNearPlayer()
        {
            float want = Mathf.Min(0.5f, _archetype?.FollowDistance ?? DEFAULT_FOLLOW_DISTANCE);
            Vector3 dir = (_playerCenter - transform.position);
            dir.y = 0f;
            dir = dir.sqrMagnitude > 0.001f ? dir.normalized : Vector3.forward;

            Vector3 target = _playerCenter - dir * want;

            //Reduce stacking
            target += Quaternion.Euler(0f, UnityEngine.Random.Range(-35f, 35f), 0f) * (Vector3.right * (want * 0.5f));
            return target;
        }

        /// <summary>
        /// Pick a point to go to when grazing
        /// </summary>
        public Vector3 GetGrazeTarget()
        {
            float step = Mathf.Max(0.2f, _archetype?.IdleWanderRadius ?? 1.0f);
            Vector2 rand = UnityEngine.Random.insideUnitCircle * step;
            return transform.position + new Vector3(rand.x, 0f, rand.y);
        }
    }

}
