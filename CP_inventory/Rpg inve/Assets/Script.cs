using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "invItem", menuName = "SCO/InvItem", order = 1)]

public class Item : ScriptableObject
{
    public string Name = "Item";
    public Sprite Icon;
    public GameObject prefab;
    public string Description = "Desc";
}
