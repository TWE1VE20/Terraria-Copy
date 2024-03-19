using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] public Item item;
    [SerializeField] public int numberOf;
    [SerializeField] public LayerMask layermask;
    [SerializeField] public InventoryManager invenManager;

    private bool touching;

    public void Start()
    {
        touching = false;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = item.image;
    }


    public void Update()
    {
        if (touching)
            if (invenManager.LootItem(item, numberOf))
                Destroy(gameObject);
    }

    Collider2D[] colliders = new Collider2D[20];
    private void FindTarget()
    {
        int size = Physics2D.OverlapCircleNonAlloc(transform.position, range, colliders, layermask);
        for (int i = 0; i < size; i++)
        {
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(layermask.Contain(collision.gameObject.layer))
        {
            touching = true;
            Debug.Log("Take Item");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (layermask.Contain(collision.gameObject.layer))
            touching = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
