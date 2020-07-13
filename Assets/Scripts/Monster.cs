using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private const int numberOfPlayers = 2;
    [Header(header: "Movement")]
    public float moveSpeed;
    private Transform[] player = new Transform[numberOfPlayers];
    
    private Vector2 direction;

    private void Start()
    {
        Player[] players = new Player[numberOfPlayers];
        players = FindObjectsOfType<Player>();
        for(int i = 0; i < numberOfPlayers; ++i)
        {
            player[i] = players[i].transform;
        }
    }

    private void Update()
    {
        SelectCloserTarget();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(direction.x * -moveSpeed * Time.fixedDeltaTime, direction.y * -moveSpeed * Time.fixedDeltaTime);
    }

    void SelectCloserTarget()
    {
        //  Calculate which target is closer and chose it as target
        Vector2[] heading = new Vector2[numberOfPlayers];
        float[] distance = new float[numberOfPlayers];
        for(int i = 0; i < numberOfPlayers; ++i) // Loop sets heading and distance for all players
        {
            heading[i] = transform.position - player[i].position;
            distance[i] = heading[i].magnitude;
        }

        var closerPlayerDistance = distance[0];
        var closerPlayerHeading = heading[0];
        for(int i = 1; i < numberOfPlayers; ++i)
        {
            if(closerPlayerDistance > distance[i])
            {
                closerPlayerDistance = distance[i];
                closerPlayerHeading = heading[i];
            }
        }
        direction = closerPlayerHeading / closerPlayerDistance;
    }
}
