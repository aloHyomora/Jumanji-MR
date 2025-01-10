using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoDataHandler : MonoBehaviour
{
    public static ArduinoDataHandler Instance;
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    
    
    // Index 중복 처리용 프로퍼티
    private int _newPositionIndex;
    public int newPositionIndex
    {
        get => _newPositionIndex;
        set
        {
            // 기존 값과 새로운 값이 다른 경우
            if (_newPositionIndex != value)
            {
                _newPositionIndex = value; // 값을 변경
                OnIndexChanged(); // 메서드 호출
            }
        }
    }
    // 값이 변경될 때 실행할 메서드
    private void OnIndexChanged()
    {
        Debug.Log($"Name property has been changed to: {_newPositionIndex}");

        if (PhotonEventHandler.Instance != null)
        {
            PhotonEventHandler.Instance.SendJumanjiInfo(_newPositionIndex);  
        }
        else
        {
            Debug.Log("PhotonEventHandler.Instance is null");
        }
    }
}
