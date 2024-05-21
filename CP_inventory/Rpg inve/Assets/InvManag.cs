using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class InvManag : MonoBehaviour
{
    private string inventorySavePath = "inv.json";
    public int VerticCellCount = 9;
    public int HoriCellCount = 9;
  
    public List<InvCell> invCells = new List<InvCell>(); // Инициализация списка invCells
  
    private InvCell _invCell = new InvCell();
    public InvCell ItemPref;
    public Transform Content;
    public Item TestItem;
    public Item Test2;
    public static InvManag Instance;
    public InvCell EnteredCell;
    public InvCell DraggingCell;
    public bool IsDragg;
    public Image MouseItemHand;
    [SerializeField] private unfo _Unfo;

    public void SaveInventory()
    {
        List<JsonCell> _JsonCells = new List<JsonCell>();
        for (var i = 0; i < invCells.Count; i++)
        {
            var Cell = invCells[i];
            if (Cell.curritem != null)
            {
                JsonCell jsonCell = new JsonCell();
                jsonCell.NumberOfCell = i;
                jsonCell.item = Cell.curritem;
                _JsonCells.Add(jsonCell);
            }
        }

        string json = JsonUtility.ToJson(new JsonData(){ invCells = _JsonCells}); 
        File.WriteAllText(inventorySavePath, json); 
    }

    public void UpdateCell()
    {
        for (int i = 0; i < invCells.Count; i++)
        {
            invCells[i].Updatecell(); // Метод Updatecell, обновляющий ячейку
        }
    }
    public void LoadInventory()
    {
        if (File.Exists(inventorySavePath))
        {
            string json = File.ReadAllText(inventorySavePath); 
            List<JsonCell> Data = JsonUtility.FromJson<JsonData>(json).invCells; 
            foreach (var loadedCell in Data)
            {
                invCells[loadedCell.NumberOfCell].curritem = loadedCell.item;
            }
            UpdateCell(); 
        }
    }

    public void Save()
    {
        SaveInventory();
    }

    private void Start()
    {
        FillInv();
    }

    private void Awake()
    {
        Instance = this;
        FillInv();
        LoadInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Tuple<bool, int> tuple = getfreecell();
            if (tuple.Item1)
            {
                invCells[tuple.Item2].curritem = TestItem;
                UpdateCell();
            }
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
            InvCell cell = Instantiate(ItemPref, Content);
            invCells.Add(cell);
            cell._Unfo = _Unfo;
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

        Debug.LogError("there are no free cells");
        return Tuple.Create(false, 0);
    }
}

public class JsonData
{
    public List<JsonCell> invCells;
}

[Serializable]
public class JsonCell
{
    public int NumberOfCell;
    public Item item;
}