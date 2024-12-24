using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int numberSceneLoad = 1;
    [SerializeField] private int numberSpawnPosition = 0;
    [SerializeField] private SaveAndLoadManager _saveAndLoadManager;
    private PointTrigerObject pto;

    void Start()
    {
        pto = GetComponent<PointTrigerObject>();
        _saveAndLoadManager = FindFirstObjectByType<SaveAndLoadManager>();
        pto.trigerE += LoadScenes;
    }

    // Update is called once per frame
    private void LoadScenes()
    {
        pto.trigerE -= LoadScenes;
        NumberSceneLoad.numberSceneLoad = numberSceneLoad;
        NumberSceneLoad.numberPositionLoad = numberSpawnPosition;
        _saveAndLoadManager.Save();
        SceneManager.LoadScene(1);
    }
}
