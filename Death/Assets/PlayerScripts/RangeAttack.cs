using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }
}
