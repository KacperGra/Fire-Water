using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Sprite[] heartSprites = new Sprite[3];
    public Image[] hearts = new Image[3];
    public Player player;
    

    private void Start()
    {
    }

    private void Update()
    {
        setHeartSprites();
    }

    void setHeartSprites()
    {
        var full = heartSprites[0];
        var half = heartSprites[1];
        var empty = heartSprites[2];

        // Hearts full, half, empty display script
        for (int i = 0; i < 3; ++i)
        {
            hearts[i].sprite = empty;
        }
        var healthValueDivided = player.health / 2;
        for (int i = player.health / 2 - 1; i >= 0; --i)
        {
            hearts[i].sprite = full;
        }
        if (player.health % 2 == 1)
        {
            hearts[healthValueDivided].sprite = half;
        }
    }
}
