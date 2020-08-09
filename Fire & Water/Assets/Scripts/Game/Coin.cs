using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject pickParticles;
    public Animator coinAnimator;
    private float coinLifetime;

    private int coinValue;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        StartCoroutine(StartAnimation());

        coinLifetime = 15f;
        Invoke("ExplosionOnDestroy", coinLifetime);
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1f);
        coinAnimator.SetTrigger("IntroEnd");
    }

    public void SetCoinValue(int _value)
    {
        coinValue = _value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            FindObjectOfType<GameMaster>().PickCoin(coinValue);
            
            ExplosionOnDestroy(); // <- Destroy
        }
    }

    public void ExplosionOnDestroy()
    {
        Instantiate(pickParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
