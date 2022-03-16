using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum packageType
{
    NONE,
    BLUE,
    RED,
    YELLOW
}

[RequireComponent(typeof(CourierMotor))]
public class CourierController : MonoBehaviour
{
    Camera cam;
    CourierMotor motor;
    public LayerMask movementMask;
    public packageType package = packageType.NONE;
    public GameObject packageTransform;
    public GameController game;
    public bool isAttacked;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<CourierMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("We hit" + hit.collider.name + " ");
                motor.MoveToPoint(hit.point);
            }
        }

        if(packageTransform)
        {
            packageTransform.transform.position = transform.position + new Vector3(0f, 3f, 0f);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            Debug.Log(other.gameObject.name);
            Box box = other.gameObject.GetComponent<Box>();
            Debug.Log(box.type);
            package = box.type;
            packageTransform = other.gameObject;
            packageTransform.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void GivePackage()
    {
        package = packageType.NONE;
        Destroy(packageTransform);
        packageTransform = null;
        game.Deliver();
    }
}
