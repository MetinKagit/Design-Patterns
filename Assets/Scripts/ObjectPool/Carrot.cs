using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Carrot : MonoBehaviour
{
    Rigidbody _rb;
    TrailRenderer _trail;
    private CarrotPool _poolRef;

    bool isActive = false;

    public void Init(CarrotPool pool)
    {
        _poolRef = pool;
        _rb = GetComponent<Rigidbody>();
        _trail = GetComponent<TrailRenderer>();

        DeactivateObject();
    }

    public void OnGet(Vector3 spawnPos, Quaternion spawnRot)
    {
        transform.SetPositionAndRotation(spawnPos, spawnRot);
        if (_rb != null)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        if (_trail != null)
        {
            _trail.Clear();
            _trail.emitting = true;
        }

        ActivateObject();
    }
   

   public void Launch(Vector3 inputForce)
{
    if (_rb == null) return;

    float mag = inputForce.magnitude;          
    Vector3 force = Vector3.right * mag;       
    _rb.AddForce(force, ForceMode.VelocityChange);
}

     public void OnRelease()
    {
        if (_trail != null)
        {
            _trail.emitting = false;
        }

        DeactivateObject();
    }

    public void ReleaseSelf()
    {
        if (_poolRef != null)
        {
            _poolRef.Release(this);
        }
        else
        {
            DeactivateObject();
        }
    }

      private void OnBecameInvisible()
    {
        if (isActive)
        {
            ReleaseSelf();
        }
    }
   
     private void ActivateObject()
    {
        isActive = true;
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    private void DeactivateObject()
    {
        isActive = false;
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
