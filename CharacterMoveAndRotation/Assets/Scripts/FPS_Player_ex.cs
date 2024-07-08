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
        //eulerAngles = ���Ͱ��� �޾� ȸ���Ѵ�.
    }

    private void Move()
    {
        //Input.GetKey��� �ϸ� A�� �ް� D�� �޾ƾߵǴϱ� Axis�� ���� �������� ��ƿ�
        h = Input.GetAxis("Horizontal");    //Ű���忡�� A(-1) �Ǵ� D(1)�� ������ ���� Ű��(-1 / 1)�� h�� ����
        v = Input.GetAxis("Vertical");      //Ű���忡�� W(1) �Ǵ� S��(-1) ������ ���� Ű��(-1 / 1)�� v�� ���� �ƹ��͵� �ȴ����� 0

        Vector3 Normal = (h * Vector3.right) + (v * Vector3.forward);

        tr.Translate(Normal.normalized * Time.deltaTime * moveSpeed);
        //tr.Translate/*��ǥ �̵� �Լ�*/(Vector3.right/*�����default���� ��ư right�� ���� ������ left�� �ٲ�*/ * h * Time.deltaTime * moveSpeed);
        //tr.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
    }

    void Jump()
    {
        GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
    }
}
