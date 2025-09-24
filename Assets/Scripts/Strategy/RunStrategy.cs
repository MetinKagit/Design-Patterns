using UnityEngine;

[System.Serializable]
public class RunStrategy : IMovementStrategy
{
    [SerializeField] private float speed = 27f;

    public RunStrategy(float speed = 27f)
    {
        this.speed = Mathf.Max(0f, speed);
    }

    public Vector3 UpdatePosition(Vector3 currentPos, Vector3 targetPos, float deltaTime)
    {
        return Vector3.MoveTowards(currentPos, targetPos, speed * deltaTime);
    }
}
