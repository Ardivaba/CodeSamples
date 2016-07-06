using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationReplicationComponent : Photon.PunBehaviour
{
    // Animator thats values are being replicated
    public Animator Animator;

    // List of smooth replicated animation floats
    public List<string> SmoothReplicatedFloats;
    // Buffer for smoothly replicated animation float values
    public Dictionary<string, float> RealSmoothFloats;

    // List of directly replicated animation floats
    public List<string> DirectlyReplicatedFloats;
    // Buffer for directly replicated animation float values
    public Dictionary<string, float> RealDirectFloats;

    public float FloatSpeed = 0.1f;

    // Initializes buffer dictionaries by copying the starting values from Animator
    private void InitializeBufferDictionaries()
    {
        RealSmoothFloats = new Dictionary<string, float>();
        foreach(string replicatedFloat in SmoothReplicatedFloats)
        {
            RealSmoothFloats.Add(replicatedFloat, Animator.GetFloat(replicatedFloat));
        }

        RealDirectFloats = new Dictionary<string, float>();
        foreach (string replicatedFloat in DirectlyReplicatedFloats)
        {
            RealDirectFloats.Add(replicatedFloat, Animator.GetFloat(replicatedFloat));
        }
    }

    #region pun hooks
    // Replicates the data
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            foreach(string replicatedFloat in SmoothReplicatedFloats)
            {
                stream.SendNext(Animator.GetFloat(replicatedFloat));
            }

            foreach (string replicatedFloat in DirectlyReplicatedFloats)
            {
                stream.SendNext(Animator.GetFloat(replicatedFloat));
            }
        }
        else if(stream.isReading)
        {
            foreach (string replicatedFloat in SmoothReplicatedFloats)
            {
                RealSmoothFloats[replicatedFloat] = (float) stream.ReceiveNext();
            }

            foreach (string replicatedFloat in DirectlyReplicatedFloats)
            {
                Animator.SetFloat(replicatedFloat, (float)stream.ReceiveNext());
            }
        }
    }
    #endregion

    #region unity hooks
    // Initializes paramters
    public virtual void Start()
    {
        InitializeBufferDictionaries();
    }

    void Update()
    {
        if (photonView.isMine)
            return;

        foreach(var realFloat in RealSmoothFloats)
        {
            string name = realFloat.Key;
            float value = realFloat.Value;
            float currentValue = Animator.GetFloat(name);

            if(value > 0)
            {
                currentValue = Mathf.MoveTowards(currentValue, 1.0f, FloatSpeed);
            }
            else if(value < 0)
            {
                currentValue = Mathf.MoveTowards(currentValue, -1.0f, FloatSpeed);
            }
            else
            {
                currentValue = Mathf.MoveTowards(currentValue, 0.0f, FloatSpeed);
            }

            Animator.SetFloat(name, currentValue);
        }
    }
    #endregion
}
