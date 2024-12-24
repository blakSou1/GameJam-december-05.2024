using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string nameAnimation;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(nameAnimation);
        StartCoroutine("LoadNextScene");
    }
    private IEnumerator LoadNextScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NumberSceneLoad.numberSceneLoad);
        while (!asyncLoad.isDone)
        {
            yield return new WaitForSeconds(0.3f);
        }
    }
}
