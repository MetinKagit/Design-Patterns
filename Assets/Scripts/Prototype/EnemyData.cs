using UnityEngine;
using System.Collections.Generic;
using PrototypeDemo;

[CreateAssetMenu(fileName = "EnemyData", menuName = "PrototypeDemo/Enemy Data")]

public class EnemyData : ScriptableObject, IPrototype<EnemyData>
{
    [Header("Display")]
    public string displayName = "Enemy";

    [Header("Stats")]
    public int health = 100;
    public float speed = 3f;
    public Color color = Color.cyan;

    [Header("Abilities (örnek referans liste)")]
    public List<string> abilities = new List<string>();

    [System.Serializable]
    public class AIParams
    {
        public float chaseRange = 6f;
        public float attackCooldown = 1.2f;

        public AIParams Clone()
        {
            return new AIParams
            {
                chaseRange = this.chaseRange,
                attackCooldown = this.attackCooldown
            };
        }
    }
    
    public AIParams ai = new AIParams();
    public EnemyData Clone()
    {
        var copy = CreateInstance<EnemyData>();

        copy.displayName = displayName;
        copy.health = health;
        copy.speed = speed;
        copy.color = color;

        copy.abilities = new List<string>(abilities);
        copy.ai = ai != null ? ai.Clone() : null;

        return copy;
    }
}
