using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int cost;
    public Sprite image;
}
