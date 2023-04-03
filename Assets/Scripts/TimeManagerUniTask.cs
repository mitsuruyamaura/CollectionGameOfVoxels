using UnityEngine;
using Cysharp.Threading.Tasks;

public class TimeManagerUniTask : MonoBehaviour
{
    [SerializeField] 
    private int initialTime;
    
    [SerializeField]
    private UIManager uiManager;

    private int currentTime;
    
    
    void Reset() {
        // 後で FindAnyObjectByType<>() に置き換える
        uiManager = FindObjectOfType<UIManager>();
    }
    
    async void Start()
    {
        currentTime = initialTime;
        uiManager.UpdateDisplayTime(currentTime);

        while (currentTime > 0) {
            await UniTask.Delay(1000);

            currentTime--;
            uiManager.UpdateDisplayTime(currentTime);
            Debug.Log($"Current count: {currentTime}");
        }
        
        Debug.Log("Countdown finished!");
    }
}
