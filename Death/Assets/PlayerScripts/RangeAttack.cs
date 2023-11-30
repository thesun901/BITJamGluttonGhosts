using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    private PlayerController pc;

    private AudioSource daggerAudio;
    [SerializeField] private AudioClip projectileCollisionSound;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if(damagable != null )
        {
            damagable.damage(pc.rangeDamage);
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerAudio.PlayOneShot(projectileCollisionSound);
        }
        Destroy(gameObject, 0.03f);
    }
}
