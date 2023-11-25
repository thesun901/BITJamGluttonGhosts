using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    public int lvl;
    public int enemiesAmount;
    public GameObject[] enemiesObjects;
    public float frequency;
    public Transform[] spawnpoints;
    private float timer;
    private int enemiesSpawned;
    [SerializeField] private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.currentLvl == lvl)
        {
            timer += Time.deltaTime;
            if (timer >= frequency && enemiesSpawned < enemiesAmount)
            {
                enemiesSpawned++;
                GameObject.Instantiate(enemiesObjects[Random.Range(0, enemiesObjects.Length)], spawnpoints[Random.Range(0, spawnpoints.Length)]);
                timer = Random.Range(-0.5f, 1f);
            }

            if (gm.killCount >= enemiesAmount)
            {
                gm.swapLvl();
                timer = 0;
            }

            if (gm.killCount >= enemiesAmount - 2 && timer > 20)
            {
                gm.swapLvl();
                timer = 0;
            }
        }
    }
}
