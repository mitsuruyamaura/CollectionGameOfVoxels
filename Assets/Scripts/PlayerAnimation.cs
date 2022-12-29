using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        TryGetComponent(out anim);    
    }

    
    public void PlayWalkAnim(float magnitude) {
        anim.SetFloat("Speed", magnitude);
    }

}
