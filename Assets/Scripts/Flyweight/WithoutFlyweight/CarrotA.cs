using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CarrotA : MonoBehaviour
{
    MeshRenderer _mr;
    Rigidbody _rb;
    Material _uniqueMat;

    void Awake()
    {
        _mr = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();

        if (_mr.sharedMaterial == null)
        {
            Debug.LogError("CarrotA: sharedMaterial is not assigned.", this);
            enabled = false;
            return;
        }

        _uniqueMat = new Material(_mr.sharedMaterial);
        _mr.material = _uniqueMat;
    }

    public void PrepareForSpawn(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);

        // Reset Rigidbody properties
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    // Called when the carrot is returned to the pool
    public void OnDespawn()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    void OnDestroy()
    {
        // Clean up the unique material to prevent memory leaks
        if (_uniqueMat != null)
        {
            Destroy(_uniqueMat);
            _uniqueMat = null;
        }
    }
}
