using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene:MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1ButtonClicked()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
