using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Dog
{
    public class Dog : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private DogConfig _config;


        private DogMovementController _movementController;


        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _movementController = GetComponent<DogMovementController>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            _movementController.Initialize(agent, _config);


            DogStateManager stateManager = GetComponent<DogStateManager>();
            stateManager.Initialize(_movementController, _playerTransform, _config);
        }

        void Start()
        {
            Initialize();
        }
    }

}