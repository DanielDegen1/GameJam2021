using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    private int chosenPoint;
    private int lastPoint;
    public void Start()
    {
        Debug.Log("There are currently " + spawnPoints.Length + " defined spawn points");

    }
    public Vector3 respawnPlayer()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        while (lastPoint == chosenPoint)
        {
            chosenPoint = Random.Range(0, spawnPoints.Length);
        }
        lastPoint = chosenPoint;
        Debug.Log("Respawning Player at " + spawnPoints[chosenPoint]);
        return spawnPoints[chosenPoint].transform.position;
    }
}
