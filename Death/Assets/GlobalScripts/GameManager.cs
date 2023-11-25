using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField] private TextMeshProUGUI healthPointsText;
    [SerializeField] private Slider healthPointsBar;
    [SerializeField] private Slider dashCooldownBar;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        UpdateHealthPoints(100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealthPoints(int healthPoints)
    {
        healthPointsBar.value = (float)healthPoints / 100;
    }

    public void UpdateDashCooldown(float percent)
    {
        dashCooldownBar.value = percent;
    }
}
