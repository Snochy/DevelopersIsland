using UnityEngine;
using System.Collections;

public class AIChildControl : MonoBehaviour {

    public States currentState;
    public GameObject targetChild;
    private float velocity;

    public enum States
    {
        Idle,
        follow,
        stationary,
        alongSide,
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
                if (Vector3.Distance(transform.position, targetChild.transform.position) >= 5f)
                {
                    if (velocity <= .1f)
                    {
                        velocity += 0.001f;
                    }
                }
                else
                {
                    if (velocity > 0f)
                    {
                        velocity -= 0.001f;
                    }
                    else velocity = 0;
                }
                gameObject.GetComponent<CharacterController>().Move(moveDirection.normalized * velocity);
            }
            else if (currentState == States.alongSide)
            {
                Vector3 moveDirection = Vector3.zero;
                targetChild = GameObject.FindGameObjectWithTag("FollowTar");
                if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(targetChild.transform.position.x, transform.position.y, targetChild.transform.position.z)) > 0.1f)
                {
                    moveDirection = new Vector3(targetChild.transform.position.x, transform.position.y, targetChild.transform.position.z) - transform.position;
                    var newRot = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.1f);
                    if (velocity <= .1f)
                    {
                        velocity += 0.001f;
                    }
                }
                else
                {
                    if (velocity > 0f)
                    {
                        velocity -= 0.001f;
                    }
                    else velocity = 0;
                }
                gameObject.GetComponent<CharacterController>().Move(moveDirection.normalized * velocity);

            }
        }
	}

    public void ChangeState(int a)
    {
        switch (a)
        {
            case 0: currentState = States.stationary; break;
            case 1: currentState = States.follow; break;
            case 2: currentState = States.alongSide; break;
            default: currentState = States.stationary; break;
        }
    }
}
