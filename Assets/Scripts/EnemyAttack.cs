using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : MonoBehaviour
{
    private SearchArea _searchArea;
    private Chaser _chaser;
    private Rigidbody rb;
    private CapsuleCollider _capsuleCollider;

    [SerializeField] private float knockBackPower = 300.0f;


    private void Reset() {
        transform.GetChild(8).TryGetComponent(out rb);
        transform.GetChild(8).TryGetComponent(out _searchArea);
        TryGetComponent(out _chaser);
        TryGetComponent(out _capsuleCollider);
    }

    void Start()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider collider) {
        if (_capsuleCollider.enabled == false) {
            return;
        }
        
        if (collider.TryGetComponent(out ScoreManager scoreManager)) {
            // スコア半分
            scoreManager.HalfScore();
            
            // ノックバック
            KnockBack(scoreManager.transform);
            
            // 攻撃後の処理
            StartCoroutine(AttackAfter());
        }
    }

    /// <summary>
    /// プレイヤーのノックバック
    /// </summary>
    /// <param name="playerTran"></param>
    /// <returns></returns>
    private void KnockBack(Transform playerTran) {
        Debug.Log("KnockBack");

        // NavMesh で移動していなくても Rigigbody で物理演算で移動させるとバグる。アタッチしている時点で問題がありそう
        // gameObject.AddComponent<Rigidbody>().AddForce((playerTran.position - transform.position).normalized * knockBackPower);
        // Destroy(GetComponent<Rigidbody>());
        
        // ノックバックする方向の設定。追跡者の反対位置にする
        Vector3 dir = (playerTran.position - transform.position).normalized;
        
        // 地面にめり込まないように高さのみ調整
        dir.y = playerTran.position.y;

        // ノックバック
        playerTran.DOMove(dir * knockBackPower, 0.15f)
            .SetRelative() // 今の座標から移動させるので相対値を利用した移動にする
            .SetEase(Ease.InQuad);
    }

    /// <summary>
    /// アタック成功後の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackAfter() {
        // 追跡停止
        _capsuleCollider.enabled = false;
        _searchArea.SearchTarget = null;
        _chaser.StopMove();
        
        yield return new WaitForSeconds(2.0f);

        // 追跡できる状態に戻す
        _capsuleCollider.enabled = true;
        _chaser.ResumeMove();
    }
}
