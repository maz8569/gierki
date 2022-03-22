using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHouse : MonoBehaviour
{
    private Stack<GameObject> stolenPackages = new Stack<GameObject>();

    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log(other.gameObject.name);
            enemy = other.gameObject.GetComponent<Enemy>();
            EnemyMotor enemyMotor = other.gameObject.GetComponent<EnemyMotor>();
            if(enemy.packageObject)
            {
                // enemy.DepositPackage(this);
                Debug.LogWarning($"Paczka {enemy.packageObject}, {enemy.package} zostala oddana");
                enemy.DropPackage();
                //enemy.availableToPickUp = false;
                Invoke("ResetDelay", 2f);
                enemyMotor.moveToStartPosition();
            }
        }
        // Invoke("CheckState", 10f);
    }

    private void ResetDelay()
    {
       // enemy.ResetDelay();
    }

    public void PutPackageOnStack(GameObject package)
    {
        stolenPackages.Push(package);
    }
    
    public GameObject GetPackageFromStack()
    {
        return stolenPackages.Pop();
    }

    private void CheckState()
    {
        foreach (var pack in stolenPackages)
        {
            Debug.Log($"Paczki na stosie: {pack}");
        }
    }
}
