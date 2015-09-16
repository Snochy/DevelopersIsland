using UnityEngine;
using System.Collections;

public class AIChildControl : MonoBehaviour {

    public States currentState;
    public GameObject targetChild;

    public enum States
    {
        Idle,
        follow,
        stationary,
    }
	void Start () 
    {
        currentState = States.stationary;
	}

	void Update () 
    {
        if (!GetComponent<CharacterMotor>().isenabled)
        {
            if (currentState == States.stationary)
            {
            }
            else if (currentState == States.follow)
            {
                targetChild = GameObject.FindGameObjectWithTag("CameraControl").GetComponent<CameraSwap>().GetChild();
                Vector3 moveDirection = new Vector3(targetChild.transform.position.x, transform.position.y, targetChild.transform.position.z)  - transform.position;
                var newRot = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.1f);
                if (Vector3.Distance(this.transform.position, targetChild.transform.position) >= 10f)
                    gameObject.GetComponent<CharacterController>().Move(moveDirection.normalized * 7f * Time.deltaTime);
            }
        }
	}

    public void ChangeState(int a)
    {
        switch (a)
        {
            case 0: currentState = States.stationary; break;
            case 1: currentState = States.follow; break;
            default: currentState = States.stationary; break;
        }
    }
}
