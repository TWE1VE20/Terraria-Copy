using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class MouseSlot : MonoBehaviour
{
    public Item item;

    [Header("UI")]
    public Image imange;

    [ContextMenu("InitialiseItem")]
    private void Start()
    {
        InitialiseItem(item);
    }

    public void InitialiseItem(Item newItem)
    {
        imange.sprite = newItem.image;
        if (newItem.type == ItemType.Block)
            gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        else
            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
    }
    
    public void ChangeItem(Item newItem)
    {
        item = newItem;
        InitialiseItem(newItem);
    }
}
