using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Cam : MonoBehaviour
{
    public const int playersNumber = 1;
    public Transform[] player = new Transform[playersNumber];

    private void Start()
    {
        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < playersNumber; ++i)
        {
            player[i] = players[i].transform;
        }
    }

    void Update()
    {
        var center = Vector3.Lerp(player[0].position, player[1].position, .5f);

        if (center.y <= 4.58f && center.y >= -3.37f)
        {
            transform.position = new Vector3(transform.position.x, center.y, transform.position.z);
        }
        if (center.x <= 11f && center.x >= -9.6f)
        {
            transform.position = new Vector3(center.x, transform.position.y, transform.position.z);
        }
    }
}
