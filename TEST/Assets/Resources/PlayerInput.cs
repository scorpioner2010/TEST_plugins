using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    void Update()
    {
        Debug.LogError("Input update");
    }

    public void Test()
    {
        Debug.LogError("play interface");
    }
}

public interface IPlayerInput
{
    void Test();
}
