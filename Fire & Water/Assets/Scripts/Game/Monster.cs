﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour
{
    #region Variables
    private const int playersNumber = 1;
    private readonly Transform[] player = new Transform[playersNumber];
    private Vector2 direction;
    [Header(header: "Details")]
    public string MonsterName;
    [Range(1, 3)] public int health;
    [Range(0.4f, 4f)] public float moveSpeed;
    [HideInInspector] public string elementalName;
    [HideInInspector] public new Rigidbody2D rigidbody;
    public GameObject coinPrefab;
    public GameObject coinBagPrefab;
    [HideInInspector] public bool IsDead;
    private int coinsNumber;


    [Header(header: "Visual effects")]
    public GameObject elementalShadow;
    public GameObject explosionParticles;
    private Shake shake;

    [Header(header: "Shooting: (Cowboy only)")]
    public GameObject bulletPrefab;
    public GameObject shootPoint;
    #endregion

    #region Functions
    private void Start()
    {
        RandomizeStats();
        Init();
    }

    private void Update()
    {
        rigidbody.velocity *= new Vector2(0.98f, 0.98f); // Levels velocity to 0 after knockback
        Animation();
        SelectCloserTarget();
        
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(direction.x * -moveSpeed * Time.fixedDeltaTime, direction.y * -moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if(player != null)
        {
            player.TakeDamage();
            shake.CamShake();
            Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = shootPoint.transform.position;
        bullet.transform.rotation = transform.rotation;
    }

    void Animation()
    {
        if (direction.x < 0)
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else if (direction.x > 0)
        {
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
    }

    void RandomizeStats()
    {
        moveSpeed = Random.Range(moveSpeed - 0.25f, moveSpeed + 0.25f); // Ranodmized move speed a bit
        var randomScaleVal = Random.Range(0.85f, 1.2f);
        transform.localScale = new Vector3(randomScaleVal, randomScaleVal, transform.localScale.z); // Different size of monsters 

        int randomValue = Random.Range(0, 3); // Value: 0 (if TESTMODE activated)
        var gameMaster = FindObjectOfType<GameMaster>();
        if (gameMaster.testMode.Equals(true))
        {
            randomValue = 0;
        }
        const int alphaComponent = 120;
        var waterColor = new Color32(36, 36, 245, alphaComponent);
        var fireColor = new Color32(255, 40, 40, alphaComponent);
        var purpleColor = new Color32(255, 0, 189, alphaComponent);
        switch (randomValue)
        {
            case 0:
                elementalShadow.GetComponent<SpriteRenderer>().color = purpleColor;
                elementalName = "Both";
                
                break;
            case 1:
                elementalShadow.GetComponent<SpriteRenderer>().color = fireColor;
                elementalName = "Fire";
                break;
            case 2:
                elementalShadow.GetComponent<SpriteRenderer>().color = waterColor;
                elementalName = "Water";
                break;
            default:
                break;
        }
    }

    void Init()
    {
        shake = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Shake>();

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody.gravityScale = 0f;

        // Script sets to players transform variables values
        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < playersNumber; ++i)
        {
            player[i] = players[i].transform;
        }


        if (MonsterName.Equals("Cowboy"))
        {
            InvokeRepeating("Shoot", 1.5f, 3.5f);
        }

        var T1_CoinsNumber = Random.Range(1, 5);
        switch(MonsterName)
        {
            case "Slime":
                coinsNumber = T1_CoinsNumber;
                break;
            case "Eye Monster":
                coinsNumber = T1_CoinsNumber;
                break;
            case "Zombie":
                coinsNumber = T1_CoinsNumber;
                break;
            case "Big Mouth":
                coinsNumber = T1_CoinsNumber;
                break;
            case "Ball Monster":
                coinsNumber = T1_CoinsNumber;
                break;
            case "Bomb Monster":
                coinsNumber = T1_CoinsNumber;
                break;
            case "Cowboy":
                coinsNumber = T1_CoinsNumber;
                break;
        }  
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        shake.CamShake();
        if (health <= 0)
        {
            IsDead = true;
            FindObjectOfType<GameMaster>().MonsterDeath();
            SpawnCoin();
            Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void SpawnCoin()
    {
        var bagWithCoinsCapacity = 30;
        if(coinsNumber > bagWithCoinsCapacity)
        {
            var bagsNumber = coinsNumber / bagWithCoinsCapacity;
            for (int i = 0; i < bagsNumber; ++i)
            {
                var coinBag = Instantiate(coinBagPrefab) as GameObject;
                coinBag.transform.position = transform.position;
                var bagsRangeX = bagsNumber * 0.3f;
                if(bagsNumber > 1)
                {
                    coinBag.transform.position += new Vector3(Random.Range(-bagsRangeX, bagsRangeX), Random.Range(-0.25f, 0.25f));
                }
                coinBag.gameObject.GetComponent<Coin>().SetCoinValue(bagWithCoinsCapacity);
            }   
        }
        var otherCoins = coinsNumber % bagWithCoinsCapacity;
        for (int i = 0; i < otherCoins; ++i)
        {
            var coin = Instantiate(coinPrefab) as GameObject;
            coin.transform.position = transform.position;
            coin.gameObject.GetComponent<Coin>().SetCoinValue(1);
            if (otherCoins > 1)
            {
                var coinsRangeX = otherCoins * 0.08f;
                coin.transform.position += new Vector3(Random.Range(-coinsRangeX, coinsRangeX), Random.Range(-0.25f, 0.25f));
            }
        }
    }

    void SelectCloserTarget()
    {
        //  Calculate which target is closer and chose it as target
        var heading = new Vector2[playersNumber];
        var distance = new float[playersNumber];
        for(int i = 0; i < playersNumber; ++i) // Loop sets heading and distance for all players
        {
            heading[i] = transform.position - player[i].position;
            distance[i] = heading[i].magnitude;
        }

        var closerPlayerDistance = distance[0];
        var closerPlayerHeading = heading[0];
        for(int i = 1; i < playersNumber; ++i) // Script select closer target
        {
            if(closerPlayerDistance > distance[i])
            {
                closerPlayerDistance = distance[i];
                closerPlayerHeading = heading[i];
            }
        }
        direction = closerPlayerHeading / closerPlayerDistance;
    }
    #endregion
}
