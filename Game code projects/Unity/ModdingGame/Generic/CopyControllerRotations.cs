using UnityEngine;
using System.Collections;

/*
    Copies specified rotations from controller
*/
public class CopyControllerRotations : MonoBehaviour
{
    // Controller to copy rotations from
    [Inspector]
    public Controller Controller;

    // If set true then local rotation will be copied and set, else global rotation
    [Inspector]
    public bool CopyLocals = true;

    // If true, copies pitch from control rotation
    [Inspector]
    public bool CopyPitch = false;
    // If true, copies roll from control rotation
    [Inspector]
    public bool CopyRoll = false;
    // If true, copies yaw from control rotation
    [Inspector]
    public bool CopyYaw = false;

    // Checks if controller is set
    private void Start()
    {
        if(Controller == null)
        {
            Debug.LogWarning("CopyControllerRotations: Controller is not set in Inspector, is this by design?");
        }
    }

    // Copies controller rotation every frame
    private void Update()
    {
        if (Controller == null)
        {
            return;
        }

        Vector3 rotation;
        if (CopyLocals)
            rotation = transform.localEulerAngles;
        else
            rotation = transform.eulerAngles;

        if (CopyPitch)
            rotation.x = Controller.ControlRotation.x;
        if (CopyRoll)
            rotation.z = Controller.ControlRotation.z;
        if (CopyYaw)
            rotation.y = Controller.ControlRotation.z;

        if (CopyLocals)
            transform.localEulerAngles = rotation;
        else
            transform.eulerAngles = rotation;

    }
}
