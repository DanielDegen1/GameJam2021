using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float rotationRate = 45;
    public float respawnTimer = 5.0f;
    private Vector3 StartPos;
    private float timer;
    public Vector3 respawnPos = new Vector3(-1000, -1000, -1000);

    [HideInInspector]
    public bool pickedUp = false;
    public bool objectNotMoved = false;



    // Start is called before the first frame update
    void Start()
    {
        StartPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationRate/2, rotationRate, 0) * Time.deltaTime);
        if(pickedUp == true)
        {
            moveObject();
        }
    }

    void moveObject()
    {
        if (objectNotMoved == true)
        {
            gameObject.transform.position = respawnPos;
            objectNotMoved = false;
        }
        timer += Time.deltaTime;
        if (timer >= respawnTimer)
        {
            gameObject.transform.position = StartPos;
            pickedUp = false;
            timer = 0;
        }
    }
}
