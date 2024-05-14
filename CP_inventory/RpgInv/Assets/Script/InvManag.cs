using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvManag : MonoBehaviour
{
    public Item[] InvItems;
    public ItemTemplate[] InvPanel;

    private void Start()
    {
        for (int i = 0; i < InvItems.Length; i++)
        {
            InvPanel[i].gameObject.SetActive(true);
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < InvItems.Length; i++)
        {
            InvPanel[i].titleText.text = InvItems[i].title;
            InvPanel[i].descrText.text = InvItems[i].description;
            InvPanel[i].RareText.text = InvItems[i].Rare;
            InvPanel[i].damageText.text = InvItems[i].Damage;
        }
    }
}
