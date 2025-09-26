using System.Collections.Generic;
using Core.Shared;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.ToolsSystem
{
    public class ToolSlotsController : MonoBehaviour
    {
        private List<IPlayerTool> _toolSlots = new List<IPlayerTool>();
        private int _currentToolIndex;
        private int _slotsAmount;

        private Gameplay.Player.PlayerInput _input;


        public void Initialize(Gameplay.Player.PlayerInput input, int slotsAmount)
        {
            _input = input;
            _slotsAmount = slotsAmount;

            for (int i = 0; i < _slotsAmount; i++)
                _toolSlots.Add(null);

            _currentToolIndex = 0;


            _input.MainUsage.started += OnCurrentToolMainUseStarted;
            _input.MainUsage.canceled += OnCurrentToolMainUseFinished;

            _input.SecondaryUsage.started += OnCurrentToolSecondaryUseStarted;
            _input.SecondaryUsage.canceled += OnCurrentToolSecondaryUseFinished;

            _input.SlotsScroll.started += UpdateCurrentSlot;
        }


        private void UpdateCurrentSlot(InputAction.CallbackContext obj)
        {
            int inputValue = Mathf.RoundToInt(obj.action.ReadValue<Vector2>().y);

            _currentToolIndex += inputValue;
            _currentToolIndex = Mathf.Clamp(_currentToolIndex, 0, _slotsAmount);
        }


        private void OnCurrentToolMainUseStarted(InputAction.CallbackContext obj)
        {
            if (_toolSlots[_currentToolIndex] != null)
                _toolSlots[_currentToolIndex].MainUsageStarted();
        }

        private void OnCurrentToolMainUseFinished(InputAction.CallbackContext obj)
        {
            if (_toolSlots[_currentToolIndex] != null)
                _toolSlots[_currentToolIndex].MainUsageFinished();
        }


        private void OnCurrentToolSecondaryUseStarted(InputAction.CallbackContext obj)
        {
            if (_toolSlots[_currentToolIndex] != null)
                _toolSlots[_currentToolIndex].SecondaryUsageStarted();
        }

        private void OnCurrentToolSecondaryUseFinished(InputAction.CallbackContext obj)
        {
            if (_toolSlots[_currentToolIndex] != null)
                _toolSlots[_currentToolIndex].SecondaryUsageFinished();
        }


        /// <summary>
        /// Sets new tool to specific quick slot.
        /// </summary>
        /// <param name="toolToSet">Tool to add.</param>
        /// <param name="index">Index of slot to add.</param>
        public void SetNewToolToSlotByIndex(IPlayerTool toolToSet, int index)
        {
            _toolSlots[index] = toolToSet;
        }


        private void Update()
        {
            Debug.Log(_currentToolIndex + 1);
        }


        private void OnDestroy()
        {
            _input.MainUsage.started -= OnCurrentToolMainUseStarted;
            _input.MainUsage.canceled -= OnCurrentToolMainUseFinished;

            _input.SecondaryUsage.started -= OnCurrentToolSecondaryUseStarted;
            _input.SecondaryUsage.canceled -= OnCurrentToolSecondaryUseFinished;

            _input.SlotsScroll.started -= UpdateCurrentSlot;
        }
    }
}