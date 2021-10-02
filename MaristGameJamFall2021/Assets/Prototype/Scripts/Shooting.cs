using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Shooting : MonoBehaviour
{
    PlayerInput playerInput;
    RaycastHit hit;
    public bool hasPistol = false; //TODO set back to true once SMG and Shotgun are implemented
    public int pistolClip = 8;
    public float pistolCD = 0.5f;
    public float pistolDMG = 1.0f;

    public bool hasSMG = false;
    public int SMGClip = 30;
    public float SMGDMG = 0.2f;

    public bool hasShotgun = true; //TODO set back to false after implementation
    public int shotgunClip = 8;
    public float shotgunCD = 2.0f;
    public float shotgunDMG = 1.5f;
    public float shotgunRange = 400f;
    public int shotgunPellets = 7;


    [HideInInspector]
    public Camera cam;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Animator gunAnim;
    public Transform sourcePoint;
    public float projectileSpeed = 30f;
    private bool projectileTest = false;
    private float timeSinceLastShot = 0;
    private bool playerShot = false;
    private Vector3 destination;
    private bool playerCanShoot = true;
    private int currentClip;
    private float randomX;
    private float randomY;
    private Vector2 localOffset;
    private Vector3 shotgunAngle;

    void Start() {
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        if (hasPistol == true)
        {
            currentClip = pistolClip;
        }
        else if (hasSMG == true)
        {
            currentClip = SMGClip;
        }
        else if(hasShotgun == true)
        {
            currentClip = shotgunClip;
        }
    }

    void Update() {

        if (playerInput.shoot) {
            if (hasPistol == true && playerCanShoot == true)
            {
                pistolShoot();
            }
            else if (hasSMG == true && playerCanShoot == true)
            {
                SMGShoot();
            }
            else if (hasShotgun == true && playerCanShoot == true)
            {
                ShotgunShoot();
            }
            else if (projectileTest == true)
            {
                InstantiateProjectile(sourcePoint);
            }
            
            /*else
            {
                Debug.Log("Player Attempted to Fire but none of the shoot functions went off. Current clip size is: " + currentClip);
            }*/
        }
        if (playerShot == true) //conditional that goes through once the player has shot. Acts as a "shot CD" so that the player can't just spam
        {
            timeSinceLastShot += Time.deltaTime;
            if (hasPistol == true && timeSinceLastShot >= pistolCD)
            {
                playerShot = false;
                playerCanShoot = true;
                timeSinceLastShot = 0;
            }
            else if(hasShotgun == true && timeSinceLastShot >= shotgunCD)
            {
                playerShot = false;
                playerCanShoot = true;
                timeSinceLastShot = 0;
            }
                //TODO implement conditional to track if the player can shoot their gun
        }
        if (playerInput.Reload && currentClip < pistolClip && hasPistol == true || playerInput.Reload && hasSMG == true && currentClip < SMGClip|| playerInput.Reload && currentClip < shotgunClip && hasShotgun == true || currentClip <= 0) //TODO implement reloading indicator and delay
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
            hit.transform.GetComponent<PlayerMovement>().playerHealth-= pistolDMG;

        }
        else if (hit.collider.gameObject.tag != "Player")
        {
            Debug.Log("Pistol hit something besides the player");
        }
        else
        {
            Debug.Log("Pistol missed");
        }
        gunAnim.SetTrigger("Fire");
        playerShot = true;
        playerCanShoot = false;
        currentClip--;
    }
    void SMGShoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("SMG hit player");
            hit.transform.GetComponent<PlayerMovement>().playerHealth -= SMGDMG;

        }
        else if (hit.collider.gameObject.tag != "Player")
        {
            Debug.Log("SMG hit something besides the player");
        }
        else
        {
            Debug.Log("SMG missed");
        }

        currentClip--;
    }
    void ShotgunShoot()
    {
        for (var i = 0; i < shotgunPellets; i++)
        { //For each pellet create a random origin and fire

            randomX = Random.Range(-1.0f, 1.0f);
            randomY = Random.Range(-1.0f, 1.0f);
            localOffset.y += randomY;
            localOffset.x += randomX;
            shotgunAngle = new Vector3 (cam.transform.position.x + localOffset.x, cam.transform.position.y + localOffset.y, cam.transform.position.z);
            Debug.Log("Shotgun fired");
            if (Physics.Raycast(shotgunAngle, cam.transform.forward, out hit, shotgunRange) && hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Shotgun hit player");
                hit.transform.GetComponent<PlayerMovement>().playerHealth -= shotgunDMG;

            }
            else if (hit.collider.gameObject.tag != "Player")
            {
                Debug.Log("Shotgun hit something besides the player");
            }
            else
            {
                Debug.Log("Shotgun missed");
            }
        }
        playerShot = true;
        playerCanShoot = false;
        currentClip--;

    }
    
    void Reload()
    {
        if (hasPistol == true)
        {
            Debug.Log("Pistol Reloaded");
            currentClip = pistolClip;
        }
        else if(hasSMG == true)
        {
            Debug.Log("SMG reloaded");
            currentClip = SMGClip;
        }
        else if(hasShotgun == true)
        {
            Debug.Log("Shotgun reloaded");
            currentClip = shotgunClip;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pickups"))
        {
            Debug.Log("Item Picked Up");
            if(other.gameObject.tag == "Pistol" && hasPistol == false)
            {
                hasPistol = true;
                currentClip = pistolClip;
            }
            other.GetComponent<PickupObject>().pickedUp = true;
            other.GetComponent<PickupObject>().objectNotMoved = true;

        }
    }
}
