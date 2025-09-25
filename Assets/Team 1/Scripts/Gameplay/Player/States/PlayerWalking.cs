using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlayerWalking : PlayerState
    {
        public PlayerWalking(PlayerStateManager stateMachine) : base(stateMachine)
        {
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override void OnUpdate()
        {
            if (_stateMachine.Input.Move.magnitude == 0)
                _stateMachine.SetState<PlayerIdle>();

            _stateMachine.Movement.ApplyGravity();

            Vector3 movementTarget = _stateMachine.Movement.CalculateMovementTargetFromInput(_stateMachine.Input.Move, _stateMachine.Input.Run);

            _stateMachine.Movement.MoveTo(movementTarget);
            _stateMachine.Rotation.Rotate(_stateMachine.Input.Look, _stateMachine.Input.Move);
        }
    }
}