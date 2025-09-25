using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private InputActionAsset _inputActions;

        #region InputActions
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        #endregion InputActions


        #region InputActionProps
        public Vector2 Move => _moveAction.ReadValue<Vector2>();
        public Vector2 Look => _lookAction.ReadValue<Vector2>();
        public bool Run => _runAction.IsPressed();
        #endregion InputActionProps


        public void Initialize(InputActionAsset inputActions)
        {
            _inputActions = inputActions;

            // Get Input Actions
            _moveAction = _inputActions.FindActionMap("Player").FindAction("Move");
            _lookAction = _inputActions.FindActionMap("Player").FindAction("Look");
            _runAction = _inputActions.FindActionMap("Player").FindAction("Sprint");

            // Enable input actions
            _moveAction.Enable();
            _lookAction.Enable();
            _runAction.Enable();
        }
    }
}