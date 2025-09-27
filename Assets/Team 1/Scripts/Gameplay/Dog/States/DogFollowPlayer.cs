using Core.Events;

using UnityEngine;

namespace Gameplay.Dog
{
    /// <summary>
    /// Dog is moving to the player.
    /// </summary>
    public class DogFollowPlayer : DogState
    {
        private readonly Transform _player;
        private readonly float _distanceToStopFollow;

        private readonly DogMovementController _dogMovement;


        /// <param name="playerTransform">Transform of player object to follow.</param>
        /// <param name="distanceToStopFollow">Distance between dog and player to stop move.</param>
        public DogFollowPlayer(DogStateManager manager, Transform playerTransform, float distanceToStopFollow) : base(manager)
        {
            _player = playerTransform;
            _distanceToStopFollow = distanceToStopFollow;

            _dogMovement = _manager.MovementController as DogMovementController;
        }


        public override void OnStart()
        {
            _manager.CurrentTarget.OnValueChanged += OnTargetChanged;
        }

        public override void OnStop()
        {
            _manager.CurrentTarget.OnValueChanged -= OnTargetChanged;
        }

        public override void OnUpdate()
        {
            _dogMovement.MoveTo(CalculateFollowPoint());
            _dogMovement.CalculateSpeedToPlayer();


            if (Vector3.Distance(_manager.MovementController.transform.position, _player.position) < _distanceToStopFollow)
                _manager.SetState<DogIdle>();
        }


        private Vector3 CalculateFollowPoint()
        {
            Vector3 direction = (_dogMovement.transform.position - _player.position).normalized;

            return _player.position + direction * _distanceToStopFollow;
        }


        private void OnTargetChanged()
        {
            _manager.SetState<DogMove>();
        }
    }
}