using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsRiseTrigger : MonoBehaviour
{

	public Animator wallsRiseAni;
    // Start is called before the first frame update
    void Start()
    {
		wallsRiseAni.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		wallsRiseAni.enabled=true;
	}
}
