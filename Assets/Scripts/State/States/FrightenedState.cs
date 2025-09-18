using UnityEngine;

namespace Game.AI
{

    public sealed class FrightenedState : EnemyState
    {
        private float untilTime;
        public FrightenedState(float endTime)
        {
            untilTime = endTime;
        }

        public override void Enter(EnemyController ctx)
        {
            ctx.SetTint(Color.cyan);
        }

        public override void Exit(EnemyController ctx)
        {
            ctx.ClearTint();
        }

        public override void UpdateState(EnemyController ctx)
        {

            if (Time.time >= untilTime)
            {
                ctx.PopState();
            }
        }
        
        public override Vector3Int DecideDir(EnemyController ctx)
        {
            Vector3Int best = Vector3Int.zero;
            int bestDelta = int.MinValue;

            int currentDist = EnemyController.Manhattan(ctx.CurrentCell, ctx.PlayerCell);

            foreach (var d in EnemyController.DIRS)
            {
                var n = ctx.CurrentCell + d;
                if (!ctx.CanMoveTo(n)) continue;

                int nxt = EnemyController.Manhattan(n, ctx.PlayerCell);
                int delta = nxt - currentDist; 

                if (best == Vector3Int.zero || delta > bestDelta || (delta == bestDelta && d != -ctx.LastDir))
                {
                    bestDelta = delta;
                    best = d;
                }
            }
            return best == Vector3Int.zero ? WanderState.Instance.DecideDir(ctx) : best;
        }
    }
}
