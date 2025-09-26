using Gameplay.ToolsSystem;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _dogFollowPoint;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private InputActionAsset _inputActions;


        /// <summary>
        /// Transform for dog AI to follow.
        /// </summary>
        public Transform DogFollowPoint => _dogFollowPoint;


        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Initialization method.
        /// </summary>
        public void Initialize()
        {
            PlayerMovement movementController = GetComponent<PlayerMovement>();
            PlayerRotation rotationController = GetComponent<PlayerRotation>();
            PlayerStateManager stateManager = GetComponent<PlayerStateManager>();
            PlayerInput playerInput = GetComponent<PlayerInput>();
            ToolSlotsController slotsController = GetComponent<ToolSlotsController>();

            playerInput.Initialize(_inputActions);

            slotsController.Initialize(playerInput, 3);

            CharacterController characterController = GetComponent<CharacterController>();
            movementController.Initialize(characterController, _config);
            rotationController.Initialize(_config.RotationSpeed);

            stateManager.Initialize(playerInput, movementController, rotationController);
        }
    }
}