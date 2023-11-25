using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMelee : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private int damage;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }


    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.OnHit(damage);
        }
    }
}
