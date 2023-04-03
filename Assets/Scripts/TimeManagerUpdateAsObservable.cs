using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TimeManagerUpdateAsObservable : MonoBehaviour
{
    [SerializeField] 
    private int initialTime = 60;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    
    private float timer;
    
    
    void Reset() {
        // 後で FindAnyObjectByType<>() に置き換える
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        currentTime = initialTime;
        timer = 0;
        uiManager.UpdateDisplayTime(currentTime);
        
        // UpdateAsObservableを使用して、毎フレーム処理を実行
        this.UpdateAsObservable()
            .Where(_ => currentTime > 0)
            .Subscribe(_ =>
            {
                timer += Time.deltaTime; // タイマーに経過時間を加算

                if (timer >= 1f) // 1秒経過したら
                {
                    timer = 0; // タイマーをリセット
                    currentTime--; // カウントを1つ減らす
                    uiManager.UpdateDisplayTime(currentTime);
                    Debug.Log($"Current count: {currentTime}");
                    
                    if (currentTime <= 0)
                    {
                        Debug.Log("Countdown finished!");
                        this.enabled = false; // このスクリプトを無効化
                    }
                }
            })
            .AddTo(this); // ゲームオブジェクトが破棄された時に自動的にイベントの購読を解除
    }
}
