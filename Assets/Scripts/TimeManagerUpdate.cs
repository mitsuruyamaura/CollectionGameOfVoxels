using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManagerUpdate : MonoBehaviour
{
    [SerializeField] 
    private int initialTime;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    private float timer;
    
    
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
