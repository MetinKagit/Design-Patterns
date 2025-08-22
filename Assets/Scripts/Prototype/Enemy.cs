using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Renderer body;

    private int health;
    private float speed;
    
    public float MoveSpeed => speed;

    public void Init(EnemyData data)
    {
        name = data.displayName;
        health = data.health;
        speed = data.speed;

        if (body == null)
            body = GetComponentInChildren<Renderer>();

        if (body != null)
        {
            var instancedMat = new Material(body.sharedMaterial);
            instancedMat.color = data.color;

            body.material = instancedMat;
        }
        else
        {
            Debug.LogWarning($"{name}: Renderer can not find!");
        }
    }
}
