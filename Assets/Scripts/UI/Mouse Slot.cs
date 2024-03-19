using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using TMPro;

public class MouseSlot : MonoBehaviour
{
    public Item item;
    public int numberofItem;
    public GameObject numpad;

    [Header("UI")]
    public Image imange;

    [ContextMenu("InitialiseItem")]
    private void Start()
    {
        numberofItem = 0;
        InitialiseItem(item);
    }

    public void InitialiseItem(Item newItem)
    {
        imange.sprite = newItem.image;
        if (newItem.type == ItemType.Block || newItem.type == ItemType.Torch)
            gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        else
            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
        numChange();
    }
    
    public void ChangeItem(Item newItem, int num)
    {
        item = newItem;
        numberofItem = num;
        InitialiseItem(newItem);
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
}
