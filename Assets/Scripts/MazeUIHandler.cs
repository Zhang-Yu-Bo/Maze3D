﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeUIHandler : MonoBehaviour
{
    public Text timeText;
    public GameObject mainCharacter;
    public AudioClip bell;
    public GameObject endCanvas;

    private float Mins;
    private float Secs;
    private float mSecs;
    private bool isEnd;

    private void Awake()
    {
        this.isEnd = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Mins = 5;
        this.Secs = 0;
        this.mSecs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.updateTime();
    }

    private void updateTime()
    {
        if (!this.isEnd)
        {
            float deltaTime = Mathf.Floor(Time.deltaTime * 100);
            this.mSecs -= deltaTime;
            if (this.mSecs < 0)
            {
                this.Secs--;
                this.mSecs += 100;
                if (this.Secs % 10 == 0)
                    this.mainCharacter.GetComponent<AudioSource>().PlayOneShot(this.bell);
            }
            if (this.Secs < 0)
            {
                this.Mins--;
                this.Secs += 60;
            }
            if (this.Mins >= 0)
                timeText.text = "Time: " + this.Mins.ToString("00") + ":" + this.Secs.ToString("00") + ":" + this.mSecs.ToString("00");
            else
            {
                this.isEnd = true;
            }
        }
        else
        {
            FindObjectOfType<CharacterControll>().gameOver();
            this.endCanvas.SetActive(true);
        }
    }

    public void backToMenu()
    {
        this.isEnd = true;
        SceneManager.LoadScene("Menu");
    }
}
