using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivot : MonoBehaviour
{
    private Transform _player;
    
    public void Init(Transform player)
    {
        _player = player;
    }
    
    private void Update()
    {
        transform.position = _player.transform.position;
    }
}
