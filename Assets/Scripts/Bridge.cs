using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "boat")
        {
            animator.SetBool("isOpened", true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "boat")
        {
            animator.SetBool("isOpened", false);
        }
    }

    // Update is called once per frame
}
