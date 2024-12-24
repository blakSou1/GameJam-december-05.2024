using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneWork
{
    public class StartGame : MonoBehaviour
    {
        public void StartGameVoid()
        {
            SceneManager.LoadScene(1);
        }
    }
}

