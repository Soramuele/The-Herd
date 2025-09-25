using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Team10.Interactions
{
    /// <summary>
    /// Listens for the Interact input and routes it to the closest Interactable in range.
    /// Hook this up on the Player and assign the Input Action in the Inspector.
    /// </summary>
    public class PlayerInteractionController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField]
        [Tooltip("Input System action for interacting (e.g., E / Gamepad X).")]
        private InputActionReference _interactAction;

        [Header("Detection")]
        [SerializeField]
        [Tooltip("Proximity detector that tracks interactables in range.")]
        private ProximityDetector _detector;

        // Reused buffer to avoid GC
        private readonly List<Interactable> _buf = new();

        public void Initialize()
        {
            if (_interactAction != null)
            {
                _interactAction.action.performed += OnInteractPerformed;
                _interactAction.action.Enable();
            }
        }

        private void OnDestroy()
        {
            if (_interactAction != null)
            {
                _interactAction.action.performed -= OnInteractPerformed;
            }
        }

        private void OnInteractPerformed(InputAction.CallbackContext ctx)
        {
            if (_detector == null) { return; }

            _buf.Clear();
            _detector.GetCurrent(_buf);
            if (_buf.Count == 0) { return; }

            var self = transform.position;
            var target = _buf.OrderBy(i => (i.transform.position - self).sqrMagnitude).First();
            Debug.Log($"[Interaction] Attempting to interact with: {target.name}");

            target.TryInteract();
        }
    }
}
