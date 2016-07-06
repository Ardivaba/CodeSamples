using UnityEngine;
using System.Collections;

/*
    Generic server manager, mainly exposes different methods to access classes using ids and vice versa
*/
public class ServerManager : PhotonSingleton<ServerManager>
{
    // Gets actor from photonView id
    public Actor GetActorByViewId(int viewId)
    {
        Actor[] actors = GameObject.FindObjectsOfType<Actor>();

        foreach (Actor actor in actors)
        {
            if (actor.photonView.viewID == viewId)
            {
                return actor;
            }
        }

        Debug.LogError("ServerManager - GetActorByViewId: Actor with viewId: " + viewId + " not found!");
        return null;
    }

    // Gets pawn from photonView id
    public Pawn GetPawnByViewId(int viewId)
    {
        Pawn[] pawns = GameObject.FindObjectsOfType<Pawn>();

        foreach(Pawn pawn in pawns)
        {
            if(pawn.photonView.viewID == viewId)
            {
                return pawn;
            }
        }

        Debug.LogError("ServerManager - GetPawnByViewId: Pawn with viewId: " + viewId + " not found!");
        return null;
    }

    // Gets controller from photonView id
    public Controller GetControllerByViewId(int viewId)
    {
        Controller[] controllers = GameObject.FindObjectsOfType<Controller>();

        foreach (Controller controller in controllers)
        {
            if (controller.photonView.viewID == viewId)
            {
                return controller;
            }
        }

        Debug.LogError("ServerManager - GetControllerByViewId: Controller with viewId: " + viewId + " not found!");
        return null;
    }

    // Gets controller owned specified photon player
    public Controller GetControllerByPhotonPlayer(PhotonPlayer photonPlayer)
    {
        Controller[] controllers = GameObject.FindObjectsOfType<Controller>();

        foreach (Controller controller in controllers)
        {
            if (controller.photonView.owner == photonPlayer)
            {
                return controller;
            }
        }

        Debug.LogError("ServerManager - GetControllerByPhotonPlayer: Controller owned by player: " + photonPlayer.name + " not found!");
        return null;
    }
}
