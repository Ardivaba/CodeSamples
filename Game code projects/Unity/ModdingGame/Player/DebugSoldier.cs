using UnityEngine;
using System.Collections;

/*
    Debug player to help figure out how to structure classes extended from BasePlayer
*/
public class DebugSoldier : BasePlayer
{
    // Animator to set third person animations to
    [Inspector]
    public Animator ThirdPersonAnimator;

    // Animator to set first person animations to
    [Inspector]
    public Animator FirstPersonAnimator;

    // Will write animation data to animator if it's our soldier
    public override void Start()
    {
        base.Start();

        if (ThirdPersonAnimator == null)
        {
            Debug.LogError("Soldier.cs - Start: Soldier is missing Third Person Animator, did you forget to set it in inspector?");
        }

        if (FirstPersonAnimator == null)
        {
            Debug.LogError("Soldier.cs - Start: Soldier is missing First Person Animator, did you forget to set it in inspector?");
        }
    }

    // Sets animator horizontal and vertical values depending on input NOTE: Refactor that shit
    public override void Update()
    {
        base.Update();

        if (photonView.isMine)
        {
            if (ThirdPersonAnimator != null)
            {
                ThirdPersonAnimator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
                ThirdPersonAnimator.SetFloat("Vertical", Input.GetAxis("Vertical"));

                if (Controller != null)
                    ThirdPersonAnimator.SetFloat("VerAimAngle", Controller.ControlRotation.x * -1);
            }
            else
            {
                Debug.LogError("Soldier.cs - Update: Soldier is missing Third Person Animator, did you forget to set it in inspector?");
                gameObject.SetActive(false);
            }

            if (FirstPersonAnimator != null)
            {
                if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.0f)
                {
                    FirstPersonAnimator.SetBool("Moving", true);
                }
                else
                {
                    FirstPersonAnimator.SetBool("Moving", false);
                }

                if (Input.GetButton("Fire1"))
                {
                    FirstPersonAnimator.SetBool("Shooting", true);
                }
                else
                {
                    FirstPersonAnimator.SetBool("Shooting", false);
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    FirstPersonAnimator.SetBool("Aiming", !FirstPersonAnimator.GetBool("Aiming"));
                }
            }
            else
            {
                Debug.LogError("Soldier.cs - Update: Soldier is missing First Person Animator, did you forget to set it in inspector?");
                gameObject.SetActive(false);
            }
        }
    }
}
