using Codice.Client.Common.GameUI;
using UnityEngine;

[CreateAssetMenu(fileName = "DogConfig", menuName = "Configs/DogConfig")]
public class DogConfig : ScriptableObject
{
    [Header("Speed")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _rotationSpeed;

    [Space]
    [Header("Distance")]
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _slowDistance;
    [SerializeField] private float _maxDistance;


    public float MinSpeed => _minSpeed;
    public float MaxSpeed => _maxSpeed;
    public float BaseSpeed => _baseSpeed;
    public float RotationSpeed => _rotationSpeed;

    public float DistanceToPlayer => _distanceToPlayer;
    public float SlowDistance => _slowDistance;
    public float MaxDistance => _maxDistance;
}
