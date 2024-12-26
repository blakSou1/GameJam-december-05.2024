using UnityEngine;

public class Bullets : MonoBehaviour
{
    [HideInInspector]
    public GrapplingHook grap;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {

            GrapplingHook._holdAllow = false;

        }
    }
}
