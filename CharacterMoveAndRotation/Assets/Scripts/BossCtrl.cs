using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    [SerializeField] Transform tr;
    [SerializeField] Animator ani;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 150f;

    float h = 0f, v = 0f, r = 0f;
    void Start()
    {
        tr = transform;
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        BossMoveAndRotation();
        Sprint();
        IdleJump();
        RunJump();
        Attack();

        if (Input.GetKeyDown(KeyCode.F))
        {
            ani.SetTrigger("Flair");
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ani.SetTrigger("Attack");
        }
    }

    private void RunJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && v > 0.1f)
        {
            ani.SetTrigger("RunningJumpTrigger");
        }
    }

    private void IdleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && h == 0 && v == 0)
            ani.SetTrigger("IdleJumpTrigger");
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 14f;
            ani.SetBool("isSprint", true);
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
            ani.SetBool("isSprint", false);
        }
    }

    private void BossMoveAndRotation()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.right * h) + (Vector3.forward * v);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime); ;
        {
            ani.SetFloat("posX", h, 0.01f, Time.deltaTime);
            ani.SetFloat("posY", v, 0.01f, Time.deltaTime);
        }

        tr.Rotate(Vector3.up * r * Time.deltaTime * turnSpeed);
    }
}
