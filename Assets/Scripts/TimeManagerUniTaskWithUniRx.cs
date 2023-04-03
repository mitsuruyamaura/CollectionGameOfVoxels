using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class TimeManagerUniTaskWithUniRx : MonoBehaviour
{
    [SerializeField] 
    private int initialTime = 60;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    
    private IDisposable subscription;   // System.IDisposable
    
    
    void Reset() {
        // 後で FindAnyObjectByType<>() に置き換える
        uiManager = FindObjectOfType<UIManager>();
    }
    
    void Start()
    {
        currentTime = initialTime;
        uiManager.UpdateDisplayTime(currentTime);

        var timer = Observable.Interval(TimeSpan.FromSeconds(1.0f));

        subscription = timer.Subscribe(async _ =>
        {
            await UniTask.Yield();
            currentTime--;
            uiManager.UpdateDisplayTime(currentTime);
            Debug.Log($"Current count: {currentTime}");
            
            if (currentTime <= 0)
            {
                Debug.Log("Countdown finished!");
                subscription.Dispose(); // イベントの購読を解除
            }
        });

        subscription.AddTo(this);
    }

    private void OnDestroy() {
        subscription?.Dispose();
    }
}
