using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 4;
    public float runSpeed = 6;
    Camera characterCamera;
    Vector3 targetPos;
    private Animator animator;

    [SerializeField]
    private float mouseSensi;
    private Rigidbody myRigid;
    private void Awake()
    {
        characterCamera = GetComponentInChildren<Camera>();
    }
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        LookMouse();
    }
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f;

        animator.SetFloat("Horizontal", _moveDirX * offset);
        animator.SetFloat("Vertical", _moveDirZ * offset);
        float applySpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxisRaw("Sprint"));
        Vector3 moveVecter = new Vector3(_moveDirX, 0f, _moveDirZ);
        //transform.Translate(new Vector3(_moveDirX, 0f, _moveDirZ).normalized * Time.deltaTime * 5);
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    public void LookMouse()
    {
        if(Input.GetMouseButton(1))
        {
            Ray ray = characterCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            RaycastHit hitResult;
            if (Physics.Raycast(ray, out hitResult,10000f))
            {
                targetPos = hitResult.point;
                //Vector3 mouseDir = new Vector3(hitResult.point.x, 0, hitResult.point.z) - transform.position;
                //gameObject.transform.forward = mouseDir;
            }
        }
        Turn(targetPos);

    }
    public void Turn(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
    }
}
