using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicelineManager : MonoBehaviour
{
    public AudioClip[] voiceLines;
    public float audioDelayMin;
    public float audioDelayMax;
    private float randDelay;
    private float timer;
    private int randLine;
    private int lastLine;
    private AudioSource chosenLine;    // Start is called before the first frame update
    void Start()
    {
        randDelay = Random.Range(audioDelayMin, audioDelayMax);
        Debug.Log("Audio should play after " + randDelay);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("Audio Timer: " + timer);
        while(randLine == lastLine)
        {
            randLine = Random.Range(0, voiceLines.Length);
            Debug.Log("Line chosen");
        }
        chosenLine.clip = voiceLines[randLine];
        if (timer >= randDelay)
        {
            chosenLine.Play();
            timer = 0;
            randDelay = Random.Range(audioDelayMin, audioDelayMax);
            Debug.Log("Audio Played");
        }
    }
}
