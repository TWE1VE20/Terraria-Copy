using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public GameObject MouseSlot;
    public int slotnum;
    public Inventory mainSlot;
    public int numberofItem;
    public GameObject numpad;

    [Header("UI")]
    public GameObject slot;

    [ContextMenu("InitialiseItem")]
    private void Start()
    {
        if (slotnum <= 9)
            item = mainSlot.hotBar[slotnum];
        else
            item = mainSlot.inventorySlot[slotnum-10];
        numberofItem = mainSlot.numberOfItem[slotnum];
        InitialiseItem(item);
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

    public void Exchange()
    {
        MouseSlot holditem = MouseSlot.GetComponent<MouseSlot>();

        // ������ ��ȯ
        Item temp = item;
        item = holditem.item;

        // ���� ��ȯ
        int bufnum = numberofItem;
        numberofItem = holditem.numberofItem;
        mainSlot.numberOfItem[slotnum] = holditem.numberofItem;

        holditem.ChangeItem(temp, bufnum);
        if (slotnum <= 9)
            mainSlot.hotBar[slotnum] = item;
        else
            mainSlot.inventorySlot[slotnum - 10] = item;

        InitialiseItem(item);
        Debug.Log($"Exchange {slotnum}");
    }

    // ������ ��������Ʈ ������Ʈ
    public void updateSlot()
    {
        item = mainSlot.hotBar[slotnum];
        InitialiseItem(item);
    }

    // ������ ���� ����
    public void numChange()
    {
        if (item.stackable)
        {
            if(numberofItem != 0)
                numpad.SetActive(true);
            numpad.GetComponent<TextMeshProUGUI>().text = numberofItem.ToString();
        }
        else
        {
            numpad.SetActive(false);
            numpad.GetComponent<TextMeshProUGUI>().text = "0";
        }

    }
}
