using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out IPlayer player)) {
            PrepareApplyEffect();
        }
    }

    protected virtual void PrepareApplyEffect() {
        
    }
}