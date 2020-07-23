using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public float timeToSpawn;
    private float currentTimeToSpawn = 0f;

    private void Update()
    {
        currentTimeToSpawn += Time.deltaTime;
        if(currentTimeToSpawn > timeToSpawn)
        {
            var willSpawn = Random.Range(0, 2);
            if(willSpawn.Equals(1))
            {
                var randomMonsterValue = Random.Range(0, monsterPrefabs.Length);
                var monster = Instantiate(monsterPrefabs[randomMonsterValue]) as GameObject;
                //monster.transform.position = transform.position;
                monster.transform.position = new Vector3(transform.position.x, Random.Range(-7f, 9f), transform.position.z);
                if (timeToSpawn > 2f) // DEMO
                {
                    timeToSpawn -= 0.05f;
                }
            }
            currentTimeToSpawn = 0f;
        }
    }
}
