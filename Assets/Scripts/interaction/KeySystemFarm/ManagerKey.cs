using UnityEngine;
using UnityEngine.UI;

public class ManagerKey : MonoBehaviour
{
    [SerializeField] private int maxCountKey = 3;
    [SerializeField] private Text textKeyCount;
    [SerializeField] private bool deleteStatistic = true;
    [SerializeField] private GameObject Titrs;
    [SerializeField] private GameObject plusik;
    [SerializeField] private Text timeText;

    int keyCountReal = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("key"))
            keyCountReal = PlayerPrefs.GetInt("key");
        else
            PlayerPrefs.SetInt("key", 0);
        PlayerPrefs.Save();

        textKeyCount.text = $"{keyCountReal}/{maxCountKey}";
        Titrs.SetActive(false);
        plusik.SetActive(false);
    }
    public int StatsPlusic()
    {
        return keyCountReal;
    }
    public void UpdateKeyLuting(int count)
    {
        keyCountReal += count;
        textKeyCount.text = $"{keyCountReal}/{maxCountKey}";
        if (keyCountReal == maxCountKey)
        {
            Titrs.SetActive(true);
            plusik.SetActive(true);
            timeText.text = $"{Time.time / 60}";
        }
    }
    private void OnDisable()
    {
        if (!deleteStatistic)
            PlayerPrefs.SetInt("key", keyCountReal);
        else
            PlayerPrefs.SetInt("key", 0);

        PlayerPrefs.Save();
    }
}
