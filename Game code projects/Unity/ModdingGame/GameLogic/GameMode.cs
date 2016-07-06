using UnityEngine;
using System.Collections;
using System;

/*
    Base class for all GameModes

    -GameModes manage game logic, they're responsible for entity spawning and game rules.
*/
public class GameMode : PhotonSingleton<PhotonConnectionManager>
{
    // Resource name of default pawn to be spawned when player joins a room
    [Inspector]
    public string DefaultPawnResourceName;
    // Resource name of default controller to be spawned when player joins a room
    [Inspector]
    public string DefaultControllerResourceName;

    // Spawns a controller
    public virtual void SpawnDefaultController()
    {
        if(DefaultControllerResourceName == null)
        {
            Debug.LogError("GameMode - SpawnDefaultController: DefaultControllerResourceName is not defined in the inspector");
            return;
        }

        GameObject controllerGameObject = PhotonNetwork.Instantiate(DefaultControllerResourceName, Vector3.zero, Quaternion.identity, 0);

        if(controllerGameObject == null)
        {
            Debug.LogError("GameMode - SpawnDefaultController: Failed to create controller with resource name: " + DefaultControllerResourceName);
        }
    }

    public virtual void SpawnDefaultPawn(PhotonPlayer forPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (DefaultPawnResourceName == null)
            {
                Debug.LogError("GameMode - SpawnDefaultPawn: DefaultPawnResourceName is not defined in the inspector");
                return;
            }

            PhotonNetwork.Instantiate(DefaultPawnResourceName, Vector3.zero, Quaternion.identity, 0, new object[] { forPlayer });
        }
    }

    #region pun hooks
    // Spawns default controller on joining a room, also spawns default pawn for master client
    public override void OnJoinedRoom()
    {
        #region debug temporary
        if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.player.name = "Master Client!";
        }
        else
        {
            PhotonNetwork.player.name = "Noob Pleb!";
        }
        #endregion

        SpawnDefaultController();
        if (PhotonNetwork.isMasterClient)
        {
            SpawnDefaultPawn(PhotonNetwork.player);
        }
    }

    // Spawns default pawn for a new player that has connected if we're master client
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);

        if(PhotonNetwork.isMasterClient)
            SpawnDefaultPawn(newPlayer);
    }

    // Serializes gamemode related data
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion
}
