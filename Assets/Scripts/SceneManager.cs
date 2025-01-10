using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private Coroutine _loadingRoutine;
    public Transform cameraTransform;
    void Start()
    {
        /*if (_loadingRoutine == null)
        {
            _loadingRoutine = StartCoroutine(TestSceneManageRoutine());
        }*/
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SetPlayerPosition();
    }

    /*IEnumerator TestSceneManageRoutine()
    {
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }*/

    public void SceneIndexSetting(int index)
    {
        switch (index)
        {
            case 2:
                _loadingRoutine = StartCoroutine(SceneManageRoutine(1));
                break;
            case 4:
                _loadingRoutine = StartCoroutine(SceneManageRoutine(2));
                break;
            case 5:
                SceneForIsland();
                break;
            case 6:
                _loadingRoutine = StartCoroutine(SceneManageRoutine(4));
                break;
            default:
                break;
        }
    }

    IEnumerator SceneManageRoutine(int i)
    {
        yield return new WaitForSeconds(8f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);

        yield return new WaitForSeconds(15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);

    }

    private void SceneForIsland()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    private void SetPlayerPosition()
    {
        Transform spawnTransform = GameObject.FindWithTag("SpawnPos").transform;
        cameraTransform.position = spawnTransform.position;
        cameraTransform.rotation = spawnTransform.rotation;
    }
}