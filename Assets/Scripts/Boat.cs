using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public int m = 1;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(pos.z > 30 || pos.z < -30)
        {
            m *= -1;
        }
        pos.z += 0.01f * m;
        transform.position = pos;
    }
}
