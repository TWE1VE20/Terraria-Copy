using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject FullInventoryPanel;
    public GameObject ShrinkInventoryPanel;

    [Header("Inventory Managemant")]
    // 인벤토리를 전부 읽어오는  Array를 추가했기에 추후 이에 연결하는 작업 필요
    public Item[] hotBar;
    public int[] numberOfItem;
    public Item[] inventorySlot;

    public InventorySlot[] inventorySlots;

    [Header("Main Slots")]
    public HotbarSlot[] mainInventory;

    [Header("Current Slot")]
    public int current;
    
    private bool activeInventory = false;

    private void Start()
    {
        activeInventory = false;
        FullInventoryPanel.SetActive(activeInventory);
        for (int i = 0; i <= 9; i++)
            mainInventory[i].updateSlot();
        ShrinkInventoryPanel.SetActive(!activeInventory);
    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                current = i-1;
    }

    // ESC가 눌렸을때 인벤토리 변경
    private void OnInventory(InputValue value)
    {
        activeInventory = !activeInventory;

        FullInventoryPanel.SetActive(activeInventory);
        for (int i = 0; i <= 9; i++)
            mainInventory[i].updateSlot();
        ShrinkInventoryPanel.SetActive(!activeInventory);
    }

    private void OnScroll(InputValue value)
    {
        if (!activeInventory)
        {
            Vector2 scroll = value.Get<Vector2>();
            current = Mathf.Abs(((int)scroll.y / 120) + current + 10) % 10;
        }
    }

    private int EmptySlot(Item lootitem)
    {
        int firstEmptyslot = -1;
        for (int i = 0; i < 50; i++)
        {
            if (numberOfItem[i] == 0)
            {
                if(firstEmptyslot == -1)
                    firstEmptyslot = i;
                continue;
            }
            if (lootitem.stackable)
            {
                if (i < 10 && (hotBar[i] == lootitem && numberOfItem[i] < 999))
                    return i;
                if (i >= 10 && (inventorySlot[i-10] == lootitem && numberOfItem[i] < 999))
                    return i;
            }
        }
        if (firstEmptyslot == -1)
            return -1;
        else
            return firstEmptyslot;
    }

    public bool LootItem(Item item, int num)
    {
        Debug.Log("Looting");
        int slot = EmptySlot(item);
        Debug.Log($"putting in slot {slot}");
        if (slot == -1)
        {
            Debug.Log("Looting failed");
            return false;
        }
        else
        {
            inventorySlots[slot].ItemInsert(item, num);
        }
        Debug.Log("Looting End");
        return true;
    }
}
