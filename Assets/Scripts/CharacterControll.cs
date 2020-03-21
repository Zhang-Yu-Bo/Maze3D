using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
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
    private bool isLive = false;

    void Awake()
    {
        // initial value
        this.mSpeed = 0.0f;
        this.onAir = true;
        this.isFirstCamera = false;
        this.mAnimator = this.gameObject.GetComponent<Animator>();
        this.mRigidbody = this.gameObject.GetComponent<Rigidbody>();
        this.mTransform = this.gameObject.GetComponent<Transform>();
        this.isLive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLive)
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
    }

    private void moveControll()
    {
        // 角度控制變數
        float angleFixedValue = 0.0f;

        // 平移控制
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || 
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            this.mSpeed += Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
                angleFixedValue += 0.0f;
            if (Input.GetKey(KeyCode.S))
                angleFixedValue += 180.0f;
            if (Input.GetKey(KeyCode.A))
                if (Input.GetKey(KeyCode.S))
                    angleFixedValue += 90.0f;
                else
                    angleFixedValue -= 90.0f;
            if (Input.GetKey(KeyCode.D))
                if (Input.GetKey(KeyCode.S))
                    angleFixedValue -= 90.0f;
                else
                    angleFixedValue += 90.0f;

            // 人物轉向控制，根據當前攝影機
            if (this.isFirstCamera)
                this.mTransform.rotation = Quaternion.Euler(new Vector3(0.0f, this.firstCamera.transform.eulerAngles.y + angleFixedValue, 0.0f));
            else
                this.mTransform.rotation = Quaternion.Euler(new Vector3(0.0f, this.thirdCamera.transform.eulerAngles.y + angleFixedValue, 0.0f));
        }
        else
            this.mSpeed -= Time.deltaTime;

        if (!this.onAir)
            if (Input.GetKeyDown(KeyCode.Space))
                this.mRigidbody.AddForce(Vector3.up * this.jumpPower);

        this.mSpeed = Mathf.Clamp(this.mSpeed, this.MIN_SPEED, this.MAX_SPEED);
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
            }
            else
            {
                this.mAnimator.SetBool("Crouch", false);
            }
            if (Input.GetKey(KeyCode.LeftShift))
                this.MAX_SPEED = 1.0f;
            else
                this.MAX_SPEED = 0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
                this.mAnimator.SetTrigger("Jump");
        }
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

    public void gameOver()
    {
        this.isLive = false;
    }
}
