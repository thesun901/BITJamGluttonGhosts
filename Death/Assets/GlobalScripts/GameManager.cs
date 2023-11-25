using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField] private TextMeshProUGUI healthPointsText;
    [SerializeField] private Slider dashCooldownBar;

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

    public void UpdateDashCooldown(float percent)
    {
        dashCooldownBar.value = percent;
    }
}
