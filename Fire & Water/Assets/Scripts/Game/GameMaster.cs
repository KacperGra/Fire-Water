using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool testMode;
    public bool androidMode;
    [Header("UI")]
    public Text coinNumberText;
    [HideInInspector] public int coinsNumber;
    public Text monsterNumberText;
    [HideInInspector] public int monsterNumber;
    public Text killedMonstersNumberText;
    [HideInInspector] public int killedMonstersNumber;

    public GameObject shopWindow;

    private void Start()
    {
        coinsNumber = 0;
        monsterNumber = 0;
        killedMonstersNumber = 0;
        IgnoreCollisionsInit();
        shopWindow.SetActive(false);
    }

    private void Update()
    {
        coinNumberText.text = coinsNumber.ToString();
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

    public void PickCoin(int _value)
    {
        coinsNumber += _value;
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
        SceneManager.LoadScene((int)ScenesIndex.MAIN_MENU);
    }
}
