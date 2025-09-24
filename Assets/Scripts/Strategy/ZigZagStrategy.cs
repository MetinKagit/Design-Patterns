using UnityEngine;


[System.Serializable]
public class ZigZagStrategy : IMovementStrategy
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 7f;
    private float phase;

    public ZigZagStrategy(float speed = 3f, float amplitude = 0.5f, float frequency = 7f)
    {
        this.speed = Mathf.Max(0f, speed);
        this.amplitude = Mathf.Max(0f, amplitude);
        this.frequency = Mathf.Max(0f, frequency);
    }

    public Vector3 UpdatePosition(Vector3 currentPos, Vector3 targetPos, float deltaTime)
    {
        var next = Vector3.MoveTowards(currentPos, targetPos, speed * deltaTime);

        var dir = (targetPos - currentPos);
        if (dir.sqrMagnitude < 1e-6f) return targetPos;

        dir.Normalize();
        var right = Vector3.Cross(Vector3.up, dir).normalized;

        phase += deltaTime * frequency * Mathf.PI * 2f;
        float lateral = Mathf.Sin(phase) * amplitude;

        return next + right * lateral;
    }

}
