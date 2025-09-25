using Core.Shared.StateMachine;
using UnityEngine;

namespace Gameplay.Player
{
    /// <summary>
    /// Basic player state class.
    /// </summary>
    public abstract class PlayerState : IState
    {
        protected PlayerStateManager _stateMachine;


        public PlayerState(PlayerStateManager stateMachine)
        {
            _stateMachine = stateMachine;
        }


        public abstract void OnStart();
        public abstract void OnStop();
        public abstract void OnUpdate();

    }
}