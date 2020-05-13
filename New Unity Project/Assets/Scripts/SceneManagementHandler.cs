using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementHandler : MonoBehaviour
{
    public int current= 0;

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(current);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void ReloadScene(string TargetScene)
    {
        SceneManager.LoadScene(TargetScene);
    }
}
