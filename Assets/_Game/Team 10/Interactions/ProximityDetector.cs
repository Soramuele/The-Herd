using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Team10.Interactions
{
    /// <summary>
    /// Place this on a trigger collider (usually a child of the Player).
    /// Tracks Interactables inside range and raises a prompt-changed event
    /// so the UI can update without using Update().
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ProximityDetector : MonoBehaviour
    {
        [System.Serializable]
        public class PromptChangedEvent : UnityEvent<string> { }

        [Header("UI Events")]
        [SerializeField]
        [Tooltip("Invoked when the 'best' prompt changes (closest interactable in range).")]
        private PromptChangedEvent _onPromptChanged = new();

        // Runtime
        private readonly HashSet<Interactable> _inRange = new();
        private Interactable _currentBest;

        public void Initialize()
        {
            // Ensure trigger collider
            var col = GetComponent<Collider>();
            if (col != null) { col.isTrigger = true; }
            RecomputeAndNotify();
        }

        /// <summary>Copy current set into provided list.</summary>
        public void GetCurrent(List<Interactable> results)
        {
            results.AddRange(_inRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                _inRange.Add(interactable);
                interactable.SignalHoverEnter();
                Debug.Log($"[Detector] Entered range of: {interactable.name}");
                RecomputeAndNotify();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                _inRange.Remove(interactable);
                interactable.SignalHoverExit();
                Debug.Log($"[Detector] Exited range of: {interactable.name}");
                RecomputeAndNotify();
            }
        }

        private void RecomputeAndNotify()
        {
            var next = GetBest();
            if (_currentBest == next) { return; }
            _currentBest = next;

            var text = _currentBest != null ? _currentBest.PromptText : string.Empty;
            _onPromptChanged?.Invoke(text);
        }

        private Interactable GetBest()
        {
            if (_inRange.Count == 0) { return null; }

            var self = transform.position;
            return _inRange
                .OrderBy(i => (i.transform.position - self).sqrMagnitude)
                .FirstOrDefault();
        }
    }
}
