using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private Coroutine _loadingRoutine;
    private GameObject gameBoard;
    public Transform cameraTransform;
    public PathMoveExample airplane;
    void Start()
    {
        gameBoard = GameObject.Find("GameBoard");
        /*if (_loadingRoutine == null)
        {
            _loadingRoutine = StartCoroutine(TestSceneManageRoutine());
        }*/
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }


    [ContextMenu("index 5")]
    public void Index3test()
    {
        SceneIndexSetting(5);
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
                airplane.pathMoveDotween(2);
                _loadingRoutine = StartCoroutine(SceneManageRoutine(1));
                break;
            case 3:
                airplane.pathMoveDotween(3);
                break;
            case 4:
                airplane.pathMoveDotween(4);
                _loadingRoutine = StartCoroutine(SceneManageRoutine(2));
                break;
            case 5:
                airplane.pathMoveDotween(5);
                _loadingRoutine = StartCoroutine(SceneForIsland(3));
                break;
            case 6:
                airplane.pathMoveDotween(6);
                _loadingRoutine = StartCoroutine(SceneManageRoutine(4));
                break;
            case 7:
                airplane.pathMoveDotween(7);
                break;
            case 8:
                airplane.pathMoveDotween(8);
                _loadingRoutine = StartCoroutine(SceneManageRoutine(5));
                break;
            default:
                break;
        }
    }

    IEnumerator SceneManageRoutine(int i)
    {
        Debug.Log($"SceneManager.LoadScene({i})");

        //airplane.pathMoveDotween(i);
     
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);

        gameBoard.SetActive(false);
        yield return new WaitForSeconds(15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
        gameBoard.SetActive(true);

    }

    IEnumerator SceneForIsland(int i)
    {
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);

        gameBoard.SetActive(false);
    }

    private void SetPlayerPosition()
    {
        if (GameObject.FindWithTag("SpawnPos") != null)
        {
            Transform spawnTransform = GameObject.FindWithTag("SpawnPos").transform;
            cameraTransform.position = spawnTransform.position;
            cameraTransform.rotation = spawnTransform.rotation;
        }
    
    }
}