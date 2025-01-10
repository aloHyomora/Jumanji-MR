using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonEventHandler : MonoBehaviourPunCallbacks
{
    public const byte SendJumanjiInfoEvent = 2; // Jumanji 정보 전송 이벤트 코드

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += HandleEvent; // 이벤트 핸들러 등록
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= HandleEvent; // 이벤트 핸들러 해제
    }

    public void HandleEvent(EventData photonEvent)
    {
        if (photonEvent.Code == SendJumanjiInfoEvent)
        {
            // Jumanji 정보 전송 이벤트 처리
            
            try
            {
                // 데이터 무결성 확인
                if (photonEvent.CustomData == null)
                {
                    Debug.Log("CustomData is null", this);
                    return;
                }

                object[] data = photonEvent.CustomData as object[];
                if (data == null || data.Length < 1 || !(data[0] is JumanjiInfo))
                {
                    Debug.Log("Invalid CustomData received or missing MatchInfo", this);
                    return;
                }

                // 데이터 처리
                JumanjiInfo receivedInfo = (JumanjiInfo)data[0];
                Debug.Log($"Message \'Move to {receivedInfo.newPositionIndex}\' From {receivedInfo.userName} ");
                
                // TODO: 아두이노가 보낸 데이터를 포톤 네트워크를 통해 보내서 받음
                // TODO: 실제로 움직이는 로직 처리
            }
            catch (Exception ex)
            {
                Debug.Log($"Error handling photon event: {ex.Message}", this);
            }
        }
    }

    [ContextMenu("Send Juman Info")]
    public void TestSendJumanjiInfo()
    {
        SendJumanjiInfo(3);
    }
    
    public void SendJumanjiInfo(int newPositionIndex)
    {
        int actorNum = PhotonUserUtility.GetOtherPlayerCode();
        
        Debug.Log($"Send Message to ({actorNum})");
        
        // 유효성 검사: Actor Number 확인
        if (PhotonNetwork.CurrentRoom.GetPlayer(actorNum) == null)
        {
            Debug.Log($"Invalid targetActorNumber: {actorNum}");
            return;
        }

        JumanjiInfo jumanjiInfo = new JumanjiInfo
        {
            userName = PhotonNetwork.NickName,
            newPositionIndex = newPositionIndex.ToString()
        };
        
        // MatchInfo 포함한 데이터 생성
        object[] data = new object[] { jumanjiInfo };

        // RaiseEventOptions 설정
        RaiseEventOptions options = new RaiseEventOptions
        {
            TargetActors = new int[] { actorNum }
        };

        try
        {
            PhotonNetwork.RaiseEvent(SendJumanjiInfoEvent, data, options, SendOptions.SendReliable);
            Debug.Log($"Send Message to ({actorNum})");
        }
        catch (Exception ex)
        {
            Debug.Log($"Failed to send UserInfo: {ex.Message}");
        }
    }
}