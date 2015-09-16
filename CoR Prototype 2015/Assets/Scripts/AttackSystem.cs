using UnityEngine;
using System.Collections;

public class AttackSystem : MonoBehaviour {


    public GameObject arrowPrefab;
    public GameObject axePrefab;
    public GameObject projectileOrgin;
    public GameObject weaponSealthed;
    private float attackSpeed;
    private bool attackRdy;
    private float nextAttack;
    private AttackType attackType;

    public bool isHoveringInteractable;

	void Update () 
    {
        if (GetComponent<CharacterMotor>().isenabled && !GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().isOverButton && !isHoveringInteractable)
        {
            attackSpeed = GetComponent<ChildStats>().attackSpeed;

            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackSpeed;
                attackRdy = true;
            }

            if (Input.GetButton("Fire1") && GetComponent<ChildStats>().attackType == AttackType.Range && attackRdy && arrowPrefab != null)
            {
                GameObject clone;
                clone = Instantiate(arrowPrefab, projectileOrgin.transform.position, projectileOrgin.transform.rotation) as GameObject;
                Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
                clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 1000f);
                attackRdy = false;
            }

            if (Input.GetButton("Fire1") && GetComponent<ChildStats>().attackType == AttackType.Melee && attackRdy && axePrefab != null)
            {
                axePrefab.transform.FindChild("Mesh").GetComponent<SkinnedMeshRenderer>().enabled = true;
                Animator anim = axePrefab.GetComponent<Animator>();
                anim.Play(0);
                attackRdy = false;
                if (weaponSealthed != null)
                {
                    weaponSealthed.transform.FindChild("Mesh").GetComponent<MeshRenderer>().enabled = false;
                }
            }

            if (GetComponent<ChildStats>().attackType == AttackType.Melee && attackRdy && axePrefab != null)
            {
                Animator anim = axePrefab.GetComponent<Animator>();
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("axeAttack"))
                {
                    axePrefab.transform.FindChild("Mesh").GetComponent<SkinnedMeshRenderer>().enabled = false;
                }
            }
        }
	}

    public void AddCDtoAttack()
    {
        nextAttack = Time.time + attackSpeed;
        attackRdy = false;
    }
}
