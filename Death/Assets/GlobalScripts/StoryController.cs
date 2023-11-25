using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class StoryController : MonoBehaviour
{
    public AudioSource storyAudio;
    [SerializeField] private List<string> messages;
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timePerLine;

    public GameObject yesButton;
    public GameObject noButton;

    // Start is called before the first frame update
    void Start()
    {
        storyAudio = GetComponent<AudioSource>();

        PrintText(messages);

        if (SceneManager.GetActiveScene().name == "story_play_again")
        {
            StartCoroutine(showButtons());
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintText(List<string> textList)
    {
        StartCoroutine(PrintTextWithDelay(textList));
    }

    IEnumerator PrintTextWithDelay(List<string> textList)
    {
        foreach (string line in textList)
        {
            PrintLine(line);
            yield return new WaitForSeconds(timePerLine);
        }
    }

    void PrintLine(string textLine)
    {
        storyAudio.PlayOneShot(wooshSound);
        text.text = textLine;
    }

    IEnumerator showButtons()
    {
        yield return new WaitForSeconds(6);
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }
}
