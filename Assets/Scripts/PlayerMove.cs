using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private Rigidbody rb;
    private PlayerAnimation playerAnim;

    private float moveX;
    private float moveZ;

    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out playerAnim);
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        Move();    
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move() {
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ) * moveSpeed;

        if (playerAnim && rb.velocity != Vector3.zero) {
            playerAnim.PlayWalkAnim(rb.velocity.sqrMagnitude);
        }

        // 移動している場合
        if (rb.velocity.normalized != Vector3.zero) {
            // 移動方向にキャラの向きを合せる
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
        }


        // こっちでも OK
        //// カメラの方向から、X-Z平面の単位ベクトルを取得
        //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //Debug.Log(cameraForward); // (0,0,1)

        //// 方向キーの入力値とカメラの向きから、移動方向を決定
        //Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x; // (0,0,1) * z + (1,0,0) * x
        //Debug.Log(moveForward);  // キー入力なし(0,0,0)

        //if (moveForward != Vector3.zero) {
        //    transform.rotation = Quaternion.LookRotation(moveForward);
        //}
    }

    /// <summary>
    /// 現在の移動速度の取得
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMoveVelocity() {
        return new Vector2(rb.velocity.x, rb.velocity.z);
    }

    /// <summary>
    /// 現在の移動速度の取得用の戻り値にタプル利用
    /// </summary>
    /// <returns></returns>
    public (float moveX, float moveZ) GetMoveVelocityTupple() {
        return (rb.velocity.x, rb.velocity.z);
    }
}
