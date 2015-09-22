using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Astar : MonoBehaviour {

    public Node startNode;
    public Node endNode;
    public List<Node> allNodes;
    public bool PathPossiable = false;

    public List<Node> GetPath(Node startPoint, Node endPoint)
    {
        startNode = startPoint;
        endNode = endPoint;
        BreatheFirst();
        return FindPathFromEndToStart();
    }

    public Node GetRandomNode()
    {
        if (allNodes.Count < 1)
            SetNetworkConnections();
        return allNodes[Random.Range(0, allNodes.Count)];
    }

    public Node FindClosestNode(GameObject target)
    {
        if (allNodes.Count < 1)
            SetNetworkConnections();
        Node returnNode = null;
        foreach (Node aNode in allNodes)
            if (!Physics.Linecast(target.transform.position, aNode.transform.position))
                if (returnNode != null)
                {
                    if (FindDistance(target.transform.position, aNode.transform.position) < FindDistance(target.transform.position, returnNode.transform.position))
                        returnNode = aNode;
                }
                else returnNode = aNode;

        return returnNode;
    }

    public void SetNetworkConnections()
    {
        FindLOSConnections();
    }

    private void BreatheFirst()
    {
        PathPossiable = false;
        if (allNodes.Count < 1)
            SetNetworkConnections();
        foreach (Node node in allNodes)
            node.SetState(Node.State.OPEN);
        foreach (Node node in allNodes)
            node.SetParent(null);

        Queue frontier = new Queue();
        frontier.Enqueue(startNode);
        startNode.SetScore(0);
        startNode.SetState(Node.State.CLOSED);
        float currentScore = 0;

        while (frontier.Count > 0)
        {
            Node current = frontier.Dequeue() as Node;
            if (current == endNode)
            {
                PathPossiable = true;
                break;
            }

            foreach (Node next in current.GetConnectedNodes())
                if (next.GetState() != Node.State.CLOSED)
                {
                    frontier.Enqueue(next);
                    next.SetState(Node.State.CLOSED);
                    next.SetParent(current);
                    frontier = PriortyQueue(frontier, currentScore);
                    Node front = frontier.Peek() as Node;
                    currentScore = currentScore + FindDistance(front.GetPos(), endNode.GetPos());
                    front.SetScore(currentScore);
                }
        }
    }

    private void FindLOSConnections()
    {
        allNodes.Clear();
        GameObject[] nodesFound = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject node in nodesFound)
            allNodes.Add(node.GetComponent<Node>());

        foreach (Node node1 in allNodes)
        {
            foreach (Node node2 in allNodes)
            {
                if (node1 != node2)
                    if (!Physics.Linecast(node1.GetPos(), node2.GetPos()))
                    {
                            node1.AddConnectedNode(node2);
                    }
            }
        }
    }

    private List<Node> FindPathFromEndToStart()
    {
        Queue parents = new Queue();
        parents.Enqueue(endNode);
        List<Node> path = new List<Node>();
        while (parents.Count > 0)
        {
            Node current = parents.Dequeue() as Node;
            if (current.GetParent() != null)
            {
                parents.Enqueue(current.GetParent());
            }

            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    private Queue PriortyQueue(Queue aQueue, float currentScore)
    {
        Node[] arrayOfNodes = new Node[aQueue.Count];
        Queue queueOfNode = new Queue();
        Node temp = null;
        int queueSize = aQueue.Count;
        if (aQueue.Count > 1)
        {
            for (int count = 0; count < queueSize; count++)
                arrayOfNodes[count] = aQueue.Dequeue() as Node;

            for (int write = 0; write < arrayOfNodes.Length; write++)
            {
                for (int sort = 0; sort < arrayOfNodes.Length - 1; sort++)
                {
                    if (FindDistance(arrayOfNodes[sort].GetPos(), endNode.GetPos()) + currentScore > FindDistance(arrayOfNodes[sort + 1].GetPos(), endNode.GetPos()) + currentScore)
                    {
                        temp = arrayOfNodes[sort + 1];
                        arrayOfNodes[sort + 1] = arrayOfNodes[sort];
                        arrayOfNodes[sort] = temp;
                    }
                }
            }
        }
        else return aQueue;

        foreach (Node aNode in arrayOfNodes)
            queueOfNode.Enqueue(aNode);
        return queueOfNode;
    }

    private float FindDistance(Vector3 pointA, Vector3 pointB)
    {
        return Vector3.Distance(pointA, pointB);
    }
}
