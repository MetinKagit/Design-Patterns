// PlayerGridStepper.cs
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class PlayerGridStepper : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private LayerMask wallMask;

    [SerializeField] private float tilesPerSecond = 8f;
    [SerializeField] private bool snapOnStart = true;


    private Rigidbody rb;
    private Collider col;

    private Vector3Int currentCell;
    private Vector3Int previousCell;
    private Vector3 targetPos;
    private bool isMoving;
    private float stepDistance;
    private float lockedY;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if (grid == null)
            grid = FindFirstObjectByType<Grid>();

        lockedY = transform.position.y;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    void Start()
    {
        currentCell = grid.WorldToCell(transform.position);
        if (snapOnStart)
            rb.position = WorldCenter(currentCell);
    }

    void Update()
    {
        if (isMoving)
            return;

        Vector3Int dir = ReadInputDir();

        if (dir == Vector3Int.zero)
            return;

        TryStartStep(dir);
    }

    void FixedUpdate()
    {
        if (!isMoving) return;

        float maxDelta = tilesPerSecond * stepDistance * Time.fixedDeltaTime;
        Vector3 next = Vector3.MoveTowards(rb.position, targetPos, maxDelta);
        rb.MovePosition(next);

        if ((next - targetPos).sqrMagnitude <= 0.000001f)
        {
            rb.MovePosition(targetPos);
            currentCell = grid.WorldToCell(targetPos);
            isMoving = false;
        }
    }

    private void TryStartStep(Vector3Int dir)
    {
        Vector3Int nextCell = currentCell + dir;

        Vector3 from = WorldCenter(currentCell);
        Vector3 to = WorldCenter(nextCell);

        if (Blocked(from, to))
            return;

        previousCell = currentCell;

        targetPos = to;
        stepDistance = Vector3.Distance(from, to);
        isMoving = stepDistance > Mathf.Epsilon;
    }

    private bool Blocked(Vector3 from, Vector3 to)
    {
        Vector3 dir = to - from;
        float dist = dir.magnitude;

        if (dist <= Mathf.Epsilon)
            return false;

        Vector3 halfExtents = col.bounds.extents * 0.95f;
        Vector3 castFrom = col.bounds.center;

        return Physics.BoxCast(
            castFrom,
            halfExtents,
            dir.normalized,
            out _,
            transform.rotation,
            dist,
            wallMask,
            QueryTriggerInteraction.Ignore
        );
    }

    void OnCollisionEnter(Collision other)
    {
        if ((wallMask.value & (1 << other.gameObject.layer)) == 0) return;

        Vector3 prev = WorldCenter(previousCell);
        rb.MovePosition(prev);
        currentCell = previousCell;

        isMoving = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private Vector3 WorldCenter(Vector3Int cell)
    {
        Vector3 w = grid.GetCellCenterWorld(cell);
        w.y = lockedY;
        return w;
    }

    private static Vector3Int ReadInputDir()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            return Vector3Int.right;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            return Vector3Int.left;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            return Vector3Int.up;    // +cellY (grid swizzle XZY -> world +Z)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            return Vector3Int.down;  // -cellY (grid swizzle XZY -> world -Z)

        return Vector3Int.zero;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (grid == null) return;

        var cellNow = Application.isPlaying
            ? currentCell
            : grid.WorldToCell(transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(WorldCenter(cellNow), 0.1f);

        if (Application.isPlaying && isMoving)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(targetPos, 0.1f);
        }
    }
#endif
}