using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button create;
    public Button connect;

    private void Start()
    {
        CreatePlayer();
        
        create.onClick.AddListener(CreateRoom);
        connect.onClick.AddListener(Connect);
        
        create.gameObject.SetActive(false);
        connect.gameObject.SetActive(false);
        
        PhotonGameManager.In.OnConnectedToMasterAction += () =>
        {
            create.gameObject.SetActive(true);
            connect.gameObject.SetActive(true);
        };
        
        PhotonGameManager.In.OnJoinedRoomAction += () =>
        {
            PhotonNetwork.LoadLevel("Game");
        };
    }
    
    private void CreatePlayer()
    {
        PhotonNetwork.LocalPlayer.NickName = "player: "+UnityEngine.Random.Range(1000, 10000);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void CreateRoom()
    {
        string roomName = "Room " + UnityEngine.Random.Range(1000, 10000);
        RoomOptions options = new RoomOptions { MaxPlayers = 8 };
        PhotonNetwork.CreateRoom(roomName, options, null);
    }
}
