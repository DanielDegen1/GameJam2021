using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class Shooting : MonoBehaviour
{
    InputController inputController;
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

    private int score = 0;
    public int targetScore = 5;
    [HideInInspector]
    public Camera cam;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Animator gunAnim;
    public Transform sourcePoint;
    public float projectileSpeed = 30f;
    private float timeSinceLastShot = 0;
    private bool playerShot = false;
    private Vector3 destination;
    private bool playerCanShoot = true;
    private int currentClip;
    private float randomX;
    private float randomY;
    private Vector2 localOffset;
    private Vector3 shotgunAngle;
    void Start()
    {
        cam = Camera.main;
        inputController = GetComponent<InputController>();
        if (hasPistol == true)
        {
            currentClip = pistolClip;
        }
        else if (hasSMG == true)
        {
            currentClip = SMGClip;
        }
        else if (hasShotgun == true)
        {
            currentClip = shotgunClip;
        }
    }

    void Update()
    {
        if(score >= targetScore)
        {
            Debug.Log("Player wins :)");
            //goto end screen
        }
        if (inputController.shoot && currentClip > 0)
        {
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
            else
            {
                Debug.Log("Player shot but none of the shot functions were called");
            }
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
            else if (hasShotgun == true && timeSinceLastShot >= shotgunCD)
                {
                    playerShot = false;
                    playerCanShoot = true;
                    timeSinceLastShot = 0;
                }
                //TODO implement conditional to track if the player can shoot their gun
        }

            if (inputController.Reload && currentClip < pistolClip && hasPistol == true || inputController.Reload && hasSMG == true && currentClip < SMGClip || inputController.Reload && currentClip < shotgunClip && hasShotgun == true || currentClip <= 0 && inputController.shoot) //TODO implement reloading indicator and delay
            {
              Reload();
            }
    }
    

        void pistolShoot()
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Pistol hit player");
                if (hit.transform.GetComponent<PlayerMovement>().invincible == false)
                {
                    Debug.Log("and they take dmg!");
                    Debug.Log("Player is now invincible");
                    hit.transform.GetComponent<PlayerMovement>().playerHealth -= pistolDMG;
                    hit.transform.GetComponent<PlayerMovement>().invincible = true;
                    score++;
                Debug.Log("Player's current score is " + score);
                }
                else
                {
                    Debug.Log("but the player was invincible");
                }

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
                if (hit.transform.GetComponent<PlayerMovement>().invincible == false)
                {
                    Debug.Log("and they take dmg!");
                    Debug.Log("Player is now invincible");
                    hit.transform.GetComponent<PlayerMovement>().playerHealth -= SMGDMG;
                    hit.transform.GetComponent<PlayerMovement>().invincible = true;
                score++;
                Debug.Log("Player's current score is " + score);

            }

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
                shotgunAngle = new Vector3(cam.transform.position.x + localOffset.x, cam.transform.position.y + localOffset.y, cam.transform.position.z);
                Debug.Log("Shotgun fired");

                if (Physics.Raycast(shotgunAngle, cam.transform.forward, out hit, shotgunRange) && hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Shotgun hit player");
                    if (hit.transform.GetComponent<PlayerMovement>().invincible == false)
                    {
                        Debug.Log("and they take dmg!");
                        Debug.Log("Player is now invincible");
                        hit.transform.GetComponent<PlayerMovement>().playerHealth -= shotgunDMG;
                        hit.transform.GetComponent<PlayerMovement>().invincible = true;
                    score++;
                    Debug.Log("Player's current score is " + score);

                }
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
            else if (hasSMG == true)
            {
                Debug.Log("SMG reloaded");
                currentClip = SMGClip;
            }
            else if (hasShotgun == true)
            {
                Debug.Log("Shotgun reloaded");
                currentClip = shotgunClip;
            }
        }


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Pickups") && inputController.Pickup)
            {
                Debug.Log("Picked Up: " + other.gameObject.tag);
                if (other.gameObject.tag == "Pistol" && hasPistol == false)
                {
                    hasPistol = true;
                    hasSMG = false;
                    hasShotgun = false;
                    currentClip = pistolClip;
                }
                else if (other.gameObject.tag == "SMG" && hasSMG == false)
                {
                    hasPistol = false;
                    hasSMG = true;
                    hasShotgun = false;
                    currentClip = SMGClip;
                }
                else if (other.gameObject.tag == "Shotgun" && hasShotgun == false)
                {
                    hasPistol = false;
                    hasSMG = false;
                    hasShotgun = true;
                    currentClip = shotgunClip;
                }
                other.GetComponent<PickupObject>().pickedUp = true;
                other.GetComponent<PickupObject>().objectNotMoved = true;
            }

        }
    }

