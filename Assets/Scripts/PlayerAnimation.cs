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

    /// <summary>
    /// 移動アニメと待機アニメの制御
    /// </summary>
    /// <param name="magnitude"></param>
    public void PlayWalkAnim(float magnitude) {
        anim.SetFloat(PlayerAnimationState.Speed.ToString(), magnitude);
    }
}
