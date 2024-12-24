using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMove : HealthCharcters
{
    [Header("Enemy Settings")]
    [SerializeField] private int enemySpeed;
    private bool movingRight = true;

    public bool MovDos()
    {
        return movingRight;
    }
    public void MovPol(bool m)
    {
        movingRight = m;
    }

    private void Move()
    {
        if (movingRight)
            transform.Translate(Vector3.right * enemySpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }
}
