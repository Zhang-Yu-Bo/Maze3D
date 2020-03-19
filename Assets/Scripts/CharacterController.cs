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
    public bool onAir;

    void Awake()
    {
        // initial value
        this.mSpeed = 0.0f;
        this.onAir = true;
        this.mAnimator = this.gameObject.GetComponent<Animator>();
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
        // 
        if (Input.GetKey(KeyCode.W))
        {
            this.mSpeed += Time.deltaTime;
        }
        else
        {
            this.mSpeed -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !this.onAir)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * this.jumpPower);
            this.mAnimator.SetTrigger("Jump");
        }
        this.mSpeed = Mathf.Clamp(this.mSpeed, this.MIN_SPEED, this.MAX_SPEED);
    }

    private void animationControll()
    {
        this.mAnimator.SetFloat("Speed", this.mSpeed);
        if (Input.GetKey(KeyCode.LeftControl))
            this.mAnimator.SetBool("Crouch", true);
        else
            this.mAnimator.SetBool("Crouch", false);
        if (Input.GetKey(KeyCode.LeftShift))
            this.mAnimator.SetBool("Run", true);
        else
            this.mAnimator.SetBool("Run", false);

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
