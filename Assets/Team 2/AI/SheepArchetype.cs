using UnityEngine;
namespace Core.AI.Sheep.Config
{
    /// <summary>
    /// Character/temperament of the sheep
    /// </summary>
    [CreateAssetMenu(fileName = "SheepArchetype", menuName = "Scriptable Objects/SheepArchetype")]
    public class SheepArchetype : ScriptableObject
    {
        [SerializeField] private float _skittishness = 0.0f;
        [SerializeField] private float _idleWanderRadius = 1.0f;
        [SerializeField] private float _followDistance = 1.8f;


        [Header("Grazing interval in seconds")]
        [SerializeField]
        private float _grazeIntervalMin = 3.0f;
        [SerializeField]
        private float _grazeIntervalMax = 5.0f;

        //Getters
        public float Skittishness => _skittishness;
        public float IdleWanderRadius => _idleWanderRadius;
        public float FollowDistance => _followDistance;

        public float GrazeIntervalMin => _grazeIntervalMin;
        public float GrazeIntervalMax => _grazeIntervalMax;
    }
}