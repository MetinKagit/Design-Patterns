using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          
    public Vector3 offset = new Vector3(0f, 5f, -7f);
    public float smoothTime = 0.15f;  

    Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);

        transform.rotation = Quaternion.Euler(33f, 25f, 0f);
       
    }
}
