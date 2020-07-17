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
        if(moveSpeed > 8f)
        {
            moveSpeed *= 0.99f;
        }
        
    }

    private void FixedUpdate()
    {
        currentLifeTime += Time.fixedDeltaTime;
        if (currentLifeTime > lifeTime)
        {
            ExplosionParticles();
        }
        
        FlyParticles();
 
        rigidbody.velocity = transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Monster monster = collision.collider.GetComponent<Monster>();
        if (collision.collider.CompareTag("Monster"))
        {
            if(monster != null)
            {
                ExplosionParticles();
                if ((monster.elementalName.Equals("Water") && elementalName.Equals("Water")) || (monster.elementalName.Equals("Fire") && elementalName.Equals("Fire"))) 
                {
                    monster.TakeDamage(1);
                }
                monster.transform.position -= new Vector3(0.2f, 0f, 0f);
            }
        }
    }

    private void ExplosionParticles()
    {  
        Instantiate(destroyParticles, transform.position, transform.rotation);
        Destroy(gameObject);      
    }

    private void FlyParticles()
    {
        currentTimeToSpawnEffect += Time.fixedDeltaTime;
        if (currentTimeToSpawnEffect > timeToSpawnEffect)
        {
            Instantiate(particles, transform.position, transform.rotation);
            currentTimeToSpawnEffect = 0f;
        }
    }
}
