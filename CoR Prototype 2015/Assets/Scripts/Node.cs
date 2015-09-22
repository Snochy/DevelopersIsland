using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

    public enum State { OPEN, CLOSED };  
    public State state = State.OPEN;	
    public List<Node> connectedNodes = new List<Node>();
    public Node parentNode;
    private float score;

    public void AddConnectedNode(Node aNode)
    {
        connectedNodes.Add(aNode);
    }

    public void RemoveConnectedNode(Node aNode)
    {
        connectedNodes.Remove(aNode);
    }

    public Vector3 GetPos()
    {
        return this.transform.position;
    }
    public void SetState(State aState)
    {
        state = aState;
    }

    public State GetState()
    {
        return state;
    }

    public List<Node> GetConnectedNodes()
    {
        return connectedNodes;
    }

    public void SetParent(Node aParent)
    {
        parentNode = aParent;
    }

    public Node GetParent()
    {
        return parentNode;
    }

    public void SetScore(float aScore)
    {
        score = aScore;
    }

    public Node RandomConnectedNode()
    {
        if (connectedNodes.Count > 0)
            return connectedNodes[Random.Range(0, connectedNodes.Count)];
        else return this;
    }
}
