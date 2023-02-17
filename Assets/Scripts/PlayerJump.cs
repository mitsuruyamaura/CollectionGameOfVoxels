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
    // private Rigidbody rb;
    // private CapsuleCollider capsuleCol;

    [SerializeField] private float jumpPower = 3.0f;
    //private bool isJump = false;

    private void Reset() {
         if (!TryGetComponent(out agent)) {
             Debug.Log("NavMeshAgent 取得出来ませんでした。");
         }
    
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

        this.UpdateAsObservable()
            .Where(_ => agent.enabled)  // 重複ジャンプ防止
            //.Where(_ => !isJump)
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ =>
            {
                //isJump = true;
                
                // NavMesh を切らないと柵や岩などを飛び越せず前の位置に戻る。建物の場合にはいいが、飛び越えられそうなものも飛び越せなくなる
                agent.enabled = false;
                float y = transform.position.y;
                
                // その場合、大きな建物やステージ外側にコライダーつけて、isTrigger オフで対応
                //    =>  ステージ外については SamplePosition で対応可能
                
                //capsuleCol.isTrigger = false;
                Sequence sequence = DOTween.Sequence();

                // Rigidbody だと NavMesh 下にあるので、NavMesh をオフにしないとジャンプしない

                sequence.Append(transform.DOJump(new Vector3(Input.GetAxis("Horizontal") * 7, 3.0f, Input.GetAxis("Vertical") * 7), jumpPower, 1, 0.5f)
                    .SetRelative());
                //sequence.Append(transform.DOMoveY(3.0f, 0.5f).SetEase(Ease.InQuad).SetRelative());

                // こっちは SetRelative いらない。
                sequence.Append(transform.DOMoveY(y, 0.25f)
                    .SetEase(Ease.OutQuad)).OnComplete(() =>
                {
                    agent.enabled = true;
                    
                    // NavMesh のエリア外(ステージの外側)に出ていないか判定
                    if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                    {
                        // エリア外の場合には最も近い位置 NavMesh の位置に再移動して位置補正
                        transform.position = hit.position;
                    }
                });
                
                // .OnComplete(() =>
                // {
                //     //rb.useGravity = true;
                //     //DOVirtual.DelayedCall(0.5f, () => agent.enabled = true);
                //     //rb.DOMoveY(y, 0.5f).SetEase(Ease.OutQuad).SetRelative().OnComplete(() => agent.enabled = true);
                //     //agent.enabled = true;
                // });
            });
    }
}
