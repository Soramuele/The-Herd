using System.Collections.Generic;

using Core.Events;
using Core.Shared;
using Core.Shared.StateMachine;
using UnityEngine;

namespace Gameplay.Dog
{
    public class DogStateManager : CharacterStateManager<DogState>
    {
        private Transform _playerTransform;
        private float _distanceToPlayer;


        public Observable<Vector3> CurrentTarget { get; set; } = new Observable<Vector3>();


        public void Initialize(DogMovementController movementController, Transform playerTransform, DogConfig config)
        {
            _movementController = movementController;

            _playerTransform = playerTransform;

            _distanceToPlayer = config.DistanceToPlayer;

            InitializeStatesMap();

            EventManager.AddListener<DogMoveCommandEvent>(OnDogMoveCommand);

            SetState<DogIdle>();
        }


        protected override void InitializeStatesMap()
        {
            StatesMap = new Dictionary<System.Type, DogState>
            {
                { typeof(DogIdle), new DogIdle(this, _playerTransform, _distanceToPlayer) },
                { typeof(DogFollowPlayer), new DogFollowPlayer(this, _playerTransform, _distanceToPlayer) },
                { typeof(DogMove), new DogMove(this) },
            };
        }


        private void OnDogMoveCommand(DogMoveCommandEvent evt)
        {
            CurrentTarget.Value = evt.MoveTarget;
        }
    }
}

