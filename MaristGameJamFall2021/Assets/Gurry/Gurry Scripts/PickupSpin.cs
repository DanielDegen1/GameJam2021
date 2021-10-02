using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpin : MonoBehaviour
{
    public float rotationRate = 45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Pistol")
        {
            transform.Rotate(new Vector3(rotationRate / 2, rotationRate, 0) * Time.deltaTime);
        }
        else if (gameObject.tag == "SMG")
        {
            transform.Rotate(new Vector3(0, rotationRate * -1, 0) * Time.deltaTime);
        }
        else if (gameObject.tag == "Shotgun")
        {
            transform.Rotate(new Vector3(0, rotationRate * -1, 0) * Time.deltaTime);
        }
    }
}
