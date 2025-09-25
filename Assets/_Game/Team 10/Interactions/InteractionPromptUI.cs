using TMPro;
using UnityEngine;

namespace Game.Team10.Interactions
{
    /// <summary>
    /// Minimal UI presenter for the interaction prompt.
    /// Subscribe this to the ProximityDetector's OnPromptChanged UnityEvent in the Inspector.
    /// </summary>
    public class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Label to display the current interaction prompt.")]
        private TextMeshProUGUI _label;

        /// <summary>Called by ProximityDetector via UnityEvent whenever the best prompt changes.</summary>
        public void SetPrompt(string text)
        {
            if (_label == null) { return; }
            _label.text = text ?? string.Empty;
        }

        public void Initialize() { /* reserved for later (styling, animations, etc.) */ }
    }
}
