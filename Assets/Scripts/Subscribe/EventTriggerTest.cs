using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class EventTriggerTest : MonoBehaviour
{
    private ObservableEventTrigger eventTrigger;
    //private SingleAssignmentDisposable disposable = new();�@//�@�������� UniRx �Ɋ܂܂�Ă���̂� System �̐錾�s�v

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
            Debug.Log("Space ����");
            SetNewEvent();
        }
    }

    /// <summary>
    /// ���݂̍w�Ǐ������~���A�V�����w�Ǐ������J�n����
    /// </summary>
    private void SetNewEvent() {

        disposable.Dispose();

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ => Debug.Log("B"))
            .AddTo(this);
    }
}
