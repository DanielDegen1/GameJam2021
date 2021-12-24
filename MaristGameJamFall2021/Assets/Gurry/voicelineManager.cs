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
    public AudioSource audioManager;    // Start is called before the first frame update
    void Start()
    {
        randDelay = Random.Range(audioDelayMin, audioDelayMax);
        Debug.Log("Audio should play after " + randDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Audio Timer: " + timer);
        if(!audioManager.isPlaying)
        {
            timer += Time.deltaTime;
        }
        while (randLine == lastLine)
        {
            randLine = Random.Range(0, voiceLines.Length);
            Debug.Log("Line chosen was " + voiceLines[randLine].name);
        }
        if (timer >= randDelay)
        {
            if(!audioManager.isPlaying)
            {
                audioManager.clip = voiceLines[randLine];
                audioManager.Play();
                timer = 0;
                randDelay = Random.Range(audioDelayMin, audioDelayMax);
                randLine = Random.Range(0, voiceLines.Length);
                Debug.Log("Audio Played");
            }
            else
            {
                Debug.Log("waiting for audio to stop before playing");
            }
        }
    }
}
