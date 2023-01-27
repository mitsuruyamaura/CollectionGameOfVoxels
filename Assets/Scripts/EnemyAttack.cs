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
            scoreManager.HalfScore();
            StartCoroutine(KnockBack(scoreManager.transform));
        }
    }


    private IEnumerator KnockBack(Transform playerTran) {
        Debug.Log("KnockBack");

        gameObject.AddComponent<Rigidbody>().AddForce((playerTran.position - transform.position).normalized * knockBackPower);
        Destroy(GetComponent<Rigidbody>());

        //playerTran.DOMove((playerTran.position - transform.position).normalized * knockBackPower, 0.15f);
//            .SetEase(Ease.InQuad);
        
        _capsuleCollider.enabled = false;
        _searchArea.SearchTarget = null;
        _chaser.StopMove();
        yield return new WaitForSeconds(2.0f);

        _capsuleCollider.enabled = true;
        _chaser.ReduceMose();
    }
}
