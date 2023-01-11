using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class EventTriggerTest : MonoBehaviour
{
    private ObservableEventTrigger eventTrigger;
    //private SingleAssignmentDisposable disposable = new();　//　こっちは UniRx に含まれているので System の宣言不要

    private IDisposable disposable;

    void Start()
    {
        if (TryGetComponent(out eventTrigger)) {
            //disposable.Disposable = eventTrigger.OnPointerDownAsObservable()
            //    .Subscribe(_ => Debug.Log("A"))
            //    .AddTo(this);


            disposable = eventTrigger.OnPointerDownAsObservable()
                .Subscribe(_ => Debug.Log("A"))
                .AddTo(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Space 押下");
            SetNewEvent();
        }
    }

    /// <summary>
    /// 現在の購読処理を停止し、新しい購読処理を開始する
    /// </summary>
    private void SetNewEvent() {

        disposable.Dispose();

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ => Debug.Log("B"))
            .AddTo(this);
    }
}
