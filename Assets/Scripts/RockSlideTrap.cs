using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlideTrap : MonoBehaviour
{
	public Rigidbody[] rock;

	void Start()
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("HI");
		for(int i=0;i<18;i++)
		{
			rock[i].GetComponent<MeshRenderer>().enabled = true;
			rock[i].GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
