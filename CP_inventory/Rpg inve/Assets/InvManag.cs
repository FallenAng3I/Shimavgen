using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvManag : MonoBehaviour
{
  public int VerticCellCount = 9;
  public int HoriCellCount = 9;

  private InvCell _invCell = new InvCell();

  [FormerlySerializedAs("InvCells")] public List<InvCell> invCells = new List<InvCell>();
  public GameObject ItemPref;
  public Transform Content;
  [Header("debug")] public Item TestItem;
  public Item Test2;
  public static InvManag Instance;
  public InvCell EnteredCell;
  public InvCell DraggingCell;
  public bool IsDragg;
  public Image MouseItemHand;
  private void Start()
  {
    FillInv();
  }

  private void Awake()
  {
    Instance = this;
    
    FillInv();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      Tuple<bool, int> tuple = getfreecell();
      if (tuple.Item1)
      {
        invCells[tuple.Item2].curritem = TestItem;
        UpdateCell();
      }
    }

    if (IsDragg && DraggingCell.curritem)
    {
      MouseItemHand.sprite = DraggingCell.curritem.Icon;
      MouseItemHand.color = Color.white;
      MouseItemHand.rectTransform.position = Input.mousePosition;
    }
    else
    {
      MouseItemHand.color = Color.clear;
    }
    if (Input.GetKeyDown(KeyCode.Q))
    {
      Tuple<bool, int> tuple = getfreecell();
      if (tuple.Item1)
      {
        invCells[tuple.Item2].curritem = Test2;
        UpdateCell();
      }
    }
  }

  public void FillInv()
  {
    for (int i = 0; i < VerticCellCount * HoriCellCount; i++)
    {
      InvCell cell = Instantiate(ItemPref, Content).GetComponent<InvCell>();
      invCells.Add(cell);

      // Добавляем обработчики событий наведения и ухода мыши
      var eventTrigger = cell.gameObject.GetComponent<EventTrigger>();
      if (eventTrigger == null)
      {
        eventTrigger = cell.gameObject.AddComponent<EventTrigger>();
      }

      var pointerEnter = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
      pointerEnter.callback.AddListener((data) => { _invCell.OnPointerEnter(null); });
      eventTrigger.triggers.Add(pointerEnter);

      var pointerExit = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
      pointerExit.callback.AddListener((data) => { _invCell.OnPointerExit(null); });
      eventTrigger.triggers.Add(pointerExit);
    }
  }





  public Tuple<bool, int> getfreecell()
  {
    for (int i = 0; i < invCells.Count; i++)
    {
      if (invCells[i].curritem == null)
      {
        return Tuple.Create(true, i);
      }
    }
    Debug.LogError("there no any freeCells");
    return Tuple.Create(false, 0);
  }

  public void AddItem(int cellId, Item item)
  {
    invCells[cellId].curritem = item;
  }

  public void UpdateCell()
  {
    for (int i = 0; i < invCells.Count; i++)
    {
      invCells[i].Updatecell();
    }
  }
}
