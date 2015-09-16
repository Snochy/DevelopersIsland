using UnityEngine;
using System.Collections;

public class projectileControl : MonoBehaviour {

    public GameObject particle;

    public GameObject clone;
    private Vector3 positionLast;
    private bool hitSomething;

    void Start()
    {
        clone = Instantiate(particle, transform.position, transform.rotation) as GameObject;
        positionLast = transform.position;
    }

	void Update () 
    {
        if (!hitSomething)
        {
            clone.transform.localPosition = positionLast;
            clone.transform.LookAt(transform);
            positionLast = transform.position;
        }
	}

    void OnCollisionStay(Collision col )
    {
        if (col.gameObject != null)
        {
            hitSomething = true;
            clone.transform.parent = col.gameObject.transform;
            Destroy(this.gameObject);
        }
    }
}
