using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Shooting : MonoBehaviour
{
    PlayerInput playerInput;
    RaycastHit hit;
    [HideInInspector]
    public Camera cam;
    public bool hasPistol = true;
    [SerializeField]
    private GameObject projectile;
    public Transform sourcePoint;
    public float projectileSpeed = 30f;
    private bool projectileTest = false;


    private Vector3 destination;

    void Start() {
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        if (playerInput.shoot) {
            //TODO different conditionals for each gun type we have 
            if(hasPistol == true)
            {
                pistolShoot();
            }
            if (projectileTest == true)
            {
                InstantiateProjectile(sourcePoint);
            }
        }
    }

    void ShootProjectile() {
        Ray raycast = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) {
            destination = hit.point;
        }
        
            InstantiateProjectile(sourcePoint);
        
    }

    void InstantiateProjectile(Transform firePoint) {
        var projectileObject = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObject.GetComponent<Rigidbody>().velocity = cam.transform.forward * projectileSpeed;
    }
    void pistolShoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("Pistol hit player");
        }
        else if (hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("Pistol hit something besides the player");
        }
        else
        {
            Debug.Log("Pistol missed");
            //missed stasis feedback
        }

    }
}
