using UnityEngine;
using UnityEngine.Events;

namespace Game.Team10.Interactions
{
    /// <summary>
    /// Attach to any world object the player can interact with.
    /// Uses trigger events (from a ProximityDetector on the player) to show prompts
    /// and exposes UnityEvents that designers can wire up in the Inspector.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField]
        [Tooltip("Shared definition for prompt, icon, type, cooldown, etc.")]
        private InteractionDefinition _definition;

        [SerializeField]
        [Tooltip("Optional override for the prompt text.")]
        private string _promptOverride;

        [Header("Events")]
        [SerializeField]
        [Tooltip("Raised when the player successfully interacts.")]
        private UnityEvent _onInteract;

        [SerializeField]
        [Tooltip("Raised when the player enters range/hover of this object.")]
        private UnityEvent _onHoverEnter;

        [SerializeField]
        [Tooltip("Raised when the player leaves range/hover of this object.")]
        private UnityEvent _onHoverExit;

        // Runtime
        private float _nextAllowedTime;

        /// <summary>Shown to the player in prompts.</summary>
        public string PromptText => string.IsNullOrWhiteSpace(_promptOverride)
            ? (_definition != null ? _definition.PromptText : string.Empty)
            : _promptOverride;

        /// <summary>Definition reference (read-only).</summary>
        public InteractionDefinition Definition => _definition;

        /// <summary>Called by SceneBootstrap.</summary>
        public void Initialize()
        {
            _nextAllowedTime = 0f;
        }

        /// <summary>Called by PlayerInteractionController to perform the interaction.</summary>
        public bool TryInteract()
        {
            if (_definition == null)
            {
                Debug.LogWarning($"[Interactable] {name} has no definition assigned.");
                return false;
            }

            if (Time.time < _nextAllowedTime)
            {
                Debug.Log($"[Interactable] {name} on cooldown.");
                return false;
            }

            Debug.Log($"[Interactable] {name} interacted with!");
            _onInteract?.Invoke();

            if (_definition.Cooldown > 0f)
            {
                _nextAllowedTime = Time.time + _definition.Cooldown;
            }

            return true;
        }


        /// <summary>For detectors to signal hover enter/exit in a decoupled way.</summary>
        public void SignalHoverEnter() => _onHoverEnter?.Invoke();
        public void SignalHoverExit()  => _onHoverExit?.Invoke();
    }
}
