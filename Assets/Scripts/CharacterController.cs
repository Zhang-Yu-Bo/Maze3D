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

    private Animator mAnimator;

    void Awake()
    {
        // initial value
        this.mSpeed = 0.0f;
        this.mAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.mSpeed += Time.deltaTime;
        }
        else
        {
            this.mSpeed -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            this.mAnimator.SetBool("Crouch", true);
        }
        else
        {
            this.mAnimator.SetBool("Crouch", false);
        }

        this.mSpeed = Mathf.Clamp(this.mSpeed, this.MIN_SPEED, this.MAX_SPEED);
        this.mAnimator.SetFloat("Speed", this.mSpeed);
    }
}
