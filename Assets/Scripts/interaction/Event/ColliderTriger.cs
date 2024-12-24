using UnityEngine;

[RequireComponent(typeof(PointTrigerObject))]
public class ColliderTriger : MonoBehaviour
{
    [SerializeField] private bool deleteColliderNextUpdate = false;

    private void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            GetComponent<PointTrigerObject>().StartEvent();
            if (deleteColliderNextUpdate)
                Destroy(gameObject, 0);
        }
    }
}
