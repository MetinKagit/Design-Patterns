using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float step = 1f;
    public float rollTime = 0.2f;
    public bool IsMoving { get; private set; }
    public void Move(Vector3 delta)
    {
        if (IsMoving) return;
        StartCoroutine(Roll(delta));
    }

    private IEnumerator Roll(Vector3 delta)
    {
        IsMoving = true;
       
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        // calculate the pivot point and axis of rotation
        Vector3 axis = Vector3.Cross(Vector3.up, delta.normalized);
        Vector3 pivot = startPos + delta * 0.5f + Vector3.down * 0.5f;

        float rotated = 0f;
        float speed = 90f / rollTime;

        // turning 90° degree
        while (rotated < 90f)
        {
            float stepAngle = speed * Time.deltaTime;
            if (rotated + stepAngle > 90f)
                stepAngle = 90f - rotated;

            transform.RotateAround(pivot, axis, stepAngle);
            rotated += stepAngle;
            yield return null;
        }

        // calculate final position
        transform.position = startPos + delta;
        transform.rotation = startRot;

        IsMoving = false;
    }
}
