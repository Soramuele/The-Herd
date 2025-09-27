using Core.Events;

using UnityEngine;

namespace Gameplay.Dog
{
    public class DogFollowPlayer : DogState
    {
        private readonly Transform _player;
        private readonly float _distanceToStopFollow;

        private readonly DogMovementController _dogMovement;


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


        public void OnTargetChanged()
        {
            _manager.SetState<DogMove>();
        }
    }
}
