using UnityEngine;

namespace Game.Team10.Interactions
{
    /// <summary>
    /// Scriptable definition to describe how an interaction appears and behaves.
    /// Reuse across many interactables (e.g., "Open", "Talk", "Search").
    /// </summary>
    [CreateAssetMenu(fileName = "InteractionDefinition", menuName = "Game/Team10/Interaction Definition")]
    public class InteractionDefinition : ScriptableObject
    {
        [Header("UI")]
        [Tooltip("Text shown in the prompt (e.g., 'Open', 'Talk').")]
        public string PromptText = "Interact";

        [Tooltip("Optional icon displayed beside the keybind.")]
        public Sprite KeyIcon;

        [Header("Behavior")]
        [Tooltip("Category for analytics/logic (optional).")]
        public InteractionType Type = InteractionType.Use;

        [Tooltip("If true, the interaction auto-fires upon player entering the trigger.")]
        public bool AutoOnEnter;

        [Tooltip("Cooldown in seconds between interactions (0 = no cooldown).")]
        [Min(0f)] public float Cooldown = 0f;
    }

    public enum InteractionType
    {
        Use,
        Inspect,
        Loot,
        Trigger,
        Talk,
        Custom
    }
}
