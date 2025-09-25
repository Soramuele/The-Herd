using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Speed")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotationSpeed;

    [Space]
    [SerializeField] private float _gravity;


    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
    public float RotationSpeed => _rotationSpeed;

    public float Gravity => _gravity;
}
