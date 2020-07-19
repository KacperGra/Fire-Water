using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private const int numberOfPlayers = 2; 
    private readonly Transform[] player = new Transform[numberOfPlayers];

    private void Start()
    {
        var players = FindObjectsOfType<Player>();
        for(int i = 0; i < numberOfPlayers; ++i)
        {
            player[i] = players[i].transform;
        }       
    }

    void Update()
    {
        var center = Vector3.Lerp(player[0].position, player[1].position, 0.5f);

        if(center.y <= 4.58f && center.y >= -3.37f)
        {
            transform.position = new Vector3(transform.position.x, center.y, transform.position.z);
        }
        if(center.x <= 11f && center.x >= -9.6f)
        {
            transform.position = new Vector3(center.x, transform.position.y, transform.position.z);
        }
    }
}
