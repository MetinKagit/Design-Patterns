using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float stopDistance = 0.05f;

    private Vector3 startPos;
    private IMovementStrategy strategy;
    private bool isMoving;


    void Awake()
    {
        startPos = transform.position;
        isMoving = false;
    }

    void Update()
    {
        if (!isMoving || strategy == null || !target)
            return;

        var pos = transform.position;
        var tpos = target.position;

        if ((tpos - pos).sqrMagnitude <= stopDistance * stopDistance)
        {
            transform.position = startPos;
            isMoving = false;
            return;
        }

        var newPos = strategy.UpdatePosition(pos, tpos, Time.deltaTime);
        transform.position = newPos;

        // var moveDir = newPos - pos;
        // if (moveDir.sqrMagnitude > 1e-6f)
        //     transform.forward = Vector3.Slerp(transform.forward, moveDir.normalized, 10f * Time.deltaTime);
    }

    public void BeginWalk() => BeginWith(new WalkStrategy(7f));
    public void BeginRun() => BeginWith(new RunStrategy(27f));
    public void BeginZigZag() => BeginWith(new ZigZagStrategy(7f, 0.6f, 2f));

    private void BeginWith(IMovementStrategy next)
    {
        if (!target)
            return;

        strategy = next;     
        transform.position = startPos;
        isMoving = true;
    }

}

