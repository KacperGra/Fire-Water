using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
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

    private void Update()
    {
        if(moveSpeed > 8f)
        {
            moveSpeed *= 0.99f;
        }  
    }

    private void FixedUpdate()
    {
        ExplosionParticles();
        FlyParticles();
 
        rigidbody.velocity = transform.right * moveSpeed;
    }

    private void ExplosionParticles()
    {
        currentLifeTime += Time.fixedDeltaTime;
        if (currentLifeTime > lifeTime)
        {
            Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
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
