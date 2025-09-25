using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Dog
{
    public class Dog : MonoBehaviour
    {
        [SerializeField] private Transform _playerFollowPoint;
        [SerializeField] private DogConfig _config;


        private DogMovementController _movementController;


        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _movementController = GetComponent<DogMovementController>();

            NavMeshAgent agent = GetComponent<NavMeshAgent>();

            _movementController.Initialize(agent, _playerFollowPoint, _config);
        }

        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}