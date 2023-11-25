using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField] private TextMeshProUGUI healthPointsText;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        UpdateHealthPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthPoints()
    {
        healthPointsText.text = "HP: " + playerObject.GetComponent<PlayerController>().healthPoints;
    }
}
