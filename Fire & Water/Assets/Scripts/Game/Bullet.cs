using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string elementalName;
    [Header(header:"Movement")]
    private float moveSpeed = 15f;

    [Header(header:"Particles")]
    // Fly particles
    public GameObject particles;
    // Explosion particles
    public GameObject destroyParticles;

    private void Start()
    {
        if(elementalName.Equals("Cowboy"))
        {
            moveSpeed = 7.5f;
            InvokeRepeating("FlyParticles", 0f, .225f);
        }
        else
        {
            InvokeRepeating("FlyParticles", 0f, .05f);
        }
        
        Invoke("Explosion", .75f);
    }

    private void Update()
    {
        FlyParticles();
        DecraseMovementSpeed();
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!elementalName.Equals("Cowboy"))
        {
            Monster monster = collision.collider.GetComponent<Monster>();
            if (monster != null)
            {
                Explosion();
                if ((monster.elementalName.Equals("Water") && elementalName.Equals("Water")) || (monster.elementalName.Equals("Fire") && elementalName.Equals("Fire")) || monster.elementalName == "Both")
                {
                    monster.TakeDamage(1);
                }
                else
                {
                    monster.TakeDamage(0);
                }
                monster.rigidbody.AddForce(new Vector2(1f, 0f), ForceMode2D.Impulse);
            }
        } 
        else
        {
            Player player = collision.collider.GetComponent<Player>();
            if(player != null)
            {
                Explosion();
                player.TakeDamage();
            }
        }
        
    }

    void DecraseMovementSpeed()
    {
        if (moveSpeed > 8f && !elementalName.Equals("Cowboy"))
        {
            moveSpeed *= .99f;
        }
        else if(moveSpeed > 3f && elementalName.Equals("Cowboy"))
        {
            moveSpeed *= .99f;
        }
    }

    private void Explosion()
    {
        Instantiate(destroyParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void FlyParticles()
    {
        Instantiate(particles, transform.position, transform.rotation);
    }
}
