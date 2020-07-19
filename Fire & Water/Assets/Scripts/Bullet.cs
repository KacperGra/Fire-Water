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
    private const float timeToSpawnEffect = 0.05f;
    private float currentTimeToSpawnEffect = 0f;
    // Explosion particles
    private float currentLifeTime = 0f;
    private const float lifeTime = 0.8f;
    public GameObject destroyParticles;

    private void Start()
    {
        var bulletLayer = LayerMask.NameToLayer("Bullet");
        var playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(bulletLayer, playerLayer);
    }

    private void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > lifeTime)
        {
            Explosion();
        }
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
        currentTimeToSpawnEffect += Time.deltaTime;
        if (currentTimeToSpawnEffect > timeToSpawnEffect)
        {
            Instantiate(particles, transform.position, transform.rotation);
            currentTimeToSpawnEffect = 0f;
        }
    }
}
