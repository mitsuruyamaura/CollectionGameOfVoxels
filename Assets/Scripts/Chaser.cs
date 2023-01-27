using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Chaser : MonoBehaviour
{
    private NavMeshAgent agent;
    private SearchArea searchArea;

    private void Reset() {

        Debug.Log(TryGetComponent(out agent) ? "NavMeshAgent 取得しました。" : "NavMeshAgent 取得出来ませんでした。");
        
        if (!transform.GetChild(8).TryGetComponent(out searchArea)) {
            Debug.Log("SearchArea 取得出来ませんでした。");
        }
        else {
            Debug.Log("SearchArea 取得しました。");
        }
    }

    void Start()
    { 
        Reset();
    }

    void Update()
    {
        if (!searchArea || !agent) {
            return;
        }
        
        if (!searchArea.SearchTarget) {
            return;
        }

        agent.SetDestination(searchArea.SearchTarget.position);
    }
}
