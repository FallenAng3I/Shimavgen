using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InvCell : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Item curritem;
    public Image Image;
    public bool IsDag;
    public bool isEnd;
    
    private void Start()
    {
        Image = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)&& isEnd)
        {
            RemoveItem();
        }
    }

    public void Updatecell()
    {
        if (curritem && curritem.Icon)
        {
            Image.sprite = curritem.Icon;
            Image.color=Color.white;
        }
        else
        {
            Image.sprite = null;
            Image.color = Color.clear;
        }
    }

    public void RemoveItem()
    {
        curritem = null;
        Updatecell();
    }
    public void OnDrag(PointerEventData eventData)
    {
      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDag = true;
        InvManag.Instance.IsDragg = true;
        InvManag.Instance.DraggingCell = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
 
        if (IsDag && InvManag.Instance.EnteredCell)
        {
            (InvManag.Instance.EnteredCell.curritem, curritem) = (curritem, InvManag.Instance.EnteredCell.curritem);
            InvManag.Instance.DraggingCell = null;
            InvManag.Instance.IsDragg = false;
            InvManag.Instance.EnteredCell = null;
            IsDag = false;
        }
        else if (IsDag)
        {
            InvManag.Instance.DraggingCell = null;
            InvManag.Instance.IsDragg = false;
            InvManag.Instance.EnteredCell = null;
            IsDag = false;
        }
        InvManag.Instance.UpdateCell();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnd = true;
        InvManag.Instance.EnteredCell = this;
        if (curritem != null)
        {
            // Отображение информации о предмете, например, всплывающего окна с названием и описанием предмета
            Debug.Log("Название: " + curritem.Name + ", Описание: " + curritem.Description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnd = false;
        InvManag.Instance.EnteredCell = null;
        Debug.Log("Скрыть информацию о предмете");
    }
}
