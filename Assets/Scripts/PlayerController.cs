using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6;
    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;
    public Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask groundMask;
    private float _terminalVelocity = 53.0f;

    public float turnSmoothTime = 0.1f;

    public bool isAttacked = false;
    public List<Enemy> enemies;

    public GameObject boat;
    public Vector3 distance;
    // Update is called once per frame
    void Update()
    {

        //jump
        GroundedCheck();

        if (Grounded && velocity.y < 0)
        {
            velocity.y =  -2f;
        }

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            boat = null;

        }
        //gravity
        velocity.y += Gravity * Time.deltaTime;
        if(velocity.y < _terminalVelocity)
        {
            velocity.y += Gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
        //walk

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(isAttacked && Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetDamage();
            }
        }
        
        isAttacked = enemies.Count>0;

    }
    private void LateUpdate()
    {
        if (boat)
        {
            transform.position = boat.transform.position + distance;
        }
    }

    private void GroundedCheck()
    {
        if(boat)
        {
            Grounded = true;
        }
        else
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, groundMask, QueryTriggerInteraction.Ignore);
        }
        // set sphere position, with offset

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "boat")
        {
            Debug.Log("boat");
            boat = collision.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log(other.gameObject.name);
            isAttacked = true;
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log(other.gameObject.name);
            RemoveEnemy(other.gameObject.GetComponent<Enemy>());
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
