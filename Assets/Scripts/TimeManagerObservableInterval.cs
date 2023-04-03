using System;
using UnityEngine;
using UniRx;

public class TimeManagerObservableInterval : MonoBehaviour
{
    [SerializeField] 
    private int initialTime;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    private IDisposable subscription;   // System.IDisposable
    

    void Start() {
        currentTime = initialTime;
        uiManager.UpdateDisplayTime(currentTime);
        
        // var は IObservable<long>
        var timer = Observable.Interval(TimeSpan.FromSeconds(1.0f));

        subscription = timer.Subscribe(_ =>
        {
            currentTime--;
            uiManager.UpdateDisplayTime(currentTime);
            
            if (currentTime <= 0)
            {
                Debug.Log("Countdown finished!");
                subscription.Dispose(); // イベントの購読を解除
            }
            else
            {
                Debug.Log($"Current count: {currentTime}");
            }
        });
            
        subscription.AddTo(this);
    }
    
    private void OnDestroy()
    {
        // ?オペレーターは、null条件演算子と呼ばれ、nullでない場合のみ後続のメソッドやプロパティにアクセスします。これにより、null参照エラーを回避できます。
        
        // 購読している場合のみ購読停止
        subscription?.Dispose();
    }
}
