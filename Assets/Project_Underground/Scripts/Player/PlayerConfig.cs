using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Base Information")] 
    public int MaxHp;
    public int Mass;
    public int ElectroEmission;
    public LayerMask PlayerMask;

    [Header("Movement")] 
    public Vector2 LinearForce;
    public Vector2 MaxLinearVelocity;
    public float AngularForce;
    public float MaxAngularVelocity;

    [Header("Lidar")]
    [Range(10, 360)]
    public int AmountOfPings;
    public GameObject PingPrefab;
    public float Delay;
    public float MaxDistance;
}
