using System.Collections.Generic;
using UnityEngine;
namespace Game.AI
{
    public sealed class WanderState : EnemyState
    {
        public static readonly WanderState Instance = new WanderState();
        private System.Random rng = new System.Random();

        private WanderState() { }

        public override void UpdateState(EnemyController ctx) { }

        public override Vector3Int DecideDir(EnemyController ctx)
        {
            var options = new List<Vector3Int>(4);

            foreach (var d in EnemyController.DIRS)
            {
                if (!ctx.CanMoveTo(ctx.CurrentCell + d))
                    continue;
                if (!IsReverse(d, ctx.LastDir))
                    options.Add(d);
            }

            if (options.Count == 0)
            {
                foreach (var d in EnemyController.DIRS)
                    if (ctx.CanMoveTo(ctx.CurrentCell + d))
                        options.Add(d);
            }

            if (options.Count == 0)
                return Vector3Int.zero;
                
            int i = rng.Next(options.Count);
            return options[i];
        }
    }

}


