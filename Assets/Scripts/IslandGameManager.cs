using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGameManager : MonoBehaviour
{
    public static IslandGameManager Instance;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        
        boxIndexes = new []{0,1,2,3,4,5,6,7,8,9,0,6,2,8,4,9,3,7,1,5,6,7,8,9,4,3,2,1};
    }

    public Material[] materials;
    public MeshRenderer[] objectRenderers;

    public float currentTime = 0f; // 현재 시간
    public float maxTime = 5f; // 최대 시간 (5초)

    private bool isRunning = true; // 타이머 실행 여부

    public int[] boxIndexes;
    public int currentBox = 0;
    public int successPoint = 0;
    
    void Update()
    {
        if (isRunning)
        {
            // 타이머 증가
            currentTime += Time.deltaTime;

            // 타이머가 최대 시간(5초)을 초과하면 정지
            if (currentTime >= maxTime)
            {
                currentTime = maxTime; // 최대 시간을 유지
                isRunning = false; // 타이머 정지
                ActivateNextBox();
                Debug.Log("Timer reached max time! Activate Next Box");
                ResetTimer();
            }

            // Debug.Log($"Current Time: {currentTime:F2}"); // 소수점 두 자리 출력
        }
    }

    // 타이머를 다시 초기화하고 실행
    [ContextMenu("Reset Timer")]
    public void ResetTimer()
    {
        currentTime = 0f; // 시간을 0으로 초기화
        isRunning = true; // 타이머 재시작
        Debug.Log("Timer reset!");
    }

    public void ActivateNextBox()
    {
        for (int i = 0; i < objectRenderers.Length; i++)
        {
            objectRenderers[i].material = materials[0];
            objectRenderers[i].gameObject.GetComponent<SelectableBox>().isSelectable = false;

        }

        int index = boxIndexes[currentBox];
        objectRenderers[index].material = materials[1];
        objectRenderers[index].gameObject.GetComponent<SelectableBox>().isSelectable = true;

        currentBox++;
        if(currentBox == boxIndexes.Length) currentBox = 0;
    }

    public void SuccessTouchBox()
    {
        successPoint++;

        if (successPoint == 20)
        {
            successPoint = 0;
            
            // TODO: 무인도 탈출~
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        }
    }
}
