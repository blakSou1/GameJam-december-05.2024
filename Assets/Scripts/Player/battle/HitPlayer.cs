using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HitPlayer : HealthCharcters
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 5f; 
    [SerializeField] private int attackDamage = 10; 
    [SerializeField] private LayerMask enemyLayer; 
    [SerializeField] private float attackCooldown = 1f;

    [Header("rеакция на получение урона")]
    [SerializeField] private Material damageMaterial;
    private Material originalMaterial;
    [SerializeField] private GameObject DefoltMaterialObject;

    private float damageDuration = 0.2f;
    private SkinnedMeshRenderer rend;

    [Header("UI Settings")]
    [SerializeField] private Slider sliderHealth;
    
    private Camera mainCamera;
    private bool canAttack = true;

    [SerializeField] private AudioSource audioSrcDamage;

    private void Start()
    {
        rend = DefoltMaterialObject.GetComponent<SkinnedMeshRenderer>();
        originalMaterial = rend.material;

        mainCamera = Camera.main;
        DamageReaction += HealBarUpdate;
    }

    private void HealBarUpdate()
    {
        audioSrcDamage.Play();
        StartCoroutine(nameof(DamageEffect));
        sliderHealth.value = _maxHealth;//реакция на полученый урон
    }
    private IEnumerator DamageEffect()
    {
        Debug.Log("material");
        rend.materials = new Material[]{ damageMaterial }; // Устанавливаем красный материал
        yield return new WaitForSeconds(damageDuration); // Ждем 0.2 секунды
        rend.materials = new Material[] { originalMaterial }; // Возвращаем исходный материал
    }

    private void OnDisable() => DamageReaction -= HealBarUpdate;

    private void Update()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
            StartCoroutine(AttackCooldownCoroutine()); 
        }
    }

    private void PerformAttack()
    {
        Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0; 

        Vector2 attackDirection = (cursorPosition - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackRange, enemyLayer);

        Debug.DrawRay(transform.position, attackDirection * attackRange, Color.red, 0.5f);

        if (hit.collider != null)
        {
            EnemyMove enemyHealthControll = hit.collider.GetComponent<EnemyMove>();
            if (enemyHealthControll)
                DealDamage(enemyHealthControll, attackDamage);
        }
        else
            Debug.Log("Врагов на линии атаки нет!");
    }
    private IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false; 
        yield return new WaitForSeconds(attackCooldown); 
        canAttack = true; 
    }

    private void OnDrawGizmosSelected()
    {
        if (mainCamera != null)
        {
            Vector3 cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0;
            Vector2 attackDirection = (cursorPosition - transform.position).normalized;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, attackDirection * attackRange);
        }
    }
}
