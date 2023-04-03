using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text txtScore;

    [SerializeField] private Text txtTime;

    [SerializeField] private Ease easeScore = Ease.Linear;
    
    private int prevScore;


    void Start() {
        Reset();    
    }
    
    void Reset() {
        prevScore = 0;
    }

    /// <summary>
    /// スコアの表示更新
    /// </summary>
    /// <param name="score"></param>
    public void UpdateDisplayScore(int score) {
        //txtScore.text = score.ToString();

        txtTime.DOCounter(prevScore, score, 0.5f).SetEase(easeScore);
        
        prevScore = score;
    }

    /// <summary>
    /// ゲーム時間の表示更新
    /// </summary>
    /// <param name="time"></param>
    public void UpdateDisplayTime(int time) {
        txtTime.text = time.ToString();
    }

}
