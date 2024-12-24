using UnityEngine;

public abstract class ExplosiveClass : MonoBehaviour
{
    [SerializeField]protected Transform explosiveCenter;
    [SerializeField]protected float explosiveRadius;
    [SerializeField]protected float waitTime;
    [SerializeField]protected float startwaitTime;

    private void Start() => waitTime = startwaitTime;
 
    protected virtual void Update()
    {
        if (waitTime <= 0)
        {
            waitTime = startwaitTime;
            Explose(explosiveCenter, explosiveRadius);
        }
        else
            waitTime -= Time.deltaTime;
    }

    protected virtual void Explose(Transform center, float radius)
    {
        Collider[] _hitColliders = Physics.OverlapSphere(center.position, radius);
        foreach (var colliders in _hitColliders)
        {
            if (colliders.CompareTag("Destroyable"))
                Destroy(colliders.gameObject);
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(explosiveCenter.position, explosiveRadius);
    }
}
