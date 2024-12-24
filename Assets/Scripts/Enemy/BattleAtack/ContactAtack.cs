using UnityEngine;

public class ContactAtack : HealthCharcters
{
    [Header("Enemy Attack Settings")]
    [SerializeField] float coolDownAttack = 0.2f;
    [SerializeField] private int _enemyDamage = 10;

    [SerializeField] private bool criticalDamage = false;
    [SerializeField] private Vector3 positionSave;

    float colTime = 0f;
    private void OnTriggerEnter(Collider collision)
    {
        if (Time.time - colTime >= coolDownAttack && collision.CompareTag("Player"))
        {
            colTime = Time.time;
            Attack(collision);
        }
    }
    private void Attack(Collider collision)
    {
        HitPlayer enemyHealthControll = collision.gameObject.GetComponent<HitPlayer>();

        if (enemyHealthControll)
        {
            if (criticalDamage)
                collision.transform.position = positionSave;

            DealDamage(enemyHealthControll, _enemyDamage);
        }
    }
}
