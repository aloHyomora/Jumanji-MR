using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

[Serializable]
public class JumanjiInfo{
    public string userName {get;set;}
    public string newPositionIndex {get;set;}
}

public class PhotonCustomTypeRegistration : MonoBehaviourPunCallbacks
{
    // Photon 메세지로 주고받는 데이터 타입 정의하는 스크립트
    private void Awake()
    {
        // UserInfo 직렬화 등록
        PhotonPeer.RegisterType(
            typeof(JumanjiInfo),             // 타입 지정
            (byte)'U',                    // 타입의 식별자 (고유해야 함)
            SerializeUserInfo,            // 직렬화 메서드
            DeserializeUserInfo           // 역직렬화 메서드
        );
    }
    
    // UserInfo를 직렬화하는 함수a
    private static byte[] SerializeUserInfo(object customObject)
    {
        JumanjiInfo jumanjiInfo = (JumanjiInfo)customObject;
        using (MemoryStream stream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(jumanjiInfo.userName ?? "");
                writer.Write(jumanjiInfo.newPositionIndex ?? "");
            }
            return stream.ToArray();
        }
    }

    // UserInfo를 역직렬화하는 함수
    private static object DeserializeUserInfo(byte[] data)
    {
        JumanjiInfo jumanjiInfo = new JumanjiInfo();
        using (MemoryStream stream = new MemoryStream(data))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                jumanjiInfo.userName = reader.ReadString();
                jumanjiInfo.newPositionIndex = reader.ReadString();
            }
        }
        return jumanjiInfo;
    }
}
