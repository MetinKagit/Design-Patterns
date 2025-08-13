using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Carrot Flyweight", fileName = "CarrotFlyweightSO")]
public class CarrotFlyweightSO : ScriptableObject
{
    [Header("Shared (Intrinsic) Properties")]
    public Material sharedMaterial;   
    public Mesh sharedMesh;           

    [Header("Physical Properties")]
    public float mass = 1f;
    public float drag = 0f;
    public float angularDrag = 0.05f;
}
