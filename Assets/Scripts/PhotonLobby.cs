using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        public static PhotonLobby Lobby;

        private int roomNumber = 1;
        private int userIdCount;

        private void Awake()
        {
            if (Lobby == null)
            {
                Lobby = this;
            }
            else
            {
                if (Lobby != this)
                {
                    Destroy(Lobby.gameObject);
                    Lobby = this;
                }
            }

            DontDestroyOnLoad(gameObject);

            GenericNetworkManager.OnReadyToStartNetwork += StartNetwork;
        }

        public override void OnConnectedToMaster()
        {
            var randomUserId = Random.Range(0, 999999);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.UserId = randomUserId.ToString();
            userIdCount++;
            PhotonNetwork.NickName = PhotonNetwork.AuthValues.UserId;
            
            // 먼저 로비에 입장
            TypedLobby conferenceLobby = new TypedLobby("Conference", LobbyType.Default);
            PhotonNetwork.JoinLobby(conferenceLobby);   
            
            Debug.Log($"마스터 서버 {conferenceLobby.Name} 연결 완료, 로비 입장 시도");            

        }
        public override void OnJoinedLobby()
        {            
            Debug.Log("로비 입장 완료");
            
            // 마스터 클라이언트만 방을 생성하도록 함
            if (PhotonNetwork.IsMasterClient)
            {                
                var roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = 4};
                PhotonNetwork.CreateRoom("DefaultRoom", roomOptions);
                Debug.Log("Default Room 생성 완료");
            }
            else
            {
                // 마스터가 아닌 클라이언트는 방 입장만 시도
                PhotonNetwork.JoinRoom("DefaultRoom");
                Debug.Log("Default Room 입장");
            }
        }
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            Debug.Log("\nPhotonLobby.OnJoinedRoom()");
            Debug.Log("Current room name: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("Other players in room: " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("Total players in room: " + (PhotonNetwork.CountOfPlayersInRooms + 1));
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Default Room Join Failed, Create room");
            CreateRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("\nPhotonLobby.OnCreateRoomFailed()");
            Debug.LogError("Creating Room Failed");
            CreateRoom();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            roomNumber++;
        }

        public void OnCancelButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        private void StartNetwork()
        {
            PhotonNetwork.ConnectUsingSettings();
            Lobby = this;
        }

        private void CreateRoom()
        {
            var roomOptions = new RoomOptions {IsVisible = true, IsOpen = true, MaxPlayers = 4};
            PhotonNetwork.CreateRoom("DefaultRoom", roomOptions);
        }
    }
