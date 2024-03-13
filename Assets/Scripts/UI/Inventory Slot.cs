using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    

    [Header("Slot info")]
    public Item item;
    public GameObject MouseSlot;
    public int slotnum;
    public InventoryManager mainSlot;
    public int numberofItem;
    public GameObject numpad;

    [Header("UI")]
    public GameObject slot;

    [Header("Prefab")]
    public DropItem dropItemPrefab;
    public Transform player;

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

        // 아이템 교환
        Item temp = item;
        item = holditem.item;

        // 갯수 교환
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

    public void ItemInsert(Item lootitem, int num)
    {
        if (lootitem == item)
        {
            Debug.Log("Same Item");
            if (num + numberofItem <= 999)
            {
                numberofItem += num;
                mainSlot.numberOfItem[slotnum] = numberofItem;
                Debug.Log(numberofItem);
                numChange();
            }
            else
            {
                DropItem dropeditem = Instantiate(dropItemPrefab, player.position, player.rotation);
                dropeditem.item = lootitem;
                dropeditem.numberOf = numberofItem + num - 999;
                numberofItem = 999;
                mainSlot.numberOfItem[slotnum] = 999;
                Debug.Log(numberofItem);
                numChange();
            }
        }
        else
        {
            Debug.Log("Empty Slot");
            numberofItem = num;
            item = lootitem;
            mainSlot.numberOfItem[slotnum] = num;
            if (slotnum <= 9)
            {
                mainSlot.hotBar[slotnum] = lootitem;
                mainSlot.mainInventory[slotnum].updateSlot();
                Debug.Log("Hotbar Slot update");
            }
            else
                mainSlot.inventorySlot[slotnum - 10] = lootitem;
            InitialiseItem(lootitem);
        }
    }

    // 아이템 스프라이트 업데이트
    public void updateSlot()
    {
        item = mainSlot.hotBar[slotnum];
        InitialiseItem(item);
    }

    // 숫자판 숫자 변경
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
