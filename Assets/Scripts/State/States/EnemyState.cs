using UnityEngine;
namespace Game.AI
{
    public abstract class EnemyState
    {
        public virtual void Enter(EnemyController ctx) { }
        public virtual void Exit(EnemyController ctx) { }
        public virtual void UpdateState(EnemyController ctx) { }
        public abstract Vector3Int DecideDir(EnemyController ctx);
        protected bool IsReverse(Vector3Int a, Vector3Int b)
            => (a + b) == Vector3Int.zero && a != Vector3Int.zero;
    }
}

