using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryController : MonoBehaviour
{
    public AudioSource storyAudio;
    [SerializeField] private List<string> messages;
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timePerLine;

    // Start is called before the first frame update
    void Start()
    {
        storyAudio = GetComponent<AudioSource>();

        PrintText(messages);
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
}
