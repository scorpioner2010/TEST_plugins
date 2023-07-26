using System;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using Photon.Pun;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    public static PhotonGameManager In;

    public event Action OnConnectedToMasterAction;
    public event Action<List<RoomInfo>> OnRoomListUpdateAction;
    public event Action OnJoinedLobbyAction;
    public event Action OnLeftLobbyAction;
    public event Action<short,string> OnCreateRoomFailedAction;
    public event Action<short,string> OnJoinRoomFailedAction;
    public event Action<short,string> OnJoinRandomFailedAction;
    public event Action OnJoinedRoomAction;
    public event Action OnLeftRoomAction;
    

    private void Awake()
    {
        In = this;
    }
    
    public override void OnConnectedToMaster()
    {
        OnConnectedToMasterAction?.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        OnRoomListUpdateAction?.Invoke(roomList);
    }

    public override void OnJoinedLobby()
    {
        OnJoinedLobbyAction?.Invoke();
    }

    // note: when a client joins / creates a room, OnLeftLobby does not get called, even if the client was in a lobby before
    public override void OnLeftLobby()
    {
        OnLeftLobbyAction?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        OnCreateRoomFailedAction?.Invoke(returnCode, message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        OnJoinRoomFailedAction?.Invoke(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        OnJoinRandomFailedAction?.Invoke(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        OnJoinedRoomAction?.Invoke();
    }

    public override void OnLeftRoom()
    {
        OnLeftRoomAction?.Invoke();
    }
}
