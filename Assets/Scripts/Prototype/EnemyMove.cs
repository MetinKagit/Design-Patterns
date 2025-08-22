using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMove : MonoBehaviour
{
    public Transform target;

    private Enemy enemy;
    private float speed;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        speed = enemy.MoveSpeed;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 to = (target.position - transform.position);
        to.y = 0f;

        float dist = to.magnitude;
        if (dist < 0.5f)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = to.normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
