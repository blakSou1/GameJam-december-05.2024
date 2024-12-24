using UnityEngine;

public class ObjectOne : MonoBehaviour
{
    [SerializeField] private GameObject managerDungeon;

    private void Start()
    {
        GetComponent<PointTrigerObject>().trigerE += ActivateManager;
    }

    private void ActivateManager()
    {
        managerDungeon.SetActive(true);
    }

    private void OnDisable()
    {
        GetComponent<PointTrigerObject>().trigerE -= ActivateManager;
    }
}
