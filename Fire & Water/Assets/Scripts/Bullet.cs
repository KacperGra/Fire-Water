using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [Header(header:"Movement")]
    public float moveSpeed;
    public new Rigidbody2D rigidbody;
    [HideInInspector]
    public Sprite image;

    [Header(header:"Particles")]
    public GameObject particles;
    public float timeToSpawnEffect;
    private float currentTimeToSpawnEffect = 0f;

    // LifeTime
    private float currentLifeTime = 0f;
    public float lifeTime;
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
        currentLifeTime += Time.fixedDeltaTime;
        if(currentLifeTime > lifeTime)
        {
            Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        currentTimeToSpawnEffect += Time.fixedDeltaTime;
        if(currentTimeToSpawnEffect > timeToSpawnEffect)
        {
            Instantiate(particles, transform.position, transform.rotation);
            currentTimeToSpawnEffect = 0f;
        }
        
        rigidbody.velocity = transform.right * moveSpeed;
    }
}
