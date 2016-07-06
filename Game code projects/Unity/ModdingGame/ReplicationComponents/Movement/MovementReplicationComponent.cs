using UnityEngine;
using System.Collections;

public class MovementReplicationComponent : Photon.PunBehaviour
{
    #region properties
    // If set false then will not replicate even if one of the replicates flags is true
    public bool Replicates = true;

    // If true then actor replicates its position
    public bool ReplicatesPosition = true;
    // If true then actor replicates its rotation
    public bool ReplicatesRotation = true;
    // If true then actor replicates its scale
    public bool ReplicatesScale = false;
    #endregion

    #region pun hooks
    // Does the actual replication
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (Replicates)
        {
            if (stream.isWriting)
            {
                Vector3 position = Vector3.zero;
                Quaternion rotation = Quaternion.identity;
                Vector3 scale = Vector3.zero;

                if (ReplicatesPosition)
                    ReplicatePosition(ref position, true);
                if (ReplicatesRotation)
                    ReplicateRotation(ref rotation, true);
                if (ReplicatesScale)
                    ReplicateScale(ref scale, true);

                if (ReplicatesPosition)
                    stream.SendNext(position);
                if (ReplicatesRotation)
                    stream.SendNext(rotation);
                if (ReplicatesScale)
                    stream.SendNext(scale);
            }
            else if (stream.isReading)
            {
                Vector3 position = Vector3.zero;
                Quaternion rotation = Quaternion.identity;
                Vector3 scale = Vector3.zero;

                if (ReplicatesPosition)
                    position = (Vector3)stream.ReceiveNext();
                if (ReplicatesRotation)
                    rotation = (Quaternion)stream.ReceiveNext();
                if (ReplicatesScale)
                    scale = (Vector3)stream.ReceiveNext();

                if (ReplicatesPosition)
                    ReplicatePosition(ref position, false);
                if (ReplicatesRotation)
                    ReplicateRotation(ref rotation, false);
                if (ReplicatesScale)
                    ReplicateScale(ref scale, false);
            }
        }
    }

    // Replicates position, uses ref
    public virtual void ReplicatePosition(ref Vector3 position, bool writing)
    {
        if (writing)
        {
            position = transform.position;
        }
        else
        {
            transform.position = position;
        }
    }

    // Replicates rotation, uses ref
    public virtual void ReplicateRotation(ref Quaternion rotation, bool writing)
    {
        if (writing)
        {
            rotation = transform.rotation;
        }
        else
        {
            transform.rotation = rotation;
        }
    }

    // Replicates scale, uses ref
    public virtual void ReplicateScale(ref Vector3 scale, bool writing)
    {
        if (writing)
        {
            scale = transform.localScale;
        }
        else
        {
            transform.localScale = scale;
        }

    }
    #endregion
}
