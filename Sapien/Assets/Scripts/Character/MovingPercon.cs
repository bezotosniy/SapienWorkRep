using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPercon : MonoBehaviour
{
    
    GameObject camera;
    CharacterController ch;
    public Transform camera_0y0;

    [Range(0, 10)]
    public float speed;
    [Range(0, 10)]
    public float speedPoint;
    [Range(0, 10)]
    public float speedRot;
    [Range(0, 10)]
    public float jumpForce;
    public Animator Anim;
   [SerializeField]
    private  float GravityMode;
    [Range(0, 50)]
    public float minusGrav;
    [Range(0, 2)]
    public float timeJump;

    private Vector3 moveVector;

    private bool VarMoving = true, MovingBool, isGrounded, jumpClosed = true;
    public bool second;
   
    // Start is called before the first frame update 
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
     
        ch = GetComponent<CharacterController>();
    }
    void Enum()
    {
        switch (camera.transform.eulerAngles.y){ 
            case 0:
                moveVector.x = Input.GetAxis("Horizontal");
                moveVector.z = Input.GetAxis("Vertical");
                break;
            case 90:
                moveVector.z = -Input.GetAxis("Horizontal");
                moveVector.x = Input.GetAxis("Vertical");
                break;
            case 180:
                moveVector.x = -Input.GetAxis("Horizontal");
                moveVector.z = -Input.GetAxis("Vertical");
                break;
            case 270:
                moveVector.z = Input.GetAxis("Horizontal");
                moveVector.x = -Input.GetAxis("Vertical");
                break;} }
    void Moving()
    {
        if (VarMoving)
        {
            moveVector = Vector3.zero;
            if (jumpClosed) Enum();

            if (moveVector.x != 0 || moveVector.z != 0) {
                Anim.SetBool("Moving", true); 
                MovingBool = true; 
            } 
            else {
                Anim.SetBool("Moving", false); 
                MovingBool = false; 
            }

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
        if (go && !second)
        {
            VarMoving = false;
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                second = true;
                VarMoving = true;
            }
            Anim.SetBool("Moving", true);
            if (Vector3.Angle(Vector3.forward, point) > 1f || Vector3.Angle(Vector3.forward, point) == 0)
            {
                Vector3 relativePos = new Vector3(point.x, transform.position.y,point.z)- transform.position;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
            }

            Vector3 targetPose = new Vector3(point.x, GravityMode, point.z) - transform.position;

            if (Vector3.Distance(transform.position, point) > 1f) ch.Move(targetPose * speedPoint * Time.deltaTime);
               
            if (Vector3.Distance(transform.position, point) < 1.2f)
            {
                VarMoving = true;
                second = true;
                Anim.SetBool("Moving", false);
            }
        }
    }
    IEnumerator JumpInPlace()
    {
        yield return new WaitForSeconds(timeJump);
        GravityMode = jumpForce; 
        yield return new WaitForSeconds(2.05f- timeJump);
        jumpClosed = true;
    }
    private void GamingGravity()
    {

        if ((ch.collisionFlags & CollisionFlags.Below) != 0)
        {
            GravityMode = -10;
            isGrounded = true;
        }
        else
        {
            GravityMode -= minusGrav * Time.deltaTime; isGrounded = false;
        }

    


        if (Input.GetKey(KeyCode.Space) && isGrounded&&jumpClosed)
        {

            if (MovingBool)
            {
                GravityMode = jumpForce;
                Anim.SetTrigger("Jump");
            }
            else { Anim.SetTrigger("JumpInPlace"); jumpClosed = false; StartCoroutine(JumpInPlace()); }
        }
    }

    
}
