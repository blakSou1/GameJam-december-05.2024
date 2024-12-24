using UnityEngine;

public class Dynamit : ExplosiveClass
{
    internal static int isWorked = 0;
    
    protected override void Explose(Transform center, float radius)
    {
        Collider[] _hitColliders = Physics.OverlapSphere(center.position, radius);
        foreach (var colliders in _hitColliders)
        {
            if (colliders.CompareTag("Destroyable") || colliders.CompareTag("NotDestroyable"))
                Destroy(colliders.gameObject);
        }
    }
}
