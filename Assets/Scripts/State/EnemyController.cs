using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

namespace Game.AI
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap walls;
        [SerializeField] private Transform player;
        [SerializeField] private PointCollector collector;
        [SerializeField] private LayerMask obstacleMask;

        [Header("Movement")]
        [SerializeField, Min(0.1f)] private float tilesPerSecond = 6f;
        [SerializeField] private int wallsLayerZ = 0;
        [SerializeField] private bool snapOnStart = true;
        [SerializeField, Min(0)] private int chaseRangeCells = 7;
        [SerializeField, Min(0.1f)] private float frightenedDuration = 6f;

        private Rigidbody rb;
        private Collider col;

        private Vector3Int currentCell;
        private Vector3 targetPos;
        private float stepDistance;
        private bool isMoving;
        private float lockedY;
        private Vector3Int lastDir = Vector3Int.zero;

        private readonly Stack<EnemyState> stack = new();

        [Header("Visuals")]
        [SerializeField] private Renderer[] tintRenderers;
        private Color[] originalColors;
        public event Action<string> OnStateChanged;

        public string CurrentStateName
        {
            get
            {
                var s = Current;
                return s == null ? "None" : s.GetType().Name;
            }
        }
        private void NotifyStateChanged()
        {
            OnStateChanged?.Invoke(CurrentStateName);
        }

        public static readonly Vector3Int[] DIRS =
        {
            Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down
        };

        void Awake()
        {

            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();

            if (grid == null) grid = FindFirstObjectByType<Grid>();
            if (walls == null) walls = FindFirstObjectByType<Tilemap>();

            lockedY = transform.position.y;

            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            if (tintRenderers != null && tintRenderers.Length > 0)
            {
                originalColors = new Color[tintRenderers.Length];
                for (int i = 0; i < tintRenderers.Length; i++)
                {
                    if (tintRenderers[i] != null)
                        originalColors[i] = tintRenderers[i].material.color;
                }
            }

            if (grid == null)
                Debug.LogError($"{name}: Grid not found yok.");
            if (walls == null)
                Debug.LogWarning($"{name}: Walls Tilemap not founs.");
        }

        void OnEnable()
        {
            if (collector != null)
                collector.OnBigPointCollected += OnBigPoint;
            else
                Debug.LogWarning($"{name}: collector not found.");
        }
        void OnDisable()
        {
            if (collector != null)
                collector.OnBigPointCollected -= OnBigPoint;
        }

        void Start()
        {
            currentCell = grid.WorldToCell(transform.position);

            if (snapOnStart)
            {
                var snap = WorldCenter(currentCell);
                rb.position = snap;
            }

            PushState(WanderState.Instance);

            isMoving = false;
            targetPos = WorldCenter(currentCell);
            stepDistance = 0f;
        }

        void Update()
        {
            var cur = stack.Count > 0 ? stack.Peek() : null;
            cur?.UpdateState(this);

            if (!isMoving && cur != null)
            {
                Vector3Int dir = cur.DecideDir(this);
                TryStartStep(dir);
            }

            ChaseOrWander();
        }

        void FixedUpdate()
        {
            if (!isMoving)
                return;

            float maxDelta = tilesPerSecond * stepDistance * Time.fixedDeltaTime;

            Vector3 next = Vector3.MoveTowards(rb.position, targetPos, maxDelta);
            rb.MovePosition(next);

            if ((next - targetPos).sqrMagnitude <= 0.000001f)
            {

                rb.MovePosition(targetPos);
                currentCell = grid.WorldToCell(targetPos);
                isMoving = false;

                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }


        private void ChaseOrWander()
        {
            var cur = stack.Count > 0 ? stack.Peek() : null;
            if (cur == null)
                return;

            if (cur is FrightenedState)
                return;


            int dist = Manhattan(currentCell, PlayerCell);

            EnemyState desired = (dist <= chaseRangeCells) ? ChaseState.Instance : WanderState.Instance;

            if (!ReferenceEquals(cur, desired))
                ReplaceState(desired);
        }

        public Vector3Int CurrentCell => currentCell;
        public Vector3Int PlayerCell => grid.WorldToCell(player.position);
        public Vector3Int LastDir => lastDir;

        public static int Manhattan(Vector3Int a, Vector3Int b)
            => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);


        private EnemyState Current => stack.Count > 0 ? stack.Peek() : null;

        private void PushState(EnemyState s)
        {
            Current?.Exit(this);
            stack.Push(s);
            s.Enter(this);
            NotifyStateChanged();
        }

        public void PopState()
        {
            if (stack.Count == 0)
                return;
            var top = stack.Pop();
            top.Exit(this);
            Current?.Enter(this);
            NotifyStateChanged();
        }

        private void ReplaceState(EnemyState s)
        {
            PopState();
            PushState(s);
        }


        private void OnBigPoint()
        {
            PushState(new FrightenedState(Time.time + frightenedDuration));
        }


        private void TryStartStep(Vector3Int dir)
        {
            if (dir == Vector3Int.zero)
                return;

            Vector3Int nextCell = currentCell + dir;
            Vector3 from = WorldCenter(currentCell);
            Vector3 to = WorldCenter(nextCell);

            if (Blocked(from, to))
                return;

            lastDir = dir;
            targetPos = to;
            stepDistance = Vector3.Distance(from, to);
            isMoving = stepDistance > Mathf.Epsilon;
        }

        private bool Blocked(Vector3 from, Vector3 to)
        {
            Vector3 dir = to - from;
            float dist = dir.magnitude;
            if (dist <= Mathf.Epsilon) return false;

            Vector3 half = col.bounds.extents * 0.95f;
            Vector3 castFrom = col.bounds.center;


            return Physics.BoxCast(
        castFrom,
        half,
        dir.normalized,
        out _,
        transform.rotation,
        dist,
        obstacleMask,
        QueryTriggerInteraction.Ignore
            );
        }

        public bool CanMoveTo(Vector3Int cell)
        {
            Vector3 from = WorldCenter(currentCell);
            Vector3 to = WorldCenter(cell);
            return !Blocked(from, to);
        }

        private Vector3 WorldCenter(Vector3Int cell)
        {
            Vector3 w = grid.GetCellCenterWorld(cell);
            w.y = lockedY;
            return w;
        }


        public void SetTint(Color c)
        {
            if (tintRenderers == null) return;
            foreach (var r in tintRenderers)
                if (r != null) r.material.color = c;
        }

        public void ClearTint()
        {
            if (tintRenderers == null || originalColors == null) return;
            for (int i = 0; i < tintRenderers.Length && i < originalColors.Length; i++)
                if (tintRenderers[i] != null) tintRenderers[i].material.color = originalColors[i];
        }

    }



}