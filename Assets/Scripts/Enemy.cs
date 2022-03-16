using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CourierMotor motor;
    Transform focus;
    public int lives = 3;
    PlayerController player;


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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "courier")
        {
            Debug.Log(other.gameObject.name);
            focus = other.transform;
            if(other.gameObject.tag == "Player")
            {
                player = other.gameObject.GetComponent<PlayerController>();
            }
        }
    }

    public void GetDamage()
    {
        lives--;
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
