using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rigid;
    private Animator ani;
    private bool isStandBy;
    private float exitTime = 0.9f;
    Vector3 moveDir;
    public float maxVelocityX, maxVelocityZ;

    void Start() {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        isStandBy = false;
    }
    
    void Update()
    {
        DroneMove();
    }

    void FixedUpdate() {
        Debug.DrawRay(rigid.position, moveDir * 1.0f, Color.red);
    }

    void DroneMove(){
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        if(inputX == 0 && inputZ == 0){
            ani.SetBool("isHover",false);
        }
        else{
            ani.SetBool("isHover", true);
        }
        Vector3 velocity = new Vector3(inputX, 0 , inputZ) * speed;
        if(isStandBy){
            // if(Input.GetKey(KeyCode.R)){
                
            // }
            if(Input.GetKey(KeyCode.E)){
                transform.Rotate(new Vector3(0, 60f, 0) * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.Q)){
                transform.Rotate(new Vector3(0, -60f, 0) * Time.deltaTime);
            }
            if(rigid.velocity.x > maxVelocityX){
                rigid.velocity = new Vector3(maxVelocityX, 0, rigid.velocity.z);
            }
            if(rigid.velocity.x < (maxVelocityX * -1)){
                rigid.velocity = new Vector3((maxVelocityX * -1), 0, rigid.velocity.z);
            }
            if(rigid.velocity.z > maxVelocityZ){
                rigid.velocity = new Vector3(rigid.velocity.x, 0, maxVelocityZ);
            }
            if(rigid.velocity.z < (maxVelocityZ * -1)){
                rigid.velocity = new Vector3(rigid.velocity.x, 0, (maxVelocityZ * -1));
            }
            rigid.AddRelativeForce(velocity);
            Debug.Log("x힘 : " + rigid.velocity.x + " z힘 : " + rigid.velocity.z);
        }
        if(!ani.GetBool("isTakeOff")){
            if(Input.GetKeyUp(KeyCode.R)){
                ani.SetBool("isTakeOff",true);
                ani.SetBool("isLanding",false);
                ani.SetBool("isStart",true);
                StartCoroutine(CheckAnimationState());
            }
        }
        else{
            if(Input.GetKeyUp(KeyCode.R) && !ani.GetBool("isHover")){
                ani.SetBool("isTakeOff",false);
                ani.SetBool("isLanding",true);
                isStandBy = false;
                ani.SetBool("isStart",false);
            }
        }
    }

    IEnumerator CheckAnimationState(){
        while(!ani.GetCurrentAnimatorStateInfo(0).IsName("Blue_Take_Off_Anim_1")){
            yield return null;
        }

        while(ani.GetCurrentAnimatorStateInfo(0).normalizedTime < exitTime){
            yield return null;
        }
        isStandBy = true;
    }
}
