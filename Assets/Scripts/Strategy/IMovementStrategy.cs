using UnityEngine;

public interface IMovementStrategy
{
    Vector3 UpdatePosition(Vector3 currentPosition, Vector3 targetPosition, float deltaTime);
}
