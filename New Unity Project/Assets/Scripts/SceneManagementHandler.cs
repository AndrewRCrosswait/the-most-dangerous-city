using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementHandler : MonoBehaviour
{
    public int current= 0; //Keeps Track of Current Scene

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(current);
    }

    // Update is called once per frame
    void Update()
    {
        current = SceneManager.GetActiveScene().buildIndex; //Updates Current Scene As the Active Scene
    }
    
    public void ReloadScene(string TargetScene)
    {
        SceneManager.LoadScene(TargetScene); //Loads Target Scene
    }
}
