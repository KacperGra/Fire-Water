using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [HideInInspector]
    public Vector4 screenBounds;

    private void Start()
    {
        screenBounds = new Vector4(-18.5f, 20f, -9.12f, 8.8f); // x - Left; y - Right; z - Up; w - Down;
    }
}
