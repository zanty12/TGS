using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public List<Data> itemList = new List<Data>();
}

[System.Serializable]
public class Data
{
    public string itemName;
    public int itemNum;
}