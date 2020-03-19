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


    private Animator mAnimator;
    private Rigidbody mRigidbody;
    private Transform mTransform;
    private bool onAir;

    void Awake()
    {
        // initial value
        this.mSpeed = 0.0f;
        this.onAir = true;
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
        this.moveControll();
        this.animationControll();
    }

    private void moveControll()
    {


        if (Input.GetKey(KeyCode.W))
            this.mSpeed += Time.deltaTime;
        else
            this.mSpeed -= Time.deltaTime;

        if (!this.onAir)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                this.mRigidbody.AddForce(Vector3.up * this.jumpPower);
        }
        if (Input.GetKey(KeyCode.A))
            this.transform.Rotate(0.0f, -1.0f, 0.0f, Space.Self);
        if (Input.GetKey(KeyCode.D))
            this.transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
        if (Input.GetKeyDown(KeyCode.S))
            this.transform.Rotate(0.0f, -180.0f, 0.0f, Space.Self);

        this.mSpeed = Mathf.Clamp(this.mSpeed, this.MIN_SPEED, this.MAX_SPEED);

        this.mTransform.Translate(Vector3.forward * this.mSpeed, Space.Self);
    }

    private void animationControll()
    {
        this.mAnimator.SetFloat("Speed", this.mSpeed);

        if (!this.onAir)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                this.mAnimator.SetBool("Crouch", true);
            else
                this.mAnimator.SetBool("Crouch", false);
            if (Input.GetKey(KeyCode.LeftShift))
                this.MAX_SPEED = 1.0f;
            else
                this.MAX_SPEED = 0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
                this.mAnimator.SetTrigger("Jump");
        }

        //if (Input.GetKey(KeyCode.A))
        //    this.mAnimator.SetTrigger("Turn Left");
        //if (Input.GetKey(KeyCode.D))
        //    this.mAnimator.SetTrigger("Turn Right");
        //if (Input.GetKey(KeyCode.S))
        //    this.mAnimator.SetTrigger("Turn Back");
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
