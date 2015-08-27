using UnityEngine;
using System.Collections;

public class PingPong : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (Mathf.PingPong(Time.time*10,10+10)-10, transform.position.y, transform.position.z);
	
	}
}
