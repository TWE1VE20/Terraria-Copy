using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] public Item item;
    [SerializeField] public LayerMask layermask;

    public void Start()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer  >();
        sprite.sprite = item.image;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(layermask.Contain(collision.gameObject.layer))
        {
            Debug.Log("Take");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
