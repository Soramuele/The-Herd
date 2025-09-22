using Core.Shared;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Dog 
{
    public class DogMovementController : MovementController
    {
        private NavMeshAgent _agent;
        
        private Transform _playerFollowPoint;
        private Coroutine _playerFollowCoroutine;
        private bool _isFollowingPlayer = true;

        private float _minSpeed;
        private float _maxSpeed;
        private float _baseSpeed;

        private float _slowDistance;
        private float _maxDistance;


        /// <summary>
        /// Initialization method.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="playerFollowPoint"></param>
        public void Initialize(NavMeshAgent agent, Transform playerFollowPoint, DogConfig config)
        {
            _agent = agent;
            _playerFollowPoint = playerFollowPoint;

            _playerFollowCoroutine = StartCoroutine(FollowPlayer());

            _minSpeed = config.MinSpeed;
            _maxSpeed = config.MaxSpeed;
            _baseSpeed = config.BaseSpeed;

            _slowDistance = config.SlowDistance;
            _maxDistance = config.MaxDistance;

            _agent.speed = _baseSpeed;

            _agent.angularSpeed = config.RotationSpeed;
        }


        public override void MoveTo(Vector3 target)
        {
            _agent.speed = _baseSpeed;

            _agent.destination = target;
        }


        private IEnumerator FollowPlayer()
        {
            while (_isFollowingPlayer)
            {
                MoveTo(_playerFollowPoint.position);
                float t = Mathf.InverseLerp(_slowDistance, _maxDistance, _agent.remainingDistance);
                _agent.speed = Mathf.Lerp(_minSpeed, _maxSpeed, t);
                yield return null;
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}