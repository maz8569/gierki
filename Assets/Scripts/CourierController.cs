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
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<CourierMotor>();
        //lineRenderer = 
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
                Debug.Log("We hit" + hit.collider.name + " " + hit.point);
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
        if (other.gameObject.tag == "box" && !isAttacked)
        {
            Debug.Log(other.gameObject.name);
            Box box = other.gameObject.GetComponent<Box>();
            if(box.parent == null)
            {
                Debug.Log(box.type);
                package = box.type;
                packageTransform = other.gameObject;
                packageTransform.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                box.parent = this.gameObject;
            }
        }
    }

    public void GivePackage()
    {
        package = packageType.NONE;
        Destroy(packageTransform);
        packageTransform = null;
        game.Deliver();
    }

    public void DropPackage()
    {
        packageTransform.transform.position = transform.position;
        packageTransform.transform.localScale = new Vector3(1f, 1f, 1f);
        Box box = packageTransform.GetComponent<Box>();
        box.parent = null;
        packageTransform = null;
        package = packageType.NONE;
    }
}
