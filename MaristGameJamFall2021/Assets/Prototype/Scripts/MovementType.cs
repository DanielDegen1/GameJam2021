using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementType : MonoBehaviour
{
    public Status changeTo;

    protected PlayerController player;
    protected PlayerMovement movement;
    protected InputController inputController;
    protected Status playerStatus;

    public virtual void Start()
    {
        player = GetComponent<PlayerController>();

        player.AddMovementType(this);
        player.AddToStatusChange(PlayerStatusChange);
    }

    public virtual void SetPlayerComponents(PlayerMovement move, InputController input)
    {
        movement = move; inputController = input;
    }

    public virtual void PlayerStatusChange(Status status, Func<IKData> call)
    {
        playerStatus = status;
    }

    public virtual void Movement()
    {
        //Movement info
    }

    public virtual void Check(bool canInteract)
    {
        //Check info
    }

    public virtual IKData IK()
    {
        IKData data = new IKData();
        return data;
    }
}
