using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private const int numberOfPlayers = 2;
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

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"));

        shake = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Shake>();

        rigidbody = gameObject.GetComponent<Rigidbody2D>();

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
