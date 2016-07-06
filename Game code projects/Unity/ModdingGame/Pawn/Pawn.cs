using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;

/*
    Base class for all actors that can be possessed and controlled by controllers
*/
public class Pawn : Actor
{
    // Current controller controlling the pawn
    [HideInInspector]
    public Controller Controller;

    // Movement component, handles all of pawns movement
    [HideInInspector]
    public MovementComponent MovementComponent;

    [Inspector]
    public bool UseControllerRotationPitch = false;
    [Inspector]
    public bool UseControllerRotationRoll = false;
    [Inspector]
    public bool UseControllerRotationYaw = true;

    // Called when possessed by a controller
    public virtual void PossessedBy(Controller controller)
    {

    }

    // Called when possession is dropped NOTE: Rename it to something better
    public virtual void PossessionDropped()
    {

    }

    // Moves player around depending on the movement input
    public virtual void AddMovementInput(Vector3 direction)
    {
        if (MovementComponent != null)
        {
            MovementComponent.InputDirection += direction;
        }
        else
        {
            Debug.LogError("Pawn.cs - AddMovementInput: Pawn is missing MovementComponent!");
        }
    }

    // Adds pitch rotation to Controller's ControlRotation
    public virtual void AddControllerPitchInput(float value)
    {
        Controller.AddPitchInput(value);
    }

    // Adds roll rotation to Controller's ControlRotation
    public virtual void AddControllerRollInput(float value)
    {
        Controller.AddRollInput(value);
    }

    // Adds yaw rotation to Controller's ControlRotation
    public virtual void AddControllerYawInput(float value)
    {
        Controller.AddYawInput(value);
    }

    // Returns view rotation, will return ControlRotation from Controller if it's present, if not, then it returns regular rotation in eulers
    public virtual Vector3 GetViewRotation()
    {
        if (Controller)
            return Controller.ControlRotation;
        else
            return transform.eulerAngles;
    }

    // Handles player rotation, if controller is present then take controller rotation if specified as so
    public virtual void HandleRotation()
    {
        if (!photonView.isMine)
            return;

        if (Controller == null)
            return;

        Vector3 rotation = transform.eulerAngles;

        if (UseControllerRotationPitch)
            rotation.x = Controller.ControlRotation.x;
        if (UseControllerRotationRoll)
            rotation.z = Controller.ControlRotation.z;
        if (UseControllerRotationYaw)
            rotation.y = Controller.ControlRotation.y;

        transform.eulerAngles = rotation;
    }

    // Sets and checks movement component
    private void SetAndCheckMovementComponent()
    {
        MovementComponent = GetComponent<MovementComponent>();
        if (MovementComponent == null)
        {
            Debug.LogError("Pawn.cs - Start: Pawn is missing MovementComponent, it shouldn't be missing it!");
            return;
        }
    }

    #region rpcs
    // Boradcasts a possession event to all the players, also sets controller data
    [PunRPC]
    public void RpcBroadcastPossession(int controllerViewId)
    {
        Debug.Log("Receiving possession from: " + controllerViewId);
        Controller = ServerManager.Instance.GetControllerByViewId(controllerViewId);
        if(Controller == null)
        {
            Debug.LogError("Pawn.cs - RpcBroadcastPossession: Controller from ServerManager is null!");
            return;
        }
        PossessedBy(Controller);
        Controller.ControlledPawn = this;
    }

    // Broadcasts an unpossession event to all players, also clears controller data
    public void RpcBroadcastUnPossession()
    {
        if (Controller == null)
        {
            Debug.LogWarning("Pawn.cs - RpcBroadcastUnPossession: Controller is already null, what are we unpossessing from?");
        }

        Controller.ControlledPawn = null;
        Controller = null;
        PossessionDropped();
    }
    #endregion

    #region unity hooks
    // Sets Movement component if one exists
    public override void Start()
    {
        SetAndCheckMovementComponent();

        base.Start();
    }

    // Handles rotation
    public virtual void Update()
    {
        HandleRotation();
    }
    #endregion
}
