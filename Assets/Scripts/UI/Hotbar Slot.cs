using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Item item;
    public GameObject MouseSlot;
    public int slotnum;
    public Inventory mainSlot;

    [Header("UI")]
    public GameObject slot;
    public Image slotImage;
    public Sprite[] Highlight;

    private void Start()
    {
        item = mainSlot.hotBar[slotnum];
        InitialiseItem(item);
    }

    private void Update()
    {
        if (mainSlot.current == slotnum && slotImage != Highlight[1])
            slotImage.sprite = Highlight[1];
        else if (mainSlot.current != slotnum && slotImage != Highlight[0])
            slotImage.sprite = Highlight[0];
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

    public void updateSlot()
    {
        item = mainSlot.hotBar[slotnum];
        InitialiseItem(item);
    }
}
