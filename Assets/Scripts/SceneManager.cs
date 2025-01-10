using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    IEnumerator TestSceneManageRoutine()
    {
        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        SetPlayerPosition();

        yield return new WaitForSeconds(10f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        SetPlayerPosition();
    }

    private void SetPlayerPosition()
    {
        Transform spawnTransform = GameObject.FindWithTag("SpawnPos").transform;
        cameraTransform.position = spawnTransform.position;
    }
}