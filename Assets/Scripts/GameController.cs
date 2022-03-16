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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText($"Packages left: " + packages);
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
        SceneManager.LoadScene(0);
    }
}
