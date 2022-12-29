using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ViewChanger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera[] cameras;
    private int index;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) {
            ChangeView();
        }
    }


    private void ChangeView() {
        index = ++index % cameras.Length; 
        for (int i = 0; i < cameras.Length; i++) {
            cameras[i].Priority = index == i ? 10 : 5;
        }
    }
}