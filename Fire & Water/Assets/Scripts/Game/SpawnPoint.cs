using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public float timeToSpawn;

    private void Start()
    {
        InvokeRepeating("SpawnMonster", 0f, timeToSpawn);
    }

    private void SpawnMonster()
    {
        var randomMonsterValue = Random.Range(0, monsterPrefabs.Length);
        var monster = Instantiate(monsterPrefabs[randomMonsterValue]) as GameObject;
        monster.transform.position = new Vector3(transform.position.x, Random.Range(-7f, 9f), transform.position.z);
    }
}
