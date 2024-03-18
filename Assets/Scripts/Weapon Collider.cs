using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] LayerMask Target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Zombie>().TakeHit(5);
        }
    }
}
