using System.Collections;
using UnityEngine;

public class TimeManagerUpdate : MonoBehaviour
{
    [SerializeField] 
    private int gameTime;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    private float timer;
    
    
    IEnumerator Start() {
        // 初期設定
        currentTime = gameTime;
        uiManager.UpdateDisplayTime(currentTime);
    }
    
    void Update() {
        timer += Time.deltaTime;

        if (timer >= 1f) // 1秒経過したら
        {
            timer = 0; // タイマーをリセット
            currentTime--; // カウントを1つ減らす

            if (currentTime <= 0)
            {
                Debug.Log("Countdown finished!");
                this.enabled = false; // このスクリプトを無効化
            }
            else
            {
                uiManager.UpdateDisplayTime(currentTime);
                Debug.Log($"Current count: {currentTime}");
            }
        }
    }
}
