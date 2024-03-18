using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gore : MonoBehaviour
{
    [SerializeField] public SpriteRenderer goreSprite;

    public float minImpulseForce = 3.0f;
    public float maxImpulseForce = 5.0f;

    private void Start()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomImpulse = Random.Range(minImpulseForce, maxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(randomDirection * randomImpulse, ForceMode2D.Impulse);
        Destroy(gameObject, 10f);
    }
}
