using UnityEngine;

namespace Game.AI
{
    public sealed class ChaseState : EnemyState
    {
        public static readonly ChaseState Instance = new ChaseState();
        private ChaseState() { }
        public override void UpdateState(EnemyController ctx)
        {

        }

        public override Vector3Int DecideDir(EnemyController ctx)
        {
            Vector3Int best = Vector3Int.zero;
            int bestDelta = int.MaxValue;

            int currentDist = EnemyController.Manhattan(ctx.CurrentCell, ctx.PlayerCell);

            foreach (var d in EnemyController.DIRS)
            {
                var n = ctx.CurrentCell + d;
                if (!ctx.CanMoveTo(n)) continue;

                int nxt = EnemyController.Manhattan(n, ctx.PlayerCell);
                int delta = nxt - currentDist;


                if (best == Vector3Int.zero || delta < bestDelta || (delta == bestDelta && d != -ctx.LastDir))
                {
                    bestDelta = delta;
                    best = d;
                }
            }
            return best == Vector3Int.zero ? WanderState.Instance.DecideDir(ctx) : best;
        }
    }
}
