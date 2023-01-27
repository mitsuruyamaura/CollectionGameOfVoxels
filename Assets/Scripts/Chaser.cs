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

    [SerializeField] private float moveSpeed = 3.5f;
    private bool isChasing = true;

    
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
        
        //Debug.Log("距離" + (transform.position - searchArea.SearchTarget.position).sqrMagnitude);

        if (!isChasing) {
            return;
        }
        
        // 近づいたらスピードアップ
        if ((transform.position - searchArea.SearchTarget.position).sqrMagnitude < 30.0f) {
            agent.speed = moveSpeed * 2f;
        } else {
            agent.speed = moveSpeed;
        }
        
        // 追跡対象の位置設定
        agent.SetDestination(searchArea.SearchTarget.position);
    }

    /// <summary>
    /// 追跡停止
    /// </summary>
    public void StopMove() {
        agent.speed = 0;
        agent.ResetPath();
        isChasing = false;
    }

    /// <summary>
    /// 追跡できる状態に戻す
    /// </summary>
    public void ResumeMove() {
        isChasing = true;
    }
}
