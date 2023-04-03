using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManagerCoroutine : MonoBehaviour
{
    [SerializeField] 
    private int initialTime = 60;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    
    
    void Reset() {
        // 後で FindAnyObjectByType<>() に置き換える
        uiManager = FindObjectOfType<UIManager>();
    }
    
    // while と Start をコルーチン化の学習
    IEnumerator Start() {
        // 初期設定
        currentTime = initialTime;
        uiManager.UpdateDisplayTime(currentTime);
        
        // 時間経過の監視
        while (currentTime > 0) {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
            uiManager.UpdateDisplayTime(currentTime);
            Debug.Log($"Current count: {currentTime}");
        }
        
        Debug.Log("Countdown finished!");
    }
}
