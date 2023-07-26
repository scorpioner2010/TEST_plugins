using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ConnectionStatus : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Update()
    {
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        
        _text.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
    }
}