using UnityEngine;

public class DeadReaction : MonoBehaviour
{
    [SerializeField] private GameObject panelDead;

    private void Start()
    {
        panelDead.SetActive(false);
        GetComponent<HitPlayer>().DeadReaction += Dead;
    }

    private void Dead()
    {
        panelDead.SetActive(true);
    }
    private void OnDisable()
    {
        GetComponent<HitPlayer>().DeadReaction -= Dead;
    }
}
