using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CapsuleCollider))]
public class Gem : MonoBehaviour
{
    private string playerTag = "Player";
    public int point;

    [SerializeField, HideInInspector]
    private GameObject effectPrefab;


    public void DestroyGem() {
        GetComponent<CapsuleCollider>().enabled = false;

        Destroy(gameObject, 1.0f);

        // TODO アニメーション
        transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack)
            .OnComplete(() => {

                // TODO エフェクト
                if (effectPrefab) {
                    GameObject effect = Instantiate(effectPrefab, new(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
                    Destroy(effect, 1.0f);
                }
            })
            .SetLink(gameObject);
    }


    private void OnTriggerEnter(Collider other) {

        if (other.TryGetComponent(out ScoreManager scoreManager)) {
            Debug.Log("プレイヤー侵入");
            scoreManager.AddScore(point);
            Destroy(gameObject, 1.0f);
        }


        //if (other.CompareTag(playerTag)) {
        //    //Debug.Log("プレイヤー侵入");

        //    Destroy(gameObject, 1.0f);
        //}
    }
}
