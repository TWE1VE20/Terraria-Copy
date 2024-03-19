using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Item item;
    public GameObject MouseSlot;
    public int slotnum;
    public InventoryManager mainSlot;
    public int numberofItem;
    public GameObject numpad;

    [Header("UI")]
    public GameObject slot;
    public Image slotImage;
    public Sprite[] Highlight;

    private void Start()
    {
        item = mainSlot.hotBar[slotnum];
        numberofItem = mainSlot.numberOfItem[slotnum];
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
        if (newItem.type == ItemType.Block || newItem.type == ItemType.Torch)
            slot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else
            slot.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        numChange();
    }

    public void updateSlot()
    {
        item = mainSlot.hotBar[slotnum];
        numberofItem = mainSlot.numberOfItem[slotnum];
        InitialiseItem(item);
    }

    public void numChange()
    {
        if (item.stackable)
        {
            if (numberofItem != 0)
                numpad.SetActive(true);
            numpad.GetComponent<TextMeshProUGUI>().text = numberofItem.ToString();
        }
        else
        {
            numpad.SetActive(false);
            numpad.GetComponent<TextMeshProUGUI>().text = "0";
        }
    }

    public void ChangetoCurrent()
    {
        mainSlot.current = slotnum;
    }
}
