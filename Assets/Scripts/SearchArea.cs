using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    private Transform searchTarget;
    public Transform SearchTarget {
        get => searchTarget;
        set => searchTarget = value;
    }

    private void OnTriggerStay(Collider other) {

        if (searchTarget) {
            return;
        }

        if (other.TryGetComponent(out PlayerMove player)) {
            searchTarget = player.transform;
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out PlayerMove player)) {
            searchTarget = null;
        }
    }
}
