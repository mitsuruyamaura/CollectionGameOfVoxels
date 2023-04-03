using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManagerUpdate : MonoBehaviour
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
    
    void Start() {
        // 初期設定
        currentTime = initialTime;
        uiManager.UpdateDisplayTime(currentTime);
    }
    
    void Update() {
        timer += Time.deltaTime;

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
    }
}
