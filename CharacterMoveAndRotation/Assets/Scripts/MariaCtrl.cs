using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaCtrl : MonoBehaviour
{
    [SerializeField] Transform tr;
    [SerializeField] Animator animator;

    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float turnSpeed = 150f;

    float h = 0f, v = 0f, r = 0f;

    void Start()
    {
        tr = transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MariaMoveAndRotation();
        Sprint();
        IdleJump();
        RunningJump();
        IdleAttack();
    }

    private void IdleAttack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && h == 0 && v == 0)
        {
            animator.SetTrigger("IdleAttack");
        }
    }

    private void RunningJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && v > 0.1f/*������ �� ����(W) ����*/)
        {
            animator.SetTrigger("RunningJumpTrigger");
        }
    }

    private void IdleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && h == 0 && v == 0/*���ڸ��� �� ����*/)
        {
            animator.SetTrigger("IdleJumpTrigger");
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 14f;
            animator.SetBool("isSprint", true);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 7f;
            animator.SetBool("isSprint", false);

        }
    }

    private void MariaMoveAndRotation()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        #region �Ʒ� �ڵ�� ����ȭ�� ����ȭ�� �ȵ� �ڵ�
        /*tr.Translate(Vector3.right * h * Time.deltaTime * moveSpeed);
        {
            animator.SetFloat("posX", h, 0.1f, Time.deltaTime);
        }
        tr.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        {
            animator.SetFloat("posY", v, 0.1f, Time.deltaTime);
        }*/
        #endregion
        #region ����ȭ �ڵ�
        Vector3 moveDir = (Vector3.right * h) + (Vector3.forward * v);

        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        {
            animator.SetFloat("posX", h, 0.01f, Time.deltaTime);
            animator.SetFloat("posY", v, 0.01f, Time.deltaTime);
        }
        #endregion

        tr.Rotate(Vector3.up * r * Time.deltaTime * turnSpeed);
    }
}
