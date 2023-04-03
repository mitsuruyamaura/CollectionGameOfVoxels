using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TimeManagerUpdateAsObservable : MonoBehaviour
{
    [SerializeField] 
    private int initialTime;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    
    private float timer;
    

    void Start()
    {
        currentTime = initialTime;
        timer = 0;
        uiManager.UpdateDisplayTime(currentTime);
        
        // UpdateAsObservableを使用して、毎フレーム処理を実行
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                timer += Time.deltaTime; // タイマーに経過時間を加算

                if (timer >= 1f) // 1秒経過したら
                {
                    timer = 0; // タイマーをリセット
                    currentTime--; // カウントを1つ減らす
                    uiManager.UpdateDisplayTime(currentTime);
                    
                    if (currentTime <= 0)
                    {
                        Debug.Log("Countdown finished!");
                        this.enabled = false; // このスクリプトを無効化
                    }
                    else
                    {
                        Debug.Log($"Current count: {currentTime}");
                    }
                }
            })
            .AddTo(this); // ゲームオブジェクトが破棄された時に自動的にイベントの購読を解除
    }
}
