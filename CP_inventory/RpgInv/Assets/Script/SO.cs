using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InvMenu", menuName = "scriptableobject/new Item", order = 1)]
public class Item : ScriptableObject
{
    
    [FormerlySerializedAs("Title")] public string title;
    public string description; 
    public string Damage;
    public string Rare;
    
}
