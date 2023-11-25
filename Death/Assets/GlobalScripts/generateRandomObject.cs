using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateRandomObject : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnObject = gameObjects[Random.Range(0, gameObjects.Length)];
        GameObject.Instantiate(spawnObject, new Vector3(transform.position.x, transform.position.y, spawnObject.transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
