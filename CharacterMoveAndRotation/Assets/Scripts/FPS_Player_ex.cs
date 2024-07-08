using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player_ex : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    float h = 0f, v = 0f;

    [SerializeField]
    private Transform tr;

    [Header("Mouse Ratation")]
    public float x_Sensitivity = 100f;
    public float y_Sensitivity = 100f;
    public float x_MinLimit = -360f;
    public float y_MinLimit = -45f;
    public float x_MaxLimit = 360f;
    public float y_MaxLimit = 45f;
    [SerializeField]
    private float x_Rot = 0.0f;
    private float y_Rot = 0.0f;
    private float jumpForce = 5.5f;
    [SerializeField]
    bool isJump;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
    }

    void Update()
    {
        Move();
        Rotation();
        Run();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump) return;

            Jump();
            isJump = true;
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 10f;
        }
    }

    private void Rotation()
    {
        x_Rot += Input.GetAxis("Mouse X") * x_Sensitivity * Time.deltaTime;
        y_Rot += Input.GetAxis("Mouse Y") * y_Sensitivity * Time.deltaTime;
        y_Rot = Mathf.Clamp(y_Rot, y_MinLimit, y_MaxLimit);
        tr.eulerAngles = new Vector3(-y_Rot, x_Rot, 0f);
        //eulerAngles = 백터값을 받아 회전한다.
    }

    private void Move()
    {
        //Input.GetKey라고 하면 A도 받고 D도 받아야되니까 Axis로 수평 수직으로 잡아옴
        h = Input.GetAxis("Horizontal");    //키보드에서 A(-1) 또는 D(1)를 누르는 중인 키값(-1 / 1)을 h에 대입
        v = Input.GetAxis("Vertical");      //키보드에서 W(1) 또는 S를(-1) 누르는 중인 키값(-1 / 1)을 v에 대입 아무것도 안누르면 0

        Vector3 Normal = (h * Vector3.right) + (v * Vector3.forward);

        tr.Translate(Normal.normalized * Time.deltaTime * moveSpeed);
        //tr.Translate/*좌표 이동 함수*/(Vector3.right/*양수가default인지 암튼 right고 음수 들어오면 left로 바뀜*/ * h * Time.deltaTime * moveSpeed);
        //tr.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
    }

    void Jump()
    {
        GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
    }
}
