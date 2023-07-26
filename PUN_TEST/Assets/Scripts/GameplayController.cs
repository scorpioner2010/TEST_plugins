using System;
using System.Globalization;
using Patterns;
using Photon.Pun;
using UnityEngine;

public class GameplayController : MonoBehaviourPun
{
    private GameObject _mainPlayer;

    public static GameplayController In;
    
    private readonly float _updateInterval = 0.1f;
    private float _accum = 0.0f;
    private int _frames = 0;
    private float _timeleft;
    private float _fps;
    private GUIStyle _textStyle = new();

    public static event Action<Vector2> OnRotate;
    public static event Action<Vector2> OnMove;
    
    
    
    private void Awake()
    {
        In = this;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 250, 100, 25), _fps.ToString("0", CultureInfo.InvariantCulture) + " FPS", _textStyle);
    }

    public void Rotate(Vector2 parameters)
    {
        OnRotate?.Invoke(parameters);
    }
    
    public void Move(Vector2 parameters)
    {
        OnMove?.Invoke(parameters);
    }
    
    private void Start()
    {
        _timeleft = _updateInterval;
        _textStyle.fontStyle = FontStyle.Bold;
        _textStyle.fontSize = 25;
        _textStyle.normal.textColor = Color.black;
        Application.targetFrameRate = 500;
        Input.multiTouchEnabled = true;
        
        PhotonGameManager.In.OnLeftRoomAction += () =>
        {
            if (_mainPlayer != null)
            {
                Destroy(_mainPlayer);
            }
        };
        
        
        Vector3 place = Singleton<Places>.Instance.allPlaces[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position + Vector3.up * 5;
        _mainPlayer = PhotonNetwork.Instantiate($"GamePlayer", place, Quaternion.identity); //create player
        Player player = _mainPlayer.GetComponent<Player>();
        player.virtualCamera.gameObject.SetActive(true);
        
//        Debug.LogError("CreatePerson: "+_mainPlayer);
        
        
        //Debug.LogError("amount: "+PhotonNetwork.PlayerList.Length);
        //Debug.LogError("id: "+PhotonNetwork.LocalPlayer.UserId);
        //Debug.LogError("Actor: "+ PhotonNetwork.LocalPlayer.ActorNumber);
        
        
        //photonView.RPC("CarSpawn", player, index);
    }
    
    /*
    [PunRPC]
    private void CarSpawn(int index)
    {
        
    }
    */
    
    private void Update()
    {
        FPSCounterBehaviour();
    }

    private void FPSCounterBehaviour()
    {
        _timeleft -= Time.deltaTime;
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;

        if (_timeleft <= 0)
        {
            _fps = (_accum / _frames);
            _timeleft = _updateInterval;
            _accum = 0;
            _frames = 0;
        }
    }
}
