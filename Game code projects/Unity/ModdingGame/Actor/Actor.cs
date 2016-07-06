using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;

/*
    Base class for all networked spawnable gameplay related objects
*/
public class Actor : Photon.PunBehaviour
{
    // Movement replication component that is used to replicate position, rotation, scale
    [HideInInspector]
    public MovementReplicationComponent MovementReplicationComponent;

    // Makes sure that actor has replication component attached to it
    private void SetAndCheckForMovementReplicationComponent()
    {
        MovementReplicationComponent = GetComponent<MovementReplicationComponent>();

        if (MovementReplicationComponent == null)
        {
            Debug.LogWarning(transform.name + " is missing replication component, all actors must have MovementReplicationComponent!");
        }
    }

    #region unity hooks
    public virtual void Start()
    {
        SetAndCheckForMovementReplicationComponent();
    }
    #endregion

    #region pun hooks
    // OnPhotonSerializeView Stub
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion
}
