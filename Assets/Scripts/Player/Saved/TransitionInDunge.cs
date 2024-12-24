using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneWork
{
    public class TransitionInDunge : MonoBehaviour
    {
        [SerializeField] private GameObject[] button;
        [SerializeField] private GameObject[] buttonNotDostup;
        [SerializeField] private ManagerKey KeyManagerControl;

        int count = 0;
        private void Start()
        {
            count = KeyManagerControl.StatsPlusic();
            for(int i = 0; i <= count; i++)
            {
                button[i].SetActive(true);
                buttonNotDostup[i].SetActive(false);
            }
        }

        public void ToDange(int indexScene)
        {
            OnExit.SaveInfo();
            SceneManager.LoadScene(indexScene);
        }
    }
}

