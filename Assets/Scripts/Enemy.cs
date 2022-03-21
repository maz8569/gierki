using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions
{
    PATROL,
    FOLLOW,
    ATTACK,
    RUNNAWAY,
    DELIVER,
    SLEEP
}

public class Enemy : MonoBehaviour
{
    CourierMotor motor;
    Transform focus;
    public int lives = 3;
    public PlayerController player;
    public CourierController courier;
    public packageType package;
    public GameObject packageObject;
    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<CourierMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (focus && isActive)
        {
            motor.MoveToPoint(focus.position);
        }
        if (packageObject)
        {
            packageObject.transform.position = transform.position + new Vector3(0f, 3f, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive)
        {
            if (other.gameObject.tag == "Player")
            {
                Tag(other);
                player = other.gameObject.GetComponent<PlayerController>();

            }

            if (other.gameObject.tag == "courier")
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

    private void UnTag()
    {
        focus = null;
    }

    public void DropPackage()
    {
        packageObject.transform.position = transform.position;
        packageObject.transform.localScale = new Vector3(1f, 1f, 1f);
        Box box = packageObject.GetComponent<Box>();
        box.parent = null;
        packageObject = null;
        package = packageType.NONE;
    }

    public void Fall()
    {
        isActive = false;
        if (player)
        {
            player.isAttacked = false;
        }
        if(courier)
        {
            courier.isAttacked = false;
        }
        //transform.localRotation = Quaternion.Euler(90f, 0, 0);
        if(packageObject) DropPackage();
        UnTag();

        Invoke("Recover", 10f);
    }

    public void Recover()
    {
        lives = 3;
        isActive = true;
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
            Fall();
        }
    }

    public void RunAway()
    {

    }

    public void DigUp()
    {
        GameController.SpawnEarth(package, transform);
    }
}
