using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Shooting : MonoBehaviour
{
    PlayerInput playerInput;
    RaycastHit hit;
    public float pistolCD = 0.5f;
    public int pistolClip = 8;
    [HideInInspector]
    public Camera cam;
    public bool hasPistol = true;
    [SerializeField]
    private GameObject projectile;
    public Transform sourcePoint;
    public float projectileSpeed = 30f;
    private bool projectileTest = false;
    private float timeSinceLastShot = 0;
    private bool playerShot = false;
    private Vector3 destination;
    private bool playerCanShoot = true;
    private int currentClip; //TODO update clip to match currently equipped gun
    

    void Start() {
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        currentClip = pistolClip;
    }

    void Update() {
        if(playerShot == true) //conditional that goes through once the player has shot. Acts as a "shot CD" so that the player can't just spam
        {
            timeSinceLastShot += Time.deltaTime;
            if(hasPistol == true && timeSinceLastShot >= pistolCD)
            {
                playerShot = false;
                playerCanShoot = true;
                timeSinceLastShot = 0;
            }
            
            //TODO implement conditional to track if the player can shoot their gun
        }
        if (playerInput.shoot) {
            //TODO different conditionals for each gun type we have 
            if(hasPistol == true && playerCanShoot == true)
            {
                pistolShoot();
            }
            if (projectileTest == true)
            {
                InstantiateProjectile(sourcePoint);
            }
        }
        if (playerInput.Reload || currentClip <= 0)
        {
            Reload();
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
            //TODO cause the affected player to lose health
        }
        else if (hit.collider.gameObject.tag != "Player")
        {
            Debug.Log("Pistol hit something besides the player");
        }
        else
        {
            Debug.Log("Pistol missed");
            //missed stasis feedback
        }
        playerShot = true;
        playerCanShoot = false;
        currentClip--;
    }
    void Reload() //TODO add a delay to the reload
    {
        if (hasPistol == true)
        { 
            Debug.Log("Pistol Reloaded");
            currentClip = pistolClip;
        }
    }
}
