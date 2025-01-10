using System;
using System.IO.Ports;
using UnityEngine;

public class ArduinoSerialReader : MonoBehaviour
{
    [SerializeField] private string portName = "COM5";
    [SerializeField] private int baudRate = 9600;

    // �ø��� ��Ʈ ��ü
    private SerialPort serialPort;

    void Start()
    {
        // �ø��� ��Ʈ �ʱ�ȭ
        serialPort = new SerialPort(portName, baudRate);
       
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 500; // �б� Ÿ�Ӿƿ�(ms)
            Debug.Log("�ø��� ��Ʈ ���� ����: " + portName);
        }
        catch (Exception e)
        {
            Debug.LogError("�ø��� ��Ʈ ���� ����: " + e.Message);
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
                    Debug.Log("���� ������: " + data);

                    // =============== ���⼭ index �־��ָ� Photon �޼����� ������ @@@@@
                    if(int.TryParse(data, out index))
                    {
                        ArduinoDataHandler.Instance.newPositionIndex = index;
                    }
                }
            }
            catch (TimeoutException)
            {
                // ReadLine()���� Ÿ�Ӿƿ� �߻� �� ���� (�����Ͱ� ���� ��)
            }
            catch (Exception e)
            {
                Debug.LogError("�ø��� ���� ����: " + e.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        // ������ ����ǰų� ����� �ٲ� �� ��Ʈ �ݱ�
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("�ø��� ��Ʈ ����");
        }
    }
}
