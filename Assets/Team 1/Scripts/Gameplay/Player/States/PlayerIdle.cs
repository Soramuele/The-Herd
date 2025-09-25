using UnityEngine;

namespace Gameplay.Player 
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerStateManager stateMachine) : base(stateMachine)
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
            if (_stateMachine.Input.Move.magnitude > 0)
                _stateMachine.SetState<PlayerWalking>();

            _stateMachine.Movement.ApplyGravity();
            _stateMachine.Rotation.Rotate(_stateMachine.Input.Look, _stateMachine.Input.Move);
        }
    }
}