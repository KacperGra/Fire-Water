using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool testMode;


    [Header("UI")]
    public Text coinNumberText;
    [HideInInspector] public int coinNumber;
    public Text monsterNumberText;
    [HideInInspector] public int monsterNumber;
    public Text killedMonstersNumberText;
    [HideInInspector] public int killedMonstersNumber;

    public GameObject shopWindow;

    private void Start()
    {
        coinNumber = 0;
        monsterNumber = 0;
        killedMonstersNumber = 0;
        IgnoreCollisionsInit();
        shopWindow.SetActive(false);
    }

    private void Update()
    {
        coinNumberText.text = coinNumber.ToString();
        monsterNumberText.text = monsterNumber.ToString();
        killedMonstersNumberText.text = killedMonstersNumber.ToString();
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (shopWindow.activeSelf == false)
            {
                shopWindow.SetActive(true);
            }
            else
            {
                shopWindow.SetActive(false);
            }
        }
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

    public void MonsterDeath()
    { 
        --monsterNumber;
        ++killedMonstersNumber;
    }

    public void MonsterSpawn()
    {
        ++monsterNumber;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
