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
    private EnemyMotor enemyMotor;
    Transform focus;
    public int lives = 3;
    public PlayerController player;
    public CourierController courier;
    public packageType package;
    public GameObject packageObject;
    public bool isActive = true;
    public Vector3 point = new Vector3();
    bool walkPointSet = false;
    public int patrolSPeed = 2;
    public int chaseSpeed = 5;
    public GameController gameController;
    // private bool hasPackage = false;
    [SerializeField] private Transform dropHouse = null;
    public bool availableToPickUp = true;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<CourierMotor>();
        motor.agent.speed = patrolSPeed;
        GameObject mainCamera = GameObject.Find("Main Camera");
        gameController = mainCamera.GetComponent<GameController>();
        Debug.Log(gameController);
        enemyMotor = GetComponent<EnemyMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (focus && isActive)
        {
            
            motor.MoveToPoint(focus.position);
        }
        if(!focus && isActive)
        {
            Patroling();
        }
        if (packageObject)
        {
            packageObject.transform.position = transform.position + new Vector3(0f, 3f, 0f);

            RunAway();
        }
    }

    public void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            motor.MoveToPoint(point);

        Vector3 distanceToWalkPoint = transform.position - point;

        if (distanceToWalkPoint.magnitude < 0.2f)
            walkPointSet = false;
            enemyMotor.moveToDropHouse(dropHouse);
        }

        // if (hasPackage)
        // {
        //     motor.MoveToPoint(dropHouse.position);
        // }
    }

    public void SearchWalkPoint()
    {
        motor.RandomPoint(transform.position, 5f, out point);
        walkPointSet = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive)
        {
            if (!packageObject)
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

                if (other.gameObject.tag == "box" && availableToPickUp)
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
                        // hasPackage = true;
                        // TagHouse(dropHouse);
                        UnTag();
                        Debug.Log($"Zmierzam do punktu {dropHouse.position}");
                    }
                }
            }
            // else
            // {
            //     if (other.gameObject.tag == "")
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "courier")
        {
            courier.isAttacked = false;
        }
    }

    public void ResetDelay()
    {
        availableToPickUp = true;
    }

    private void Tag(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        focus = other.transform;
        motor.agent.speed = chaseSpeed;
    }
    
    private void TagHouse(Transform other)
    {
        //Debug.Log(other.gameObject.name);
        focus = other;
    }

    private void UnTag()
    {
        focus = null;
        motor.agent.speed = patrolSPeed;
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

    public void DepositPackage(EnemyHouse house)
    {
        house.PutPackageOnStack(packageObject);
        Box box = packageObject.GetComponent<Box>();
        box.parent = null;
        package = packageType.NONE;
        Destroy(packageObject);
        packageObject = null;
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
        bool hide = false;
        for(int i = 0; i < 30; i++)
        {
            float range = Random.Range(20, 80);
            hide = motor.RandomPoint(transform.position, range, out point);
            if (hide) break;
        }

        if(hide && package != packageType.NONE)
        {
            motor.MoveToPoint(point);

            Invoke("DigUp", 3f);
        }
    }

    public void DigUp()
    {
        gameController.SpawnEarth(package, transform);

        package = packageType.NONE;
        Destroy(packageObject);
    }
}
