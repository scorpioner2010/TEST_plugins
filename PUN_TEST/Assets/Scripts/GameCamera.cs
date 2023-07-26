using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    //private Transform _player;

    public Camera cameraMain;
    
    private void Awake()
    {
        cameraMain = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        /*
        if (_player == null)
        {
            return;
        }
        */
        //transform.position = _player.transform.position - _player.transform.forward * 5 + _player.transform.up * 4;
    }
}
