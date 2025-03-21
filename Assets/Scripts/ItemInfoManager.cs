using UnityEngine;
using UnityEngine.UI;

public class ItemInfoManager : MonoBehaviour
{
    [SerializeField] GameObject name;
    [SerializeField] GameObject image;
    [SerializeField] GameObject num;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetItemName("Mirror");
        SetItemNum(5);
    }


    public void SetItemName(string ItemName)
    {//アイテム名セット
        //name = transform.Find("ItemName").gameObject;
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
