using UnityEngine;
using Core.Shared.StateMachine;

namespace Core.AI.Sheep
{
    /// <summary>
    /// Following player if outside of the square
    /// </summary>
    public sealed class SheepFollowState : IState
    {
        private readonly SheepStateManager _stateManager;

        public SheepFollowState(SheepStateManager context)
        {
            _stateManager = context;
        }

        public void OnStart()
        {
            //for animations, sounds and so on
        }

        public void OnUpdate()
        {
            if (_stateManager == null) { return; };
            Vector3 target = _stateManager.GetTargetNearPlayer();
            _stateManager.SetDestinationWithHerding(target);
        }

        public void OnStop() { }
    }


    /// <summary>
    /// Grazing in the square
    /// </summary>
    public sealed class SheepGrazeState : IState
    {
        private readonly SheepStateManager _stateManager;
        private Vector3 _currentTarget;
        private float _nextGrazeAt;
        private const float REACH_THRESHOLD = 0.35f;

        public SheepGrazeState(SheepStateManager context)
        {
            _stateManager = context;
        }

        public void OnStart()
        {
            ScheduleNextGraze();
            if(_stateManager.Agent !=  null)
            {
                _stateManager.Agent.isStopped = false;
            }
        }

        public void OnUpdate()
        {
            if (_stateManager == null) { return; }

            if(Time.time < _nextGrazeAt)
            {
                if (HasArrived())
                {
                    _stateManager.Agent.isStopped = true;
                }
                return;
            }

            _stateManager.Agent.isStopped = false;
            _currentTarget = _stateManager.GetGrazeTarget();
            _stateManager.SetDestinationWithHerding(_currentTarget);

            ScheduleNextGraze();
        }

        public void OnStop()
        {
            if(_stateManager?.Agent != null)
            {
                _stateManager.Agent.isStopped = false;
            }
        }

        private void ScheduleNextGraze()
        {
            float min = Mathf.Max(0.1f, _stateManager.Archetype?.GrazeIntervalMin ?? 3f);
            float max = Mathf.Max(min, _stateManager.Archetype?.GrazeIntervalMax ?? 5f);
            _nextGrazeAt = Time.time + Random.Range(min, max);
        }

        private bool HasArrived()
        {
            var agent = _stateManager.Agent;
            if(agent.pathPending) return false;
            return agent.remainingDistance <= REACH_THRESHOLD;
        }
    }
}
