using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Skill : MonoBehaviour
{
    public Item item;

    public Text nameText;
    public Image image;
    public Text costText;

    private void Start()
    {
        nameText.text = item.itemName;
        image.sprite = item.image;
        var buyText = "Buy: ";
        costText.text = buyText + item.cost.ToString();
    }
}
