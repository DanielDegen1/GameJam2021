using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputController : MonoBehaviour
{
    private Vector2 inputVal, mouseData;
    private bool runVal, throwVal, jumpVal, pauseVal, shootVal, crouchVal, crouchingVal, interactVal, reloadVal, pickupVal, swapVal;
    public int playerIndex;

    public PlayerConfiguration playerConfig;

    Vector2 mouseDelta;
    
    /*
    private static InputController _instance;

    public static InputController Instance{
        get {
            return _instance;
        }
    }
    */
    
    public PlayerControls playerControls;

    private void Awake() {
        /*
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
        */
        playerControls = new PlayerControls();
        Cursor.visible = false;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }


    public void setInput(Vector2 inputData) {
        inputVal = inputData;
    }
    public Vector2 input
    {
        get
        {
            return inputVal;
            /*
            Vector2 i = Vector2.zero;
            i.x = Input.GetAxis("Horizontal");
            i.y = Input.GetAxis("Vertical");
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
            */
        }
    }

    public Vector2 down
    {
        get { return _down; }
    }

    public void SetMouseDelta(Vector2 maus) {
        mouseData = maus;
    }
    public Vector2 GetMouseDelta() {
        return mouseData;
    }

    public Vector2 raw
    {
        get
        {
            return playerControls.PlayerMovement.Movement.ReadValue<Vector2>();
            /*
            Vector2 i = Vector2.zero;
            i.x = Input.GetAxisRaw("Horizontal");
            i.y = Input.GetAxisRaw("Vertical");
            i *= (i.x != 0.0f && i.y != 0.0f) ? .7071f : 1.0f;
            return i;
            */
        }
    }

    public float elevate
    {
        get
        {
            return playerControls.PlayerMovement.Elevate.ReadValue<float>();
            //return Input.GetAxis("Elevate");
        }
    }

    public void setRun(bool runValue){
        runVal = runValue;

    }
    public bool run
    {
        get {
            return runVal;
        }
    }

    public void setCrouch(bool crouchValue) {
        crouchVal = crouchValue;
    }
    public bool crouch
    {
        get { return crouchVal;}
    }

    public void setCrouching(bool crouchingValue) {
        crouchingVal = crouchingValue;
    }

    public bool crouching
    {
        get { return crouchingVal;}
    }

    public void setInteract(bool interactValue) {
        interactVal = interactValue;
    }
    public bool interact {
        get{
            return interactVal;
        }
    }

    public void setThrow(bool throwValue) {
        throwVal = throwValue;
    }
    public bool Throw {
        get{
            return throwVal;
        }
    }

    public void setShoot(bool shootValue) {
        shootVal = shootValue;
    }
    public bool shoot {
        get {
            return shootVal;
        }
    }

    public bool Stasis
    {
        get
        {
            return playerControls.PlayerMovement.Stasis.triggered;
        }
    }

    public void setPause(bool pauseValue) {
        pauseVal = pauseValue;
    }
    public bool Pause {
        get {
            return pauseVal;
        }
    }


    public void setReload(bool reloadValue) {
        reloadVal = reloadValue;
    }
    public bool Reload
    {
        get
        {
            return reloadVal;
        }
    }

    public void setPickup(bool pickupValue) {
        pickupVal = pickupValue;
    }
    public bool Pickup
    {
        get
        {
            return pickupVal;
        }

    }

    public void setSwap(bool swapValue) {
        swapVal = swapValue;
    }
    public bool Swap
    {
        get
        {
            return swapVal;
        }
    }

    /*
    public bool throw {
        get {
            return playerControls.PlayerMovement.Throw.triggered;
        }
    }
    */

    /*
    public KeyCode interactKey
    { 
        get { return KeyCode.E; }
    }
    */

    /*
    public bool reload
    {
        get { return Input.GetKeyDown(KeyCode.R); }
    }

    public bool aim
    {
        get { return Input.GetMouseButtonDown(1); }
    }

    public bool aiming
    {
        get { return Input.GetMouseButton(1); }
    }

    public bool shooting
    {
        get { return Input.GetMouseButton(0); }
    }

    public float mouseScroll
    { 
        get { return Input.GetAxisRaw("Mouse ScrollWheel"); }
    }
    */

    private Vector2 previous;
    private Vector2 _down;

    public int jumpTimer;
    public bool jump;

    public void setJump(bool jumpValue) {
        jumpVal = jumpValue;
    }

    void Start()
    {
        jumpTimer = -1;
    }

    void Update()
    {
        _down = Vector2.zero;
        if (raw.x != previous.x)
        {
            previous.x = raw.x;
            if (previous.x != 0)
                _down.x = previous.x;
        }
        if (raw.y != previous.y)
        {
            previous.y = raw.y;
            if (previous.y != 0)
                _down.y = previous.y;
        }
    }

    public void FixedUpdate()
    {
        /*
        if (!(playerControls.PlayerMovement.Jump.ReadValue<float>() > 0))
        {
            jump = false;
            jumpTimer++;
        }
        else if (jumpTimer > 0) {
            Debug.Log("Jump");
            jump = true;
        }
        */
    }

    public bool Jump()
    {
        return jumpVal;
    }

    public void ResetJump()
    {
        jumpTimer = -1;
    }
}
