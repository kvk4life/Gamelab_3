using UnityEngine;
using System.Collections;

public enum CameraMode
{
    ThirdPerson,
    ThirdPersonStrafe,
    FirstPerson,
    LockOn,
}
public class Orbit : MonoBehaviour
{

    public CameraMode camMode;
    public Animator anim;
    public Transform lookAt;
    public float rotSpeed;
    public Transform camTransform;
    public float moveSpeed;
    public GameObject obj;
    public Camera cam;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;
    public LockOnStrafe strafe;
    public Transform cameraLocStrafe;
    public Transform cameraLocHead;
    public Quaternion savedRot;
    public bool sprint;
    public Transform camRotator;
    public bool lerpBack;
    public float jumpForce;
    public Combat combat;
    public bool mayJump;
    public bool keyboard;
    // Use this for initialization
    void Start()
    {
        camTransform = transform;

    }

    void Update()
    {
        if (!keyboard)
        {
            switch (camMode)
            {

                case CameraMode.ThirdPerson:
                    obj.transform.position += obj.transform.forward * Input.GetAxis("LeftJoyY") * Time.deltaTime * moveSpeed;
                    obj.transform.Rotate(new Vector3(0, Input.GetAxis("LeftJoyX") * rotSpeed * Time.deltaTime, 0));
                    if (transform.parent != camRotator)
                    {
                        transform.SetParent(camRotator);
                    }
                    if (lerpBack == true)
                    {
                        CameraLerp(cameraLocStrafe);
                    }
                    transform.LookAt(camRotator);
                    if (sprint)
                    {
                        anim.SetFloat("MoveSpeed", 3);
                    }
                    else
                    {
                        anim.SetFloat("MoveSpeed", 2);
                    }
                    if (Input.GetAxis("LeftJoyY") == 0 && Input.GetAxis("LeftJoyX") == 0)
                    {
                        camRotator.Rotate(0, Input.GetAxis("RightJoyX") * rotSpeed * Time.deltaTime, 0);
                        anim.SetFloat("MoveSpeed",1);
                    }
                    else
                    {
                        CameraLerp(cameraLocStrafe);
                    }
                    if (Input.GetAxis("LeftJoyX") != 0)
                    {
                        anim.SetFloat("MoveSpeed", 2);
                    }

                    break;
                case CameraMode.ThirdPersonStrafe:
                    CameraLerp(cameraLocStrafe);
                    transform.LookAt(camRotator);
                    obj.transform.position += obj.transform.forward * Input.GetAxis("LeftJoyY") * Time.deltaTime * moveSpeed;
                    obj.transform.position += obj.transform.right * Input.GetAxis("LeftJoyX") * Time.deltaTime * moveSpeed;
                    obj.transform.Rotate(0, Input.GetAxis("RightJoyX") * rotSpeed * Time.deltaTime, 0);
                    break;
                case CameraMode.LockOn:
                    //CameraLerp(combat.closestEnemy);
                    obj.transform.position += obj.transform.forward * Input.GetAxis("LeftJoyY") * Time.deltaTime * moveSpeed;
                    obj.transform.position += obj.transform.right * Input.GetAxis("LeftJoyX") * Time.deltaTime * moveSpeed;
                    obj.transform.Rotate(0, Input.GetAxis("RightJoyX") * rotSpeed * Time.deltaTime, 0);
                    break;
                case CameraMode.FirstPerson:
                    CameraLerp(cameraLocHead);
                    obj.transform.position += obj.transform.forward * Input.GetAxis("LeftJoyY") * Time.deltaTime * moveSpeed;
                    obj.transform.Rotate(new Vector3(0, Input.GetAxis("LeftJoyX") * rotSpeed * Time.deltaTime, 0));
                    transform.Rotate(-Input.GetAxis("RightJoyY") * rotSpeed * Time.deltaTime, 0, 0);
                    break;

            }
            Animate();
            if (Input.GetButtonDown("L3") && camMode != CameraMode.ThirdPersonStrafe)
            {
                EnterSprint();
            }
            if (Input.GetButtonUp("L3"))
            {
                ExitSprint();
            }
            if (Input.GetButtonDown("A") && camMode != CameraMode.FirstPerson && mayJump == true)
            {
                obj.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);
                anim.SetBool("Jump",true);
                StartCoroutine("Jump");
                mayJump = false;
            }
        }
        else
        {
            obj.transform.position += obj.transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            obj.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0));
            if (Input.GetButtonDown("Jump") && camMode != CameraMode.FirstPerson && mayJump == true)
            {
                obj.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);
                anim.SetBool("Jump", true);
                StartCoroutine("Jump");
                mayJump = false;
            }
            Animate();
        }



    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0f);
        anim.SetBool("Jump",false);
        mayJump=true;
    }

    void EnterSprint()
    {
        if (sprint == false)
        {
            moveSpeed = moveSpeed * 4;
            anim.speed = 4;
            sprint = true;
        }

    }



    public void ExitSprint()
    {
        if (sprint == true)
        {
            moveSpeed = moveSpeed / 4;
            anim.speed = 1f;
            sprint = false;
        }

    }

    public IEnumerator ExitFirstPerson()
    {
        lerpBack = true;
        yield return new WaitForSeconds(1f);
        lerpBack = false;
    }

    void Animate()
    {

    }

    public void CameraLerp(Transform cameraLoc)
    {
        if (transform.parent != obj.transform)
        {
            transform.SetParent(obj.transform);
        }
        transform.position = Vector3.Lerp(transform.position, cameraLoc.position, 4 * Time.deltaTime);
    }
}
