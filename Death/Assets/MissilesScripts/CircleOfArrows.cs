using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOfArrows : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private GameObject circleDamageObject;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 randomChange = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
        transform.position = player.transform.position + randomChange;
        sr = gameObject.GetComponent<SpriteRenderer>();
        Invoke("spawn", 1);
        Destroy(gameObject, 1.7f);
    }

    // Update is called once per frame
    void Update()
    {
        sr.color += new Color(0, 0, 0, Time.deltaTime);
    }

    void spawn()
    {
        GameObject.Instantiate(circleDamageObject, transform.position, Quaternion.identity);
    }
}