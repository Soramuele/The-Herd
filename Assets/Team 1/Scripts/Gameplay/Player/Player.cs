using UnityEngine;

namespace Gameplay.Player 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _dogFollowPoint;


        /// <summary>
        /// Transform for dog AI to follow.
        /// </summary>
        public Transform DogFollowPoint => _dogFollowPoint;



        /// <summary>
        /// Initialization method.
        /// </summary>
        public void Initialize()
        {

        }

    }
}