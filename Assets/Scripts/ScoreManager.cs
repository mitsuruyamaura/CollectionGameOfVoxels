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
    /// �X�R�A�̉��Z
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount) {

        totalPoint += amount;
        Debug.Log("�X�R�A���v�l : " + totalPoint);

        gemCount++;
        Debug.Log("��΂̊l���� : " + gemCount + " ��");

        uiManager.UpdateDisplayScore(totalPoint);
    }
}
