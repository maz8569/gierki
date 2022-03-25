using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int packages = 1;
    public TextMeshProUGUI text;
    public Canvas win;

    public GameObject blue_dirt;
    public GameObject red_dirt;
    public GameObject yellow_dirt;
    public int sceneNumber = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText($"Packages left: " + packages);

        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Deliver()
    {
        packages--;
        if (packages == 0)
        {
            Debug.Log("WIN");
            win.gameObject.SetActive(true);
        }

    }

    public void Reload()
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void SpawnEarth(packageType type, Transform transform)
    {
        Vector3 pos = transform.position + new Vector3(0, 1f, 0);
        switch(type)
        {
            case packageType.BLUE:
                Instantiate(blue_dirt, pos , Quaternion.identity);
                break;
            case packageType.RED:
                Instantiate(red_dirt, pos, Quaternion.identity);
                break;
            case packageType.YELLOW:
                Instantiate(yellow_dirt, pos, Quaternion.identity);
                break;
        }
    }
}
