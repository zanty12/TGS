using UnityEngine;

public class ItemUiManager : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject parent;

    [SerializeField] ItemData itemData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < itemData.itemList.Count;i++)
        {
            gameObject.GetComponent<ItemInfoManager>().memberNum = i;
            Instantiate(gameObject, parent.transform);
        }
    }
}
