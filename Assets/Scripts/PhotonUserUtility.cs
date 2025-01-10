using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonUserUtility : MonoBehaviourPunCallbacks
{
    public static int GetOtherPlayerCode()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log($"Other Player code : {player.ActorNumber}");
                return player.ActorNumber;
            }
        }
        return -1;
    }
}
