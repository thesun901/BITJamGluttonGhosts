using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorCircleOfArrows : MonoBehaviour
{
    [SerializeField] private int damage;
    private float timer = 0;
    [SerializeField] GameObject cosmeticArrow;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.07f)
        {
            Vector3 addedRandom = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0.5f, 3), 0);
            GameObject.Instantiate(cosmeticArrow, transform.position + addedRandom, Quaternion.identity);
            timer = 0;
        }
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
