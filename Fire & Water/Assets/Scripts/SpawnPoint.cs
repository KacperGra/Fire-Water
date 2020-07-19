using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private const int numberOfMonsters = 2;
    public GameObject[] monsterPrefabs = new GameObject[numberOfMonsters];
    public float timeToSpawn;
    private float currentTimeToSpawn = 0f;

    private void Update()
    {
        currentTimeToSpawn += Time.deltaTime;
        if(currentTimeToSpawn > timeToSpawn)
        {
            var randomMonsterValue = Random.Range(0, numberOfMonsters);
            var monster = Instantiate(monsterPrefabs[randomMonsterValue]) as GameObject;
            monster.transform.position = transform.position;
            currentTimeToSpawn = 0f;
        }
    }
}
