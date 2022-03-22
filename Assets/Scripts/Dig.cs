using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : Interactable
{
    public int amount = 3;
    public Animator animator;
    public GameObject package;
    public PlayerController playerContr;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("state", amount);
        if(!player)
        {
            GameObject yer = GameObject.Find("Player");
            Debug.Log(yer);
            player = yer.transform;
            playerContr = yer.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose() && Input.GetMouseButtonDown(0) && amount >0 && !playerContr.isAttacked)
        {
            amount--;
            animator.SetInteger("state", amount);
        }

        if(amount == 0)
        {
            Instantiate(package, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
