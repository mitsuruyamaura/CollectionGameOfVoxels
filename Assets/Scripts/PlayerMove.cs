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
    /// �ړ�
    /// </summary>
    private void Move() {
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ) * moveSpeed;

        if (playerAnim && rb.velocity != Vector3.zero) {
            playerAnim.PlayWalkAnim(rb.velocity.sqrMagnitude);
        }

        // �ړ����Ă���ꍇ
        if (rb.velocity.normalized != Vector3.zero) {
            // �ړ������ɃL�����̌�����������
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
        }


        // �������ł� OK
        //// �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //Debug.Log(cameraForward); // (0,0,1)

        //// �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        //Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x; // (0,0,1) * z + (1,0,0) * x
        //Debug.Log(moveForward);  // �L�[���͂Ȃ�(0,0,0)

        //if (moveForward != Vector3.zero) {
        //    transform.rotation = Quaternion.LookRotation(moveForward);
        //}
    }

    /// <summary>
    /// ���݂̈ړ����x�̎擾
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMoveVelocity() {
        return new Vector2(rb.velocity.x, rb.velocity.z);
    }

    /// <summary>
    /// ���݂̈ړ����x�̎擾�p�̖߂�l�Ƀ^�v�����p
    /// </summary>
    /// <returns></returns>
    public (float moveX, float moveZ) GetMoveVelocityTupple() {
        return (rb.velocity.x, rb.velocity.z);
    }
}
