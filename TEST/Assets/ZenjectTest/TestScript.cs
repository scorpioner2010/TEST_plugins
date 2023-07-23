using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class TestScript : MonoBehaviour
{
    [Inject] public Player player;

    private void Start()
    {
        player.transform.DOJump(new Vector3(5, player.transform.position.y, 0), 3, 1, 2);
    }
}

