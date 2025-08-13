using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CarrotB : MonoBehaviour
{
    [SerializeField] private CarrotFlyweightSO flyweight;
    Renderer _renderer;
    Rigidbody _rb;
    
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();

        if (flyweight == null || flyweight.sharedMaterial == null)
        {
            Debug.LogError("CarrotB: Flyweight SO veya sharedMaterial atanmamış.", this);
            enabled = false; return;
        }

        // shared material using
        _renderer.sharedMaterial = flyweight.sharedMaterial;

        // İstersen paylaşılan mesh
        var mf = GetComponent<MeshFilter>();
        if (mf && flyweight.sharedMesh) mf.sharedMesh = flyweight.sharedMesh;

        // Set Rigidbody properties from flyweight
        _rb.mass = flyweight.mass;
        _rb.linearDamping = flyweight.drag;
        _rb.angularDamping = flyweight.angularDrag;
    }
    public void PrepareForSpawn(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void OnDespawn()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}