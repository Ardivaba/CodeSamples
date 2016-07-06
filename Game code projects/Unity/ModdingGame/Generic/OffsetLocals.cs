using UnityEngine;
using System.Collections;

/*
    Offsets local positions in late update, mainly used to adjust animations
*/
public class OffsetLocals : MonoBehaviour
{
    // Position offset to be added to the local position
    [Inspector]
    public Vector3 PositionModification;
    // Rotation offset to be added to the local rotation
    [Inspector]
    public Vector3 RotationModification;

    // Adds offsets to local position and local rotation
    void LateUpdate()
    {
        if(PositionModification == null && RotationModification == null)
        {
            Debug.LogError("OffsetLocals: Neither PositionModification nor RotationModification is set in the inspector!");
            return;
        }

        transform.localPosition += PositionModification;
        transform.localEulerAngles += RotationModification;
    }
}
