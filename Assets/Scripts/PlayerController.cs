using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;


    // 상태 변수
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;
    public static bool cat = false;
    public static bool isEnding = false;
    public static bool is5KeyPicked = false;
    public static bool isRoom1Book = false;
    public static bool isRoom2Book = false;
    public static bool isRoom3Book = false;
    float timer;

    // 앉았을 때 얼마나 앉을지 결정하는 변수.
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    // 땅 착지 여부
    private CapsuleCollider capsuleCollider;


    // 민감도
    [SerializeField]
    private float lookSensitivity;


    // 카메라 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;


    //필요한 컴포넌트
    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;
    

    //문여는
    public float interactDiastance = 3f;

    // Use this for initialization
    void Start()
    {
        timer = 0.0f;
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

        // 초기화.
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }




    // Update is called once per frame
    void Update()
    {
        if (cat == false)
        {
            IsGround();
            TryJump();
            TryRun();
            TryCrouch();
            Move();
          


            Vector3 forward = transform.TransformDirection(Vector3.forward) * 1f;
            Debug.DrawRay(transform.position, forward, Color.green);


            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(ray, out hit, interactDiastance))
                {
                    Debug.Log(hit.collider.tag);
                    if (hit.collider.tag == "Door")
                    {

                        hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
                    }

                    if (hit.collider.tag == "Cabinet")
                    {
                        is5KeyPicked = true;
                       
                    }
                    if (hit.collider.tag == "Book")
                    {
                        isRoom1Book = true;
                    }

                    if (hit.collider.tag == "Book2")
                    {
                        isRoom2Book = true;
                    }

                    if (hit.collider.tag == "Book3")
                    {
                        isRoom3Book = true;
                    }

                }

            }
        }
        else
        {
            if (timer < 100.0f) timer += Time.deltaTime;
            if (timer > 2.0f)
            {
                isEnding = true;
                timer = 100.0f;
            }
        }

        CameraRotation();
        CharacterRotation();
    }

    // 앉기 시도
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }


    // 앉기 동작
    private void Crouch()
    {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());

    }

    // 부드러운 동작 실행.
    IEnumerator CrouchCoroutine()
    {

        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }


    // 지면 체크.
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }


    // 점프 시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }


    // 점프
    private void Jump()
    {

        // 앉은 상태에서 점프시 앉은 상태 해제.
        if (isCrouch)
            Crouch();

        myRigid.velocity = transform.up * jumpForce;
    }


    // 달리기 시도
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    // 달리기 실행
    private void Running()
    {
        if (isCrouch)
            Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }


    // 달리기 취소
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }


    // 움직임 실행
    private void Move()
    {

        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // 좌우 캐릭터 회전
    private void CharacterRotation()
    {

        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }



    // 상하 카메라 회전
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.name == "mannequin_02")
        {
            Debug.Log(target.gameObject.name + " : 충돌  시작");
            cat = true;
            GameObject ghost = GameObject.Find("mannequin_02");
            Vector3 direction = ghost.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            transform.rotation = newRotation;

        }
    }

}
