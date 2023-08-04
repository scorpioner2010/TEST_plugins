using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public static Earth In;

    private void Awake()
    {
        In = this;
    }
}
