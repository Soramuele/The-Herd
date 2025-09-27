using System.Collections.Generic;

using Core.Shared.StateMachine;

using UnityEngine;

namespace Gameplay.Player
{
    /// <summary>
    /// Player state machine.
    /// </summary>
    public class PlayerStateManager : CharacterStateManager<PlayerState>
    {
        /// <summary>
        /// Player input.
        /// </summary>
        public PlayerInput Input { get; private set; }
        /// <summary>
        /// Player rotation class.
        /// </summary>
        public PlayerRotation Rotation { get; private set; }


        /// <summary>
        /// Initialization method.
        /// </summary>
        /// <param name="input">Player input class.</param>
        /// <param name="movement">Player movement class.</param>
        /// <param name="playerRotation">Player rotation class.</param>
        public void Initialize(PlayerInput input, PlayerMovement movement, PlayerRotation playerRotation)
        {
            Input = input;
            _movementController = movement;
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