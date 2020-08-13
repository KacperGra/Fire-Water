using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string bulletName;
    [Header(header:"Movement")]
    private float moveSpeed = 15f;
    [HideInInspector] public float timeToDestroy = .75f;
    public int health;

    [Header(header:"Particles")]
    // Fly particles
    public GameObject particles;
    // Explosion particles
    public GameObject destroyParticles;

    private void Start()
    {
        if(bulletName.Equals("Cowboy"))
        {
            moveSpeed = 7.5f;
        }
        InvokeRepeating("FlyParticles", 0f, .25f);

        Invoke("Explosion", timeToDestroy);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!bulletName.Equals("Cowboy"))
        {

            Monster monster = collision.GetComponent<Monster>();
            if (monster != null && monster.IsDead.Equals(false))
            {
                if ((monster.elementalName.Equals("Water") && bulletName.Equals("Water")) || (monster.elementalName.Equals("Fire") && bulletName.Equals("Fire")) || monster.elementalName == "Both")
                {
                    TakeDamage(1);
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
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Explosion();
                player.TakeDamage();
            }
        }
    }

    void TakeDamage(int _value)
    {
        health -= _value;
        if(health <= 0)
        {
            Explosion();
        }
    }

    void DecraseMovementSpeed()
    {
        if (moveSpeed > 8f && !bulletName.Equals("Cowboy"))
        {
            moveSpeed *= .99f;
        }
        else if(moveSpeed > 3f && bulletName.Equals("Cowboy"))
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
