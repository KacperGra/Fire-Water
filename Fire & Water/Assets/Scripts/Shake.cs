using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnimator;

    public void CamShake()
    {
        camAnimator.SetTrigger("Shake");
    }
}
