using UnityEngine;
using System.Collections;

/*
    Smoothly replicates position and rotation of an actor
*/
public class SmoothMovementReplicationComponent : MovementReplicationComponent
{
    // Lerp speed for position
    public float PositionLerpSpeed = 7.5f;
    // Lerp speed for rotation
    public float RotationLerpSpeed = 15.0f;

    // Actual position of actor, buffered
    public Vector3 RealPosition;
    // Actual rotation of actor, buffered
    public Quaternion RealRotation;

    // Replicates position, writes current position and reads position into buffer
    public override void ReplicatePosition(ref Vector3 position, bool writing)
    {
        if(writing)
        {
            position = transform.position;
        }
        else
        {
            RealPosition = position;
        }
    }

    // Replicates rotation, writes current rotation and reads rotation into buffer
    public override void ReplicateRotation(ref Quaternion rotation, bool writing)
    {
        if (writing)
        {
            rotation = transform.rotation;
        }
        else
        {
            RealRotation = rotation;
        }
    }

    #region pun hooks
    #endregion

    #region untiy hooks
    // Initialize buffered position and rotation
    public virtual void Start()
    {
        RealPosition = transform.position;
        RealRotation = transform.rotation;
    }

    // Lerps positon and rotation to their real values over specified lerp speeds
    public virtual void Update()
    {
        if(!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, RealPosition, Time.deltaTime * PositionLerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, RealRotation, Time.deltaTime * RotationLerpSpeed);
        }
    }
    #endregion
}
