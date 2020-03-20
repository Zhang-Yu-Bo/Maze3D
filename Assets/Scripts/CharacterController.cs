using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("最大速率")]
    public float MAX_SPEED;
    [Header("最小速率")]
    public float MIN_SPEED;
    [Header("現在速率")]
    public float mSpeed;
    [Header("跳躍力")]
    public float jumpPower;
    [Header("第一人稱相機")]
    public Camera firstCamera;
    [Header("第三人稱相機")]
    public Camera thirdCamera;


    private Animator mAnimator;
    private Rigidbody mRigidbody;
    private Transform mTransform;
    private bool onAir;
    private bool isFirstCamera;

    void Awake()
    {
        // initial value
        this.mSpeed = 0.0f;
        this.onAir = true;
        this.isFirstCamera = false;
        this.mAnimator = this.gameObject.GetComponent<Animator>();
        this.mRigidbody = this.gameObject.GetComponent<Rigidbody>();
        this.mTransform = this.gameObject.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            this.isFirstCamera = true;
            this.firstCamera.enabled = true;
            this.thirdCamera.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            this.isFirstCamera = false;
            this.firstCamera.enabled = false;
            this.thirdCamera.enabled = true;
        }

        this.moveControll();
        this.animationControll();
    }

    private void moveControll()
    {
        //float leftOrRightMove = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            this.mSpeed += Time.deltaTime;
            if (this.isFirstCamera)
                this.mTransform.rotation = Quaternion.Euler(new Vector3(0.0f, this.firstCamera.transform.eulerAngles.y, 0.0f));
            else
                this.mTransform.rotation = Quaternion.Euler(new Vector3(0.0f, this.thirdCamera.transform.eulerAngles.y, 0.0f));
        }
        else
            this.mSpeed -= Time.deltaTime;

        if (!this.onAir)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                this.mRigidbody.AddForce(Vector3.up * this.jumpPower);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //if (Input.GetKey(KeyCode.W))
            //{
            //    leftOrRightMove = -1;
            //}
            //else
            //{
                this.transform.Rotate(0.0f, -1.0f, 0.0f, Space.Self);
                this.firstCamera.transform.RotateAround(
                    this.transform.position, Vector3.up, -1.0f);
                this.thirdCamera.transform.RotateAround(
                    this.transform.position, Vector3.up, -1.0f);
            //}
        }
        if (Input.GetKey(KeyCode.D))
        {
            //if (Input.GetKey(KeyCode.W))
            //{
            //    leftOrRightMove = 1;
            //}
            //else
            //{
                this.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
                this.firstCamera.transform.RotateAround(
                    this.transform.position, Vector3.up, 1.0f);
                this.thirdCamera.transform.RotateAround(
                    this.transform.position, Vector3.up, 1.0f);
            //}
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.mSpeed = 0.0f;
        }

        this.mSpeed = Mathf.Clamp(this.mSpeed, this.MIN_SPEED, this.MAX_SPEED);

        //this.mTransform.Translate(new Vector3(leftOrRightMove, 0.0f, 1.0f) * this.mSpeed, Space.Self);
        this.mTransform.Translate(Vector3.forward * this.mSpeed, Space.Self);

    }

    private void animationControll()
    {
        this.mAnimator.SetFloat("Speed", this.mSpeed);

        if (!this.onAir)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                this.mAnimator.SetBool("Crouch", true);
                this.firstCamera.transform.Translate(-Vector3.up * 5, Space.Self);
            }
            else
            {
                this.firstCamera.transform.Translate(Vector3.up * 5, Space.Self);
                this.mAnimator.SetBool("Crouch", false);
            }
            if (Input.GetKey(KeyCode.LeftShift))
                this.MAX_SPEED = 1.0f;
            else
                this.MAX_SPEED = 0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
                this.mAnimator.SetTrigger("Jump");
        }

        //if (this.mSpeed == 0.0f && Input.GetKey(KeyCode.A))
        //    this.mAnimator.SetBool("Turn Left", true);
        //else
        //    this.mAnimator.SetBool("Turn Left", false);
        //if (this.mSpeed == 0.0f && Input.GetKey(KeyCode.D))
        //    this.mAnimator.SetBool("Turn Right", true);
        //else
        //    this.mAnimator.SetBool("Turn Right", false);
        //if (this.mSpeed == 0.0f && Input.GetKey(KeyCode.S))
        //    this.mAnimator.SetBool("Turn Back", true);
        //else
        //    this.mAnimator.SetBool("Turn Back", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            this.onAir = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            this.onAir = true;
    }
}
