using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;

/*
    Base class for basic player pawn
*/

public class BasePlayer : Pawn
{
    // Enables camera, disables mesh rendering if pawn is controlled by local player
    public override void PossessedBy(Controller controller)
    {
        if (controller.photonView.isMine)
        {
            SetMeshRendering(false);
            SetCamera(true);

            // Sets controller for CopyControllerRotation to enable camera pitching
            CopyControllerRotations copyComponent = transform.GetChild(1).GetComponent<CopyControllerRotations>();
            if(!copyComponent)
            {
                Debug.LogError("BasePlayer - PossessedBy: Second child of player prefab doesn't have CopyControllerRotations!");
                return;
            }
            else
            {
                copyComponent.Controller = Controller;
            }
        }
    }

    // Disables camera whenever possession is dropped
    public override void PossessionDropped()
    {
        CopyControllerRotations copyComponent = transform.GetChild(1).GetComponent<CopyControllerRotations>();
        if (!copyComponent)
        {
            Debug.LogError("BasePlayer - PossessionDropped: Second child of player prefab doesn't have CopyControllerRotations!");
            return;
        }

        // Sets controller for CopyControllerRotation to enable camera pitching
        copyComponent.Controller = null;

        SetMeshRendering(true);
        SetCamera(false);
    }

    // Sets camera on or off, NOTE: Camera is currently hardcoded to be the second child of parent gameobject
    void SetCamera(bool on)
    {
        if(transform.GetChild(1) == null)
        {
            Debug.LogError("BasePlayer.cs - SetCamera: BasePlayer transform is missing second child that's supposed to be camera!");
            return;
        }
        transform.GetChild(1).gameObject.SetActive(on);
    }

    // Sets mesh rendering on or off, NOTE: Player mesh is currently hradcoded to be the first child of parent gameobject
    void SetMeshRendering(bool on)
    {
        Transform modelTransform = transform.GetChild(0);
        foreach(SkinnedMeshRenderer renderer in modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
        {
            renderer.enabled = false;
        }
    }
}
