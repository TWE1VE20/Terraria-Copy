using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;

    private void Update()
    {
        
    }

    private void OnClick(InputValue value)
    {
        if (value.isPressed)
            Debug.Log("click");
        if(!value.isPressed)
            Debug.Log("click out");
    }
}
