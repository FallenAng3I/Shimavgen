using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class unfo : MonoBehaviour
{

    public Text itemNameText;
    public Text itemDescriptionText;
    
    public GameObject itemInfoPanel;

    public void ShowItemInfo(Item item)
    {
        itemNameText.text = item.Name;
        itemDescriptionText.text = item.Description;
       
        itemInfoPanel.SetActive(true);
    }

    public void HideItemInfo()
    {
        itemNameText.text = "";
        itemDescriptionText.text = "";
        itemInfoPanel.SetActive(false);
    }
}

