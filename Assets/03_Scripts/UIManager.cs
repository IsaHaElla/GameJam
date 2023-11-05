using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;
    public GameObject PauseMenu;
    private bool isShowing=false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           isShowing = false;
           PauseMenu.SetActive(isShowing);


            if (Time.timeScale==1)
            {
                Time.timeScale = 0;
                showPaused();
             
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
    }
    public void pauseControl()
    {
        if (Time.timeScale==1)
        {
            Time.timeScale=0;
            showPaused();
        }
        else if (Time.timeScale ==0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }
    public void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }public void hidePaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
