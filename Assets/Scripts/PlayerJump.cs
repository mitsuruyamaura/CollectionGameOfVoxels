using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;

// NavMeshComponent の記事。動く床の動的生成。NavMesh 付のプレハブの作り方など
// https://note.com/k1togami/n/n2a678ccf426c

public class PlayerJump : MonoBehaviour
{
    private NavMeshAgent agent;
    
    // 不要
    // private Rigidbody rb;
    // private CapsuleCollider capsuleCol;

    [SerializeField] private float jumpPower = 3.0f;

    
    private void Reset() {
         if (!TryGetComponent(out agent)) {
             Debug.Log("NavMeshAgent 取得出来ませんでした。");
         }
    
         // 不要
         // if (!TryGetComponent(out rb)) {
         //     Debug.Log("Rigidbody 取得出来ませんでした。");
         // }
         //
         // if (!TryGetComponent(out capsuleCol)) {
         //     Debug.Log("CapsuleCollider 取得出来ませんでした。");
         // }
    }

    void Start() {
        Reset();

        // UniRx 正常動作確認済
        // this.UpdateAsObservable()
        //     .Where(_ => agent.enabled)  // 重複ジャンプ防止
        //     .Where(_ => Input.GetKeyDown(KeyCode.Space))
        //     .Subscribe(_ =>
        //     {
        //         // NavMesh を切らないと柵や岩などを飛び越せず前の位置に戻る。建物の場合にはいいが、飛び越えられそうなものも飛び越せなくなる
        //         agent.enabled = false;
        //         float y = transform.position.y;
        //         
        //         // その場合、大きな建物やステージ外側にコライダーつけて、isTrigger オフで対応
        //         //    =>  ステージ外については SamplePosition で対応可能
        //         //capsuleCol.isTrigger = false;  // 現状は使っていない
        //
        //         Sequence sequence = DOTween.Sequence();
        //
        //         // Rigidbody だと NavMesh 下にあるので、NavMesh をオフにしないとジャンプしない
        //
        //         sequence.Append(transform.DOJump(new Vector3(Input.GetAxis("Horizontal") * 7, 3.0f, Input.GetAxis("Vertical") * 7), jumpPower, 1, 0.5f)
        //             .SetRelative());
        //
        //         // こっちは SetRelative いらない。
        //         sequence.Append(transform.DOMoveY(y, 0.25f)
        //             .SetEase(Ease.OutQuad)).OnComplete(() =>
        //         {
        //             agent.enabled = true;
        //             
        //             // NavMesh のエリア外(ステージの外側)に出ていないか判定
        //             if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        //             {
        //                 // エリア外の場合には最も近い位置 NavMesh の位置に再移動して位置補正
        //                 transform.position = hit.position;
        //             }
        //         });
        //         
        //         // ここは不要
        //         //.OnComplete(() =>
        //         //{
        //         //    rb.useGravity = true;
        //         //    DOVirtual.DelayedCall(0.5f, () => agent.enabled = true);
        //         //    rb.DOMoveY(y, 0.5f).SetEase(Ease.OutQuad).SetRelative().OnComplete(() => agent.enabled = true);
        //         //    agent.enabled = true;
        //         //    });
        //     });
    }


    void Update() {
        // 重複ジャンプ防止
        if (!agent.enabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump() {
        agent.enabled = false;
        float y = transform.position.y;

        Sequence sequence = DOTween.Sequence();
        // ジャンプ
        sequence.Append(transform.DOJump(new Vector3(Input.GetAxis("Horizontal") * 7, 3.0f, Input.GetAxis("Vertical") * 7), jumpPower, 1, 0.5f)
                .SetRelative());

        // 落下。こっちは SetRelative いらない。
        sequence.Append(transform.DOMoveY(y, 0.25f)
                .SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    // 再ジャンプ可能
                    agent.enabled = true;

                    // NavMesh のエリア外(ステージの外側)に出ていないか判定
                    if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
                        // エリア外の場合には最も近い位置 NavMesh の位置に再移動して位置補正
                        transform.position = hit.position;
                }
            });
    }
}
