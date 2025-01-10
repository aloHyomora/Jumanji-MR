using System;
using System.IO.Ports;
using UnityEngine;

public class ArduinoSerialReader : MonoBehaviour
{
    [SerializeField] private string portName = "COM5";
    [SerializeField] private int baudRate = 9600;

    // 시리얼 포트 객체
    private SerialPort serialPort;

    void Start()
    {
        // 시리얼 포트 초기화
        serialPort = new SerialPort(portName, baudRate);
       
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 500; // 읽기 타임아웃(ms)
            Debug.Log("시리얼 포트 연결 성공: " + portName);
        }
        catch (Exception e)
        {
            Debug.LogError("시리얼 포트 열기 실패: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
            
                string data = serialPort.ReadLine();
                if (!string.IsNullOrEmpty(data))
                {
                    int index;
                    Debug.Log("수신 데이터: " + data);

                    // =============== 여기서 index 넣어주면 Photon 메세지도 보내짐 @@@@@
                    if(int.TryParse(data, out index))
                    {
                        ArduinoDataHandler.Instance.newPositionIndex = index;
                    }
                }
            }
            catch (TimeoutException)
            {
                // ReadLine()에서 타임아웃 발생 시 무시 (데이터가 없는 것)
            }
            catch (Exception e)
            {
                Debug.LogError("시리얼 수신 에러: " + e.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        // 게임이 종료되거나 장면이 바뀔 때 포트 닫기
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("시리얼 포트 해제");
        }
    }
}
