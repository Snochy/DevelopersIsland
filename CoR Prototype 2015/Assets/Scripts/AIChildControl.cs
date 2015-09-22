using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIChildControl : MonoBehaviour {

    public FollowStates currentFollowState;
	public LostBehavior currentLostState;
	public GameObject targetChild;
    public float velocity;
	private GameObject aStar;

	private Node endNode;
	private Node startNode;
	private Queue queuePath = new Queue();	
	public List<Node> path;

	public float speed;
	
	void Start()
	{
		aStar = GameObject.Find("A*").gameObject;		
		aStar.GetComponent<Astar>().SetNetworkConnections();
		targetChild = GameObject.FindGameObjectWithTag("CameraControl").GetComponent<CameraSwap>().GetChild();
	}

	public enum FollowStates
    {
        Idle,
        follow,
        stationary,
        alongSide,
    }

	public enum LostBehavior
	{
		stationary,
		roam,
		seekout,
	}
	
	void Update () 
	{
		if (currentLostState == LostBehavior.roam)
			speed = 2.1f;
		else if (currentLostState == LostBehavior.seekout)
			speed = 4.1f;
		if (!GetComponent<CharacterMotor>().isenabled)
        {
            if (currentFollowState == FollowStates.follow)
            {
                targetChild = GameObject.FindGameObjectWithTag("CameraControl").GetComponent<CameraSwap>().GetChild();
				if(HasLineOfSight())
				{
					path.Clear();
					queuePath.Clear();
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
				else if(currentLostState == LostBehavior.roam)
				{
					if (queuePath.Count == 0)
						StartCoroutine(PathFinderRoam());
				}
				else if(currentLostState == LostBehavior.seekout)
				{
					if (PathPossiable())
					{
						if (queuePath.Count == 0)
							StartCoroutine(PathFinderChase());
					}
				}
			}
            else if (currentFollowState == FollowStates.alongSide)
            {
                Vector3 moveDirection = Vector3.zero;
				targetChild = GameObject.FindGameObjectWithTag("CameraControl").GetComponent<CameraSwap>().GetChild();
				if(HasLineOfSight())
				{
					path.Clear();
					queuePath.Clear();					
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
				else if(currentLostState == LostBehavior.roam)
				{
					if (queuePath.Count == 0)
						StartCoroutine(PathFinderRoam());
				}
				else if(currentLostState == LostBehavior.seekout)
				{
					if (PathPossiable())
					{
						if (queuePath.Count == 0)
							StartCoroutine(PathFinderChase());
					}
				}
			}
		}
		
		if (queuePath.Count != 0)
		{
			Node currentNode = queuePath.Peek() as Node;
			Vector3 moveDirection = currentNode.transform.position - transform.position;
			var newRot = Quaternion.LookRotation(moveDirection);
			newRot.x = 0;
			newRot.z = 0;
			transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.1f);
			gameObject.GetComponent<CharacterController>().Move(moveDirection.normalized * speed * Time.deltaTime);
			if (Vector3.Distance(this.transform.position, currentNode.GetPos()) <= .5f)
				queuePath.Dequeue();
		}
	}
	private List<Node> FindPathToTarget(Node endNde, Node startNode)
	{
		queuePath.Clear();
		if (endNode != null && startNode != null)
		{
			path = aStar.GetComponent<Astar>().GetPath(startNode, endNode);
		}
		AddListToQue();
		return path;
	}
	
	private List<Node> FindNextClosest()
	{
		startNode = aStar.GetComponent<Astar>().FindClosestNode(this.gameObject);
		endNode = startNode.RandomConnectedNode();
		return FindPathToTarget(endNode, startNode);
	}
	
	private bool PathPossiable()
	{
		return aStar.GetComponent<Astar>().PathPossiable;
	}
	
	private void AddListToQue()
	{
		queuePath.Clear();
		foreach (Node aNode in path)
			queuePath.Enqueue(aNode);
	}

	private bool HasLineOfSight()
	{
		RaycastHit rcHit;
		Vector3 centerOfEntity = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
		if (Physics.Raycast(centerOfEntity, targetChild.transform.position - transform.position, out rcHit, Mathf.Infinity))
		{
			if (rcHit.transform.gameObject == targetChild.transform.gameObject)
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator PathFinderChase()
	{
		endNode = aStar.GetComponent<Astar>().FindClosestNode(targetChild);
		startNode = aStar.GetComponent<Astar>().FindClosestNode(this.gameObject);
		path = FindPathToTarget(endNode, startNode);
		yield return new WaitForSeconds(0.5f);
	}
	
	private IEnumerator PathFinderRoam()
	{
		path = FindNextClosest();
		yield return new WaitForSeconds(0.5f);
	}
	
	public void ChangeFollowState(int a)
	{
		switch (a)
		{
		case 0: currentFollowState = FollowStates.stationary; break;
		case 1: currentFollowState = FollowStates.follow; break;
            case 2: currentFollowState = FollowStates.alongSide; break;
            default: currentFollowState = FollowStates.stationary; break;
        }
    }

	public void ChangeLostState(int a)
	{
		switch (a)
		{
		case 0: currentLostState = LostBehavior.stationary; break;
		case 1: currentLostState = LostBehavior.seekout; break;
		case 2: currentLostState = LostBehavior.roam; break;
		default: currentLostState = LostBehavior.stationary; break;
		}
	}

	public void ClearQue()
	{
		path.Clear();
		queuePath.Clear();	
	}
}
