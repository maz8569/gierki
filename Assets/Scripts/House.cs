using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public packageType houseType = packageType.BLUE;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "courier")
        {
            Debug.Log(other.gameObject.name);
            CourierController courier = other.gameObject.GetComponent<CourierController>();
            if(courier.packageTransform && courier.package == houseType && !courier.isAttacked)
            {
                courier.GivePackage();
            }
        }
    }
}
