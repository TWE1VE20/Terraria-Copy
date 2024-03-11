using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public GameObject MouseSlot;
    public int slotnum;
    public Inventory mainSlot;

    [Header("UI")]
    public GameObject slot;

    [ContextMenu("InitialiseItem")]
    private void Start()
    {
        if (slotnum <= 9)
            item = mainSlot.hotBar[slotnum];
        InitialiseItem(item);
    }

    public void InitialiseItem(Item newItem)
    {
        Image image = slot.GetComponent<Image>();
        image.sprite = newItem.image;
        if (newItem.type == ItemType.Block)
            slot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else
            slot.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }

    public void Exchange()
    {
        MouseSlot holditem = MouseSlot.GetComponent<MouseSlot>();
        Item temp = item;
        item = holditem.item;
        InitialiseItem(holditem.item);
        holditem.ChangeItem(temp);
        Debug.Log("Exchange");
        if(slotnum <= 9)
            mainSlot.hotBar[slotnum] = item;
    }

    public void updateSlot()
    {
        item = mainSlot.hotBar[slotnum];
        InitialiseItem(item);
    }
}
