using System.Collections;
using UnityEngine;

public class ItemSpeedUp : MonoBehaviour, IItemEffect
{
    [SerializeField] private float speedBoostAmount = 0;
    [SerializeField] private float duration = 5.0f;
    
    
    public virtual void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerMove playerMove)) {
            StartCoroutine(SpeedBoost(playerMove));
        }
    }
    
    private IEnumerator SpeedBoost(PlayerMove playerMove)
    {
        gameObject.SetActive(false);
        playerMove.MoveSpeed += speedBoostAmount;
        yield return new WaitForSeconds(duration);
        playerMove.MoveSpeed -= speedBoostAmount;
        
        Destroy(gameObject);
    }
}
