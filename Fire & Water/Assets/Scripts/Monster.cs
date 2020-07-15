using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private const int numberOfPlayers = 2;
    [Header(header: "Movement")]
    public float moveSpeed;
    private readonly Transform[] player = new Transform[numberOfPlayers];

    public bool isWaterElemental = false, isFireElemental = false;
    public GameObject elementalShadow;
    
    private Vector2 direction;

    private void Start()
    {
        var players = FindObjectsOfType<Player>();
        for(int i = 0; i < numberOfPlayers; ++i)
        {
            player[i] = players[i].transform;
        }

        // Script to set color and elemental type of monster
        int randomValue = Random.Range(0, 2);
        const int alphaComponent = 120;
        var waterColor = new Color32(36, 36, 245, alphaComponent);
        var fireColor = new Color32(255, 40, 40, alphaComponent);
        switch(randomValue)
        {
            case 0:
                elementalShadow.GetComponent<SpriteRenderer>().color = waterColor;
                isWaterElemental = true;
                break;
            case 1:
                elementalShadow.GetComponent<SpriteRenderer>().color = fireColor;
                isFireElemental = true;
                break;
            default:
                break;
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
        var heading = new Vector2[numberOfPlayers];
        var distance = new float[numberOfPlayers];
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
