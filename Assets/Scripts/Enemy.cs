using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CourierMotor motor;
    Transform focus;
    public int lives = 3;
    public PlayerController player;
    public CourierController courier;
    public packageType package;
    public GameObject packageObject;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<CourierMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(focus)
        {
            motor.MoveToPoint(focus.position);
        }
        if(packageObject)
        {
            packageObject.transform.position = transform.position + new Vector3(0f, 3f, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Tag(other);
            player = other.gameObject.GetComponent<PlayerController>();

        }

        if(other.gameObject.tag == "courier")
        {
            Tag(other);
            courier = other.gameObject.GetComponent<CourierController>();
            courier.isAttacked = true;
        }

        if (other.gameObject.tag == "box")
        {
            Debug.Log(other.gameObject.name);
            Box box = other.gameObject.GetComponent<Box>();
            if (box.parent == null)
            {
                Debug.Log(box.type);
                package = box.type;
                packageObject = other.gameObject;
                packageObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                box.parent = this.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "courier")
        {
            courier.isAttacked = false;
        }
    }

    private void Tag(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        focus = other.transform;
    }

    public void GetDamage()
    {
        lives--;
        Debug.Log(lives);
        if(lives == 0)
        {
            if(player)
            {
                player.RemoveEnemy(this);
            }
            Debug.Log("Enemy down");
            Destroy(gameObject);
        }
    }
}
