using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public GameObject FullInventoryPanel;
    public GameObject ShrinkInventoryPanel;
    bool activeInventory = false;

    private void Start()
    {
        activeInventory = false;
        FullInventoryPanel.SetActive(activeInventory);
        ShrinkInventoryPanel.SetActive(!activeInventory);
    }

    private void Update()
    {
        
    }

    private void OnInventory(InputValue value)
    {
        activeInventory = !activeInventory;
        FullInventoryPanel.SetActive(activeInventory);
        ShrinkInventoryPanel.SetActive(!activeInventory);
    }
}
