using Core.Shared.StateMachine;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player 
{
    /// <summary>
    /// Player state machine manager.
    /// </summary>
    public class PlayerStateManager : CharacterStateManager<PlayerState>
    {
        public PlayerInput Input { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerRotation Rotation { get; private set; }


        public void Initialize(PlayerInput input, PlayerMovement movement, PlayerRotation playerRotation)
        {
            Input = input;
            Movement = movement;
            Rotation = playerRotation;

            InitializeStatesMap();
            SetState<PlayerIdle>();
        }


        protected override void InitializeStatesMap()
        {
            StatesMap = new Dictionary<System.Type, PlayerState>
            {
                { typeof(PlayerIdle), new PlayerIdle(this) },
                { typeof(PlayerWalking), new PlayerWalking(this) },
            };
        }
    }
}