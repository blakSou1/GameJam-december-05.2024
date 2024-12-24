using UnityEngine;

public class Vidjet : MonoBehaviour
{
    [SerializeField] private GameObject Vidjets;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerInputETriger>() != null && Vidjets != null)
            Vidjets.SetActive(true);
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerInputETriger>() != null && Vidjets != null)
            Vidjets.SetActive(false);
    }
    private void OnEnable()
    {
        if (Vidjets != null)
            Vidjets.SetActive(false);
    }
    private void OffVidjets()
    {
        Vidjets.SetActive(false);
    }
}
