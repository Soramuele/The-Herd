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
        private InputAction _reloadAction;
        private InputAction _mainUsageAction;
        private InputAction _SecondaryUsageAction;
        private InputAction _slotsScrollAction;
        #endregion InputActions


        #region InputActionProps
        public Vector2 Move => _moveAction.ReadValue<Vector2>();
        public Vector2 Look => _lookAction.ReadValue<Vector2>();
        public bool Run => _runAction.IsPressed();
        public InputAction Reload => _reloadAction;
        public InputAction MainUsage => _mainUsageAction;
        public InputAction SecondaryUsage => _SecondaryUsageAction;
        public InputAction SlotsScroll => _slotsScrollAction;
        #endregion InputActionProps


        public void Initialize(InputActionAsset inputActions)
        {
            _inputActions = inputActions;

            // Get Input Actions
            _moveAction = _inputActions.FindActionMap("Player").FindAction("Move");
            _lookAction = _inputActions.FindActionMap("Player").FindAction("Look");
            _runAction = _inputActions.FindActionMap("Player").FindAction("Sprint");
            _reloadAction = _inputActions.FindActionMap("Player").FindAction("Reload");
            _mainUsageAction = _inputActions.FindActionMap("Player").FindAction("MainUsage");
            _SecondaryUsageAction = _inputActions.FindActionMap("Player").FindAction("SecondaryUsage");
            _slotsScrollAction = _inputActions.FindActionMap("Player").FindAction("SlotsScroll");

            // Enable input actions
            _moveAction.Enable();
            _lookAction.Enable();
            _runAction.Enable();
            _reloadAction.Enable();
            _mainUsageAction.Enable();
            _SecondaryUsageAction.Enable();
            _slotsScrollAction.Enable();
        }
    }
}