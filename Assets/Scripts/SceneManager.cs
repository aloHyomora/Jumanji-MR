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

    [ContextMenu("index 2")]
    public void Index2test()
    {
        SceneIndexSetting(2);
    }

    [ContextMenu("index 3")]
    public void Index3test()
    {
        SceneIndexSetting(3);
    }

    [ContextMenu("index 4")]
    public void Index4test()
    {
        SceneIndexSetting(4);
    }

    [ContextMenu("index 5")]
    public void Index5test()
    {
        SceneIndexSetting(5);
    }

    [ContextMenu("index 6")]
    public void Index6test()
    {
        SceneIndexSetting(6);
    }

    [ContextMenu("index 7")]
    public void Index7test()
    {
        SceneIndexSetting(7);
    }

    [ContextMenu("index 8")]
    public void Index8test()
    {
        SceneIndexSetting(8);
    }


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
                //_loadingRoutine = StartCoroutine(SceneManageRoutine(2));
                break;
            case 5:
                airplane.pathMoveDotween(5);
                SceneForIsland();
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
     
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);

        gameBoard.SetActive(false);
        yield return new WaitForSeconds(15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
        gameBoard.SetActive(true);

    }

    private void SceneForIsland()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
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