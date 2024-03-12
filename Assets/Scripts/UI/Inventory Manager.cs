using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public GameObject FullInventoryPanel;
    public GameObject ShrinkInventoryPanel;

    [Header("Inventory Managemant")]
    public Item[] hotBar;
    public int[] numberOfItem;
    public Item[] inventorySlot;

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
        for (int i = 0; i < 50; i++)
        {
            if (numberOfItem[i] == 0)
                return i;
            if (i < 10 && (hotBar[i] == lootitem && numberOfItem[i] < 999))
                return i;
            if (i >= 10 && (hotBar[i] == lootitem && numberOfItem[i] < 999))
                return i;
        }
        return -1;
    }

    public bool LootItem(Item item, int num)
    {
        int slot = EmptySlot(item);
        if (slot != -1)
            return false;

    }
}
