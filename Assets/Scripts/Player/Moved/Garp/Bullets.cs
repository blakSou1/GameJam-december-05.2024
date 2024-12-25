using UnityEngine;

public class Bullets : MonoBehaviour
{
    [HideInInspector]
    public GrapplingHook grap;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            //grap.GetComponent<GrapplingHook>().PlayerStart(transform.position);
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
