using UnityEngine;

public class LutingKey : MonoBehaviour
{
    [SerializeField] private ManagerKey man;
    [SerializeField] private int namberNamePlayerPrefs = 0;
    [SerializeField] private bool deleted = true;

    int stats = 0;
    private void Start()
    {
        if (PlayerPrefs.HasKey($"{namberNamePlayerPrefs}"))
            stats = PlayerPrefs.GetInt($"{namberNamePlayerPrefs}");
        else
            PlayerPrefs.SetInt($"{namberNamePlayerPrefs}", stats);
        PlayerPrefs.Save();

        if (stats == 1)
            gameObject.SetActive(false);
        GetComponent<PointTrigerObject>().trigerE += UpdateKeyLuting;
    }
    private void UpdateKeyLuting()
    {
        stats = 1;
        man.UpdateKeyLuting(1);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (deleted)
            stats = 0;
        PlayerPrefs.SetInt($"{namberNamePlayerPrefs}", stats);
        PlayerPrefs.Save();

        GetComponent<PointTrigerObject>().trigerE -= UpdateKeyLuting;
    }
}
