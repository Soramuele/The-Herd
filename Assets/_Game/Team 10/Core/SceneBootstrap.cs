using System.Collections.Generic;
using UnityEngine;

namespace Game.Team10.Core
{
    /// <summary>
    /// Calls Initialize() on listed scene objects once at scene start.
    /// Keeps initialization deterministic and central.
    /// </summary>
    public class SceneBootstrap : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("All scene objects that expose a public void Initialize() method.")]
        private List<MonoBehaviour> _initializables = new();

        private void Start()
        {
            // Single entry point that triggers Initialize() on referenced components.
            foreach (var mb in _initializables)
            {
                if (mb == null) { continue; }
                var m = mb.GetType().GetMethod(
                    "Initialize",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.NonPublic);

                m?.Invoke(mb, null);
            }
        }
    }
}
