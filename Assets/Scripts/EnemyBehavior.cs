using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemy.isActive)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        }
        else
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("courier"))
        {
            Debug.Log(enemy.courier.package);
            if(enemy.courier.package != packageType.NONE)
            {
                enemy.courier.DropPackage();
            }
        }
    }
}
