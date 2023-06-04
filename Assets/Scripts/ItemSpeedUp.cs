using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class ItemSpeedUp : MonoBehaviour, IItemEffect
{
    [SerializeField] private float speedBoostAmount = 0;
    [SerializeField] private float duration = 5.0f;
    [SerializeField] private BoxCollider boxCol;
    [FormerlySerializedAs("light")] [SerializeField] private GameObject lightObj;
    
    
    protected virtual void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.TryGetComponent(out PlayerMove playerMove)) {
            boxCol.enabled = false;
            //lightObj.SetActive(false);
            
            
            
            StartCoroutine(SpeedBoost(playerMove));
        }
    }
    
    private IEnumerator SpeedBoost(PlayerMove playerMove) {
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
        
        // サイズを 0 にしても Light は残るので、演出に使う
        transform.SetParent(playerMove.gameObject.transform);
        transform.localPosition = new(0, transform.position.y, 0);
        
        playerMove.MoveSpeed += speedBoostAmount;
        yield return new WaitForSeconds(duration);
        playerMove.MoveSpeed -= speedBoostAmount;
        
        Destroy(gameObject);
    }
}
