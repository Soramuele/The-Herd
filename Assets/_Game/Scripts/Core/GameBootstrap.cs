using UnityEngine;

namespace Core
{
  ///<summary>
  /// Entry point for initialization
  ///</summary>
  public class GameBootstrap : MonoBehaviour
  {
    [SerializeField] private Dog _dog;

    ///<summary>
    /// Initialize everything inside this Start() function
    ///</summary>
    void Start()
    {
      _dog.Initialize();
    }
  }
}
