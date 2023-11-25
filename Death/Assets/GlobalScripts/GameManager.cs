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
    public int currentLvl;
    public int killCount;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private GameObject blackscreen;
    [SerializeField] private Text blackscreenText;
    [SerializeField] private string[] betweenLevelComunicates;
    [SerializeField] private string[] deathComunicates;
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        currentLvl = 1;
        playerObject = GameObject.Find("Player");
        UpdateHealthPoints(100);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyUp(KeyCode.Escape))
        {
            swapLvl();
        }
    }

    public void UpdateHealthPoints(int healthPoints)
    {
        healthPointsBar.value = (float)healthPoints / 100;
    }

    public void UpdateDashCooldown(float percent)
    {
        dashCooldownBar.value = percent;
    }

    public void swapLvl()
    {
        playerObject.transform.position = spawnpoints[currentLvl].position;
        currentLvl++;
        killCount = 0;
        blacksreentextflash(betweenLevelComunicates[Random.Range(0, betweenLevelComunicates.Length - 1)]);
        playerObject.GetComponent<PlayerController>().healthPoints = 100;

        if(currentLvl == 3)
        {
            cam.backgroundColor = Color.black;
        }
    }

    private void blacksreentextflash(string txt)
    {
        blackscreen.SetActive(true);
        blackscreenText.text = txt;
        Invoke("disableBlackscreen", 3);
    }

    private void disableBlackscreen()
    {
        blackscreen.SetActive(false);
    }

}
