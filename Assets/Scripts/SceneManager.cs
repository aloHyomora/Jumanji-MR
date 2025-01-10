using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private Coroutine _loadingRoutine;
    public Transform cameraTransform;
    void Start()
    {
        if (_loadingRoutine == null)
        {
            _loadingRoutine = StartCoroutine(TestSceneManageRoutine());
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SetPlayerPosition();
    }

    IEnumerator TestSceneManageRoutine()
    {
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    private void SetPlayerPosition()
    {
        Transform spawnTransform = GameObject.FindWithTag("SpawnPos").transform;
        cameraTransform.position = spawnTransform.position;
        cameraTransform.rotation = spawnTransform.rotation;
    }
}