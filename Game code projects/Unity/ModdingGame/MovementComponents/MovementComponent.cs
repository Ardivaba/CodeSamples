using UnityEngine;
using System.Collections;

/*
    Base Movement component for all pawns
*/
public class MovementComponent : MonoBehaviour
{
    // Pawn that this component is attached to
    [HideInInspector]
    public Pawn Pawn;

    // Forward, right input direction
    [HideInInspector]
    public Vector3 InputDirection { get; internal set; }

    #region inspector parameters
    [Inspector]
    public float MaxSpeed = 1.0f;
    #endregion

    // Determines movement vector and then calls Move
    public virtual void UpdateMove()
    {
        Vector3 movement = InputDirection * Time.fixedDeltaTime;
        if(movement.magnitude > MaxSpeed)
        {
            Vector3 movementDirection = movement.normalized;
            movement = movementDirection * MaxSpeed;
        }

        Move(movement);

        InputDirection = Vector3.zero;
    }

    // Applies movement vector
    public virtual void Move(Vector3 movement)
    {
        transform.Translate(movement * 2.0f);
    }

    #region unity hooks
    // Sets pawn property
    private void Start()
    {
        Pawn = GetComponent<Pawn>();

        if(Pawn == null)
        {
            Debug.LogError("MovementComponent.cs: Pawn is null, something got really fucked up.");
            return;
        }
    }

    // Calls UpdateMove if local player owns this pawn
    private void FixedUpdate()
    {
        if(Pawn.photonView.isMine)
            UpdateMove();
    }
    #endregion
}
