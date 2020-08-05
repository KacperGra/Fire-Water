using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject pickParticles;
    public Animator coinAnimator;
    public SpriteRenderer coinSprite;
    public SpriteRenderer LightSpirte;
    private float coinLifetime;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        StartCoroutine(StartAnimation());

        coinLifetime = 15f;
        Invoke("Explosion", coinLifetime);
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(.834f);
        coinAnimator.SetTrigger("IntroEnd");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            FindObjectOfType<GameMaster>().PickCoin();

            
            Explosion(); // <- Destroy
        }
    }

    public void Explosion()
    {
        Instantiate(pickParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
