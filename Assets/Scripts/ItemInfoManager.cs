using UnityEngine;
using UnityEngine.UI;

public class ItemInfoManager : MonoBehaviour
{
    [SerializeField] GameObject name;
    [SerializeField] GameObject image;
    [SerializeField] GameObject num;

    [SerializeField] ItemData itemData;

    public int memberNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetItemName(itemData.itemList[memberNum].itemName);
        SetItemNum(itemData.itemList[memberNum].itemNum);
    }

    public void SetItemName(string ItemName)
    {//アイテム名セット
        name.GetComponent<Text>().text = ItemName;
    }

    private void SetItemImage(Texture2D ItemTexture)
    {//アイテムイメージセット
    }

    private void SetItemNum(int ItemNum)
    {//アイテム数セット
        num.GetComponent<Text>().text = "x" + ItemNum.ToString();
    }
}
