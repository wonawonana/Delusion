using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationeventtest : MonoBehaviour
{
        [SerializeField] private float speed; // 걷기 스피드.
    private Vector3 direction; // 방향.
        // 상태변수
    private bool isCatched; // 행동중인지 아닌지 판별.
    private bool isRunning; // 걷는지 안 걷는지 판별.
    private bool Collision; // 걷는지 안 걷는지 판별.
    private bool isAction; // 행동중인지 아닌지 판별.
    private bool beenCatched; // 행동중인지 아닌지 판별.
        // 필요한 컴포넌트
    [SerializeField] private float waitTime; // 대기 시간.
        [SerializeField] private float walkTime; // 대기 시간.
        private float currentTime;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
    [SerializeField] private float ani_startpoint;
    private void OnCollisionEnter (Collision target)
    {
        if(target.gameObject.name=="Player")
        {
            //Debug.Log(target.gameObject.name + " : 충돌  시작");
            isAction=false;
            isCatched=true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isCatched=false;
        isRunning=false;
        Collision=false;
        isAction=false;
        beenCatched=false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject person=GameObject.Find("Player");
        if(person.transform.position.z>ani_startpoint){
            ElapseTime();
            //rigid.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime)); 
            if(isAction && PlayerController.cat!=true)
            {
                Vector3 vec=person.transform.position;
                vec.z=vec.z+0.5f;
                vec.y=transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position,  vec,  Time.deltaTime * speed);
            }
            //Debug.Log(transform.position);
            Run();
            Catch();
        }
    }
    private void ElapseTime()
    {

            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && beenCatched!=true)
                {
                    isRunning=true;
                    isAction=true;
                }
                else{
                    isRunning=false;
                }
        
    }
    private void Run()
    {
        if (isRunning)
            {
                currentTime = walkTime;
                //rigid.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));           
                anim.SetTrigger("Run");

                Debug.Log("Run");
            }
    }
    private void Catch()
    {
        if (isCatched)
        {
            isRunning=false;
            /*
             Vector3 vec=transform.position;
             vec.z=vec.z+0.5f;
            transform.position = Vector3.MoveTowards(transform.position,  vec,  Time.deltaTime * speed);
            */
            GameObject person=GameObject.Find("Player");
            

            Vector3 direction = person.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            newRotation.y=180;
            Debug.Log(newRotation);
            Quaternion newRotation1=newRotation;
            transform.rotation = newRotation;
            //Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);

            //rigid.MoveRotation(Quaternion.Euler(_rotation));
            anim.SetTrigger("Catch");
            Debug.Log("Catch");
            isCatched=false;
            beenCatched=true;
        }
    }

}