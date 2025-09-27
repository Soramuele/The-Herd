﻿using Core.Events;

using UnityEngine;

namespace Gameplay.Dog
{
    /// <summary>
    /// Dog moves to the target of command.
    /// </summary>
    public class DogMove : DogState
    {
        private readonly DogMovementController _dogMovement;


        public DogMove(DogStateManager manager) : base(manager)
        {
            _dogMovement = _manager.MovementController as DogMovementController;
        }


        public override void OnStart()
        {
            UpdateTarget();

            _manager.CurrentTarget.OnValueChanged += UpdateTarget;

            EventManager.AddListener<DogFollowCommandEvent>(OnDogFollowCommand);
        }

        public override void OnStop()
        {
            _manager.CurrentTarget.OnValueChanged -= UpdateTarget;

            EventManager.RemoveListener<DogFollowCommandEvent>(OnDogFollowCommand);
        }

        public override void OnUpdate()
        {
            if (!_dogMovement.IsMoving)
            {
                _manager.SetState<DogIdle>();
            }
        }


        private void UpdateTarget()
        {
            _dogMovement.MoveTo(_manager.CurrentTarget.Value);
        }


        private void OnDogFollowCommand(DogFollowCommandEvent evt)
        {
            _manager.SetState<DogFollowPlayer>();
        }
    }
}