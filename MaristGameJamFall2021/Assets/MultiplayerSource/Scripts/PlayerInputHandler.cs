using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private InputController inputController;

    [SerializeField]
    private MeshRenderer playerMesh;

    private PlayerControls controls;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        Debug.Log("Player Initialized");
        playerConfig = config;
        //playerMesh.material = config.playerMaterial;
        config.Input.onActionTriggered += Input_onActionTriggered;
    }   

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Jump.name)
        {
            OnJump(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Crouch.name)
        {
            OnCrouch(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Sprint.name)
        {
            OnRun(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Crouching.name)
        {
            OnCrouching(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Interact.name)
        {
            OnInteract(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Swap.name)
        {
            OnSwap(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Pickup.name)
        {
            OnPickup(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Shoot.name)
        {
            OnShoot(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Look.name)
        {
            OnLook(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Reload.name)
        {
            OnReload(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Pause.name)
        {
            OnPause(obj);
        }
    }

    public void OnMove(CallbackContext context)
    {
        //if(mover != null)
            inputController.setInput(context.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext context) {
        if (!(controls.PlayerMovement.Jump.ReadValue<float>() > 0)) {
            inputController.setJump(false);
        }
        else if (inputController.jumpTimer > 0) {
            inputController.setJump(true);
        }
    }

    public void OnShoot(CallbackContext context) {
        inputController.setShoot(context.action.triggered);
    }

    public void OnCrouch(CallbackContext context) {
        inputController.setCrouch(context.action.triggered);
    }

    public void OnRun(CallbackContext context) {
        if(controls.PlayerMovement.Sprint.ReadValue<float>() > 0) {
            inputController.setRun(true);
        }
        else {
            inputController.setRun(false);
        }
    }

    public void OnCrouching(CallbackContext context) {
        inputController.setCrouching(context.action.triggered);
    }

    public void OnInteract(CallbackContext context) {
        inputController.setInteract(context.action.triggered);
    }

    public void OnLook(CallbackContext context)
    {
        //if(mover != null)
            inputController.SetMouseDelta(context.ReadValue<Vector2>());
    }

    public void OnReload(CallbackContext context) {
        inputController.setReload(context.action.triggered);
    }

    public void OnSwap(CallbackContext context) {
        inputController.setSwap(context.action.triggered);
    }

    public void OnPickup(CallbackContext context) {
        inputController.setPickup(context.action.triggered);
    }

    public void OnPause(CallbackContext context) {
        inputController.setPause(context.action.triggered);
    }

}
