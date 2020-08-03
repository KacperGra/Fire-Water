using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject pickParticles;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            Instantiate(pickParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
