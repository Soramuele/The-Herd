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
        
        private float _minSpeed;
        private float _maxSpeed;
        private float _baseSpeed;

        private float _slowDistance;
        private float _maxDistance;


        public bool IsMoving => (_agent.hasPath || _agent.pathPending); 


        /// <summary>
        /// Initialization method.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="playerFollowPoint"></param>
        public void Initialize(NavMeshAgent agent, DogConfig config)
        {
            _agent = agent;

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


        public void CalculateSpeedToPlayer()
        {
            float t = Mathf.InverseLerp(_slowDistance, _maxDistance, _agent.remainingDistance);
            _agent.speed = Mathf.Lerp(_minSpeed, _maxSpeed, t);
        }
    }
}