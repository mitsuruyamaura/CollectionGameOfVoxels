using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //[SerializeField]
    private int totalPoint;

    //[SerializeField]
    private int gemCount;

    [SerializeField]
    private UIManager uiManager;

    //private void OnTriggerEnter(Collider other) {

    //    if (other.TryGetComponent(out Gem gem)) {
    //        totalPoint += gem.point;
    //        gemCount++;

    //        gem.DestroyGem();
    //    }
    //}

    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount) {

        totalPoint += amount;
        Debug.Log("スコア合計値 : " + totalPoint);

        gemCount++;
        Debug.Log("宝石の獲得数 : " + gemCount + " 個");

        uiManager.UpdateDisplayScore(totalPoint);
    }
}
