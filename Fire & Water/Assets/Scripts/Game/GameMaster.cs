using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool testMode;

    public Text coinNumberText;
    [HideInInspector] public int coinNumber;

    private void Start()
    {
        coinNumber = 0;
        IgnoreCollisionsInit();
    }

    private void Update()
    {
        coinNumberText.text = coinNumber.ToString();
    }

    private void IgnoreCollisionsInit()
    {
        // Bullet
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Decoration"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Bullet"));

        // Player
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));

        // Monster
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Decoration"), LayerMask.NameToLayer("Monster"));
    }

    public void PickCoin()
    {
        ++coinNumber;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
