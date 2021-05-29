using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPercon : MonoBehaviour
{

   
    CharacterController ch;

    [Range(0, 10)]
    public float speed;
    [Range(0, 10)]
    public float speedPoint;
    [Range(0, 10)]
    public float speedRot;
    [Range(0, 10)]
    public float jumpForce;
    public Animator Anim;
   
    private  float GravityMode;
    [Range(0, 50)]
    public float minusGrav;

    private Vector3 moveVector;

    bool VarMoving;
    public bool second;
    // Start is called before the first frame update 
    void Start()
    {
      
        ch = GetComponent<CharacterController>();
    }
    void Moving()
    {
        if (!VarMoving)
        {
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal");
            moveVector.z = Input.GetAxis("Vertical");
            if (moveVector.x != 0 || moveVector.z != 0) Anim.SetBool("Moving", true); else Anim.SetBool("Moving", false);
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedRot, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
            moveVector.y = GravityMode;
            ch.Move(moveVector * Time.deltaTime * speed);
        }
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
        GamingGravity();
       
        
    }
    public void SecondMoving(bool go,Vector3 point)
    {
        if (go &&!second)
        {
            VarMoving = true;
            Anim.SetBool("Moving", true);
            if (Vector3.Angle(Vector3.forward, point) > 1f || Vector3.Angle(Vector3.forward, point) == 0)
            {
                Vector3 relativePos = point- transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
            }

            Vector3 targetPose = new Vector3(point.x, GravityMode, point.z) - transform.position;

            if (Vector3.Distance(transform.position, point) > 1f) ch.Move(targetPose * speedPoint * Time.deltaTime);
               
            if (Vector3.Distance(transform.position, point) < 1.2f)
            {
                second = true;
                VarMoving = false;
                Anim.SetBool("Moving", false);
            }
        }
    }
    private void GamingGravity()
    {
      
        if(!ch.isGrounded)
        {
            GravityMode -= minusGrav * Time.deltaTime;
        }else  GravityMode = -10; 


        if (Input.GetKeyDown(KeyCode.Space) && ch.isGrounded)
        {
            GravityMode = jumpForce;
            Anim.SetTrigger("Jump");
        }
    }

    
}
