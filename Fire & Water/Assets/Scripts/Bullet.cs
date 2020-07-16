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

    private void Update()
    {
        var screenBounds = FindObjectOfType<GameMaster>().screenBounds;
        if (transform.position.x < screenBounds.x - 1 || transform.position.x > screenBounds.y + 1)
        {
            Destroy(gameObject);
        }

        if(moveSpeed > 5)
        {
            moveSpeed *= 0.985f;
        }  
    }

    private void FixedUpdate()
    {
        currentTimeToSpawnEffect += Time.fixedDeltaTime;
        if(currentTimeToSpawnEffect > timeToSpawnEffect)
        {
            Instantiate(particles, transform.position, transform.rotation);
            currentTimeToSpawnEffect = 0f;
        }
        
        rigidbody.velocity = transform.right * moveSpeed;
    }
}
