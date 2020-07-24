using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour
{
    private const int numberOfPlayers = 2;

    public string MonsterName; // If needed
    [Header(header: "Movement")]
    [HideInInspector]
    public new Rigidbody2D rigidbody;
    public float moveSpeed;
    private readonly Transform[] player = new Transform[numberOfPlayers];
    private Vector2 direction;

    public int health;
    [HideInInspector]
    public string elementalName;

    // Visual effects
    public GameObject elementalShadow;
    public GameObject explosionParticles;
    private Shake shake;

    [Header(header: "Shooting: (Cowboy only)")]
    public GameObject bulletPrefab;
    public GameObject shootPoint;

    private void Start()
    {
        moveSpeed = Random.Range(moveSpeed - 0.25f, moveSpeed + 0.25f); // Random speed 
        var randomScaleVal = Random.Range(0.85f, 1.2f);
        transform.localScale = new Vector3(randomScaleVal, randomScaleVal, transform.localScale.z); // Random scale   

        shake = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Shake>();

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody.gravityScale = 0f;

        // Script sets to players transform variables values
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
                elementalName = "Water";
                break;
            case 1:
                elementalShadow.GetComponent<SpriteRenderer>().color = fireColor;
                elementalName = "Fire";
                break;
            default:
                break;
        }

        if (MonsterName.Equals("Cowboy"))
        {
            InvokeRepeating("Shoot", 1.5f, 3.5f);
        }
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

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        shake.CamShake();
        if (health <= 0)
        {    
            Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
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
        for(int i = 1; i < numberOfPlayers; ++i) // Script select closer target
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
