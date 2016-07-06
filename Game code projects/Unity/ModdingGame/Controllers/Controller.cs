using UnityEngine;
using System.Collections;

/*
    Base class for all controllers that can possess and control pawns
*/
public class Controller : Photon.PunBehaviour
{
    // Player owning this controller
    public PhotonPlayer PhotonPlayer;
    // Pawns can use the rotation of controller instead of their own
    public Vector3 ControlRotation = Vector3.zero;
    // Current pawn that is being controlled by this controller
    public Pawn ControlledPawn;

    // Adds pitch rotation to ControlRotation
    public virtual void AddPitchInput(float value)
    {
        ControlRotation.x += value;
    }

    // Adds roll rotation to ControlRotation
    public virtual void AddRollInput(float value)
    {
        ControlRotation.z += value;
    }

    // Adds yaw rotation to ControlRotation
    public virtual void AddYawInput(float value)
    {
        ControlRotation.y += value;
    }

    // Possesses the targetPawn
    public virtual void Possess(Pawn targetPawn)
    {
        targetPawn.photonView.TransferOwnership(PhotonPlayer);

        if (ControlledPawn == null)
            targetPawn.photonView.RPC("RpcBroadcastPossession", PhotonTargets.All, photonView.viewID);
        else
            Debug.LogWarning("Controller - Possess: Trying to Possess but already has possessed Pawn!");
    }

    // Stops possessing the currently possessed pawn
    public virtual void UnPossess()
    {
        if (ControlledPawn != null)
            ControlledPawn.photonView.RPC("RpcBroadcastUnPossession", PhotonTargets.All, photonView.viewID);
        else
            Debug.LogWarning("Controller - UnPossess: Trying to UnPossess but has no possessed Pawn!");
    }

    // Will autopossess a pawn if it has PhotonPlayer (matching PhotonPlayer) set in instantiationData[0]
    public virtual void AutoPossessIfPossible()
    {
        if (PhotonNetwork.isMasterClient)
        {
            foreach (Pawn pawn in GameObject.FindObjectsOfType<Pawn>())
            {
                if (pawn.photonView.instantiationData == null)
                    continue;

                if (pawn.photonView.instantiationData[0] != null)
                {
                    PhotonPlayer player = (PhotonPlayer)pawn.photonView.instantiationData[0];
                    if (player == photonView.owner)
                    {
                        Possess(pawn);
                        // NOTE: Should controller be able to possess multiple pawns or should it return from here?
                    }
                }
            }
        }
    }

    // Adds rotation input to ControlRotation and movement input to ControlledPawn
    public virtual void AddInputs()
    {
        AddPitchInput(Input.GetAxis("Mouse Y"));
        AddYawInput(Input.GetAxis("Mouse X"));
        ControlledPawn.AddMovementInput(transform.forward * Input.GetAxis("Vertical"));
        ControlledPawn.AddMovementInput(transform.right * Input.GetAxis("Horizontal"));
    }

    #region pun hooks
    // Called when Instantiated by Photon, also sets PhotonPlayer property
    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        PhotonPlayer = info.sender;
    }

    // Does serialization for data, stubbed in controller base
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion

    #region unity hooks
    // Calls AutoPossess on start
    void Start()
    {
        AutoPossessIfPossible();
    }

    // If we are controlling the pawn then call AddInputs
    public virtual void Update()
    {
        if(ControlledPawn != null && photonView.isMine)
        {
            AddInputs();
        }
    }
    #endregion
}
