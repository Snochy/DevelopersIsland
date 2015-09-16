using UnityEngine;
using System.Collections;

public class ChildAbilties : MonoBehaviour {

    public Abilities currentAbility;

    public Material detectMat;
    public Material defaultMat;

    public enum Abilities
    {
        None,
        CreatureSense,
    }
	void Update () 
    {
        if (GetComponent<CharacterMotor>().isenabled)
        {
            if (currentAbility == Abilities.CreatureSense)
            {
                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject go in Enemies)
                    if (Vector3.Distance(transform.position, go.transform.position) <= 60f && !HasLineOfSight(go))
                        go.GetComponent<MeshRenderer>().material = detectMat;
                    else go.GetComponent<MeshRenderer>().material = defaultMat;
            }
            else if (currentAbility != Abilities.CreatureSense)
            {
                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject go in Enemies)
                    go.GetComponent<MeshRenderer>().material = defaultMat;
            }
        }

	}

    private bool HasLineOfSight(GameObject target)
    {
            RaycastHit rcHit;
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out rcHit, Mathf.Infinity))
            {
                if (rcHit.transform.gameObject == target.gameObject)
                {
                    return true;
                }
            }
            return false;
    }
}
