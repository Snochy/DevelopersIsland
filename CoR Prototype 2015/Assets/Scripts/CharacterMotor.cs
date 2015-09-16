using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour
{
    public float rotateSpeed = 6.0F;
    public float jumpSpeed = 140.0F;
    private float defaultSpeed;

    public bool isenabled = true;

    public double speedMod = 0;

    private double currentSpeed;

    private Vector3 moveDirection;

    void Update()
    {
        defaultSpeed = GetComponent<ChildStats>().speed;

        if (Input.GetKey(KeyCode.LeftShift))
            speedMod = defaultSpeed * .5f;
        else speedMod = 0;

        currentSpeed = defaultSpeed + speedMod;
        if (currentSpeed <= 0)
            currentSpeed = 0;


        CharacterController controller = GetComponent<CharacterController>();
        moveDirection.y -= 1f;

        if (isenabled)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().currentChild = gameObject;
            if (controller.isGrounded)
            {
                float forward;

                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                {
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                    forward = 1;
                }
                else forward = Input.GetAxis("Vertical");

                moveDirection = new Vector3(0, 0, forward);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= (float)currentSpeed;

                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpSpeed;
                }

            }

            else
            {

                if (moveDirection.y >= 1)
                {
                    moveDirection.y -= jumpSpeed * Time.deltaTime;
                }

                moveDirection.y -= 1f;

            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, rotateSpeed, 0);

            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -rotateSpeed, 0);
            }

            if ((controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                if (moveDirection.y > 0)
                {
                    moveDirection.y = 0;
                }
            }
        }

        if (isenabled)
            controller.Move(moveDirection * Time.deltaTime);
        else
        {
            moveDirection = Vector3.zero;
            moveDirection.y -= 10f;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
