using UnityEngine;

public class ItemUiManager : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(gameObject, parent.transform);
    }
}
