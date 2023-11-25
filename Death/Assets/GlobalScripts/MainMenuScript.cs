using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject controlsPanel;

    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button closeControlsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button closeCreditsButton;
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(Play);

        controlsButton.onClick.AddListener(OpenControls);
        closeControlsButton.onClick.AddListener(CloseControls);

        creditsButton.onClick.AddListener(OpenCredits);
        closeCreditsButton.onClick.AddListener(CloseCredits);

        exitButton.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenControls()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    void CloseControls()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    void OpenCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    void CloseCredits()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    void Play()
    {
        SceneManager.LoadScene("gameplay");
    }
}
