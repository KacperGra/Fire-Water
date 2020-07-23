using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string elementalName;
    [Header(header:"Movement")]
    private float moveSpeed = 15f;
    public new Rigidbody2D rigidbody;

    [Header(header:"Particles")]
    // Fly particles
    public GameObject particles;
    // Explosion particles
    public GameObject destroyParticles;

    private void Start()
    {
        InvokeRepeating("FlyParticles", 0f, 0.05f);
        Invoke("Explosion", 0.8f);
    }

    private void Update()
    {
        FlyParticles();
        DecraseMovementSpeed();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Monster monster = collision.collider.GetComponent<Monster>();
        
        if(monster != null)
        {
            Explosion();
            if ((monster.elementalName.Equals("Water") && elementalName.Equals("Water")) || (monster.elementalName.Equals("Fire") && elementalName.Equals("Fire"))) 
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

    void DecraseMovementSpeed()
    {
        if (moveSpeed > 8f)
        {
            moveSpeed *= 0.99f;
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
