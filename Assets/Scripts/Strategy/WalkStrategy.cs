using UnityEngine;

[System.Serializable]
public class WalkStrategy : IMovementStrategy
{
    [SerializeField] private float speed = 7f;

    public WalkStrategy(float speed = 7f)
    {
        this.speed = Mathf.Max(0f, speed);
    }
    
     public Vector3 UpdatePosition(Vector3 currentPos, Vector3 targetPos, float deltaTime)
    {
        return Vector3.MoveTowards(currentPos, targetPos, speed * deltaTime);
    }
}
