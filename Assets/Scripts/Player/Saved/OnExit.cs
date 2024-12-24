using UnityEngine;

namespace SceneWork
{
    public class OnExit : MonoBehaviour
    {
        private static Coins coinScript;
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            coinScript = FindFirstObjectByType<Coins>();
            LoadInfo();
        }

        private void OnApplicationQuit()
        { 
            if (PlayerPrefs.HasKey("isWork"))
            {
                SaveInfo(); //Сохраняет все значение заданные во время рантайма при выходе
            }
            
        }

        public static void SaveInfo()
        {
            PlayerPrefs.SetInt("isWork", Dynamit.isWorked);
        }

        public static void LoadInfo()
        {
            Dynamit.isWorked = PlayerPrefs.GetInt("isWork");
        }
    }
}

