using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraControll : MonoBehaviour
{
    [Header("視角轉動靈敏度")]
    public float rotateSensitivity;
    [Header("滾輪靈敏度")]
    public float distenceSensitivity;
    [Header("角色物件")]
    public GameObject charactor;

    // 相機對自身x軸的現在角度
    private float yAngle = 0.0f;

    private bool isLock = false;
    private bool showMap = false;

    void Awake()
    {
        this.yAngle = -this.gameObject.transform.eulerAngles.x;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.changeView();
        if (Input.GetKey(KeyCode.Tab))
            this.showMap = true;
        else
            this.showMap = false;

        if (this.showMap)
            this.gameObject.GetComponent<Camera>().cullingMask = 0b0010_0011_0011_0111;
        else
            this.gameObject.GetComponent<Camera>().cullingMask = 0b0000_0011_0011_0111;
    }

    private void changeView()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            this.isLock = !this.isLock;

        // 第一人稱視角
        if (this.isLock || Input.GetMouseButton(1))
        {
            // 隱藏游標
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Vector2 rotateAngle = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            rotateAngle *= this.rotateSensitivity;

            // 對自己的x軸轉y度 (限定y範圍在 [-60, 40] 之間)
            rotateAngle.y *= 0.5f;
            if (yAngle + rotateAngle.y <= 40 && yAngle + rotateAngle.y >= -60)
            {
                yAngle += rotateAngle.y;
                this.gameObject.transform.eulerAngles = new Vector3(
                    -yAngle,
                    this.gameObject.transform.eulerAngles.y,
                    this.gameObject.transform.eulerAngles.z);
            }
            // 對自己的x軸轉y度
            //this.gameObject.transform.Rotate(rotateAngle.y, 0.0f, 0, Space.Self);
            // 對世界的y軸轉x度
            //this.gameObject.transform.Rotate(0.0f, diff.x, 0,Space.World);
            // 對目標的x軸轉y度
            //this.gameObject.transform.RotateAround(
            //    this.charactor.gameObject.transform.position, Vector3.left, rotateAngle.y);
            // 對目標的y軸轉x度
            this.gameObject.transform.RotateAround(
                this.charactor.gameObject.transform.position, Vector3.up, rotateAngle.x);
        }
        else
        {
            // 顯示游標
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // 跟人物跑
        Vector3 cameraPosition = this.gameObject.transform.rotation
            * new Vector3(0.0f, 0.0f, 2.2f)
            + this.charactor.transform.position + Vector3.up * 9;
        this.gameObject.transform.position = cameraPosition;
    }
}
