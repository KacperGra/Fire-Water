using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Player player;
    private Vector3 playerPosition;

    void Update()
    {
        playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
    }
}
