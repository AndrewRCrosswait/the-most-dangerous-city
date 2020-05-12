using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[Serializable]
public class Scene{
    public string Diologue, Name;
    public Sprite Frame;
    [NonSerialized]public float FrameTime;
}

public class Theater : MonoBehaviour
{
    public Text Display, NameDisplay;
    int CurrentScene = 0;
    string CurrentText = "";
    public GameObject Handler, CurrentFrame;
    float SavedTime, deley = .2f;
    [SerializeField] public Scene[] Scenes;
    public string NextScene;

    public void Transefer()
    {
        SceneManager.LoadScene(NextScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        Handler.SetActive(true);
        //check to make sure the sprites are not empty
        foreach (Scene X in Scenes)
        {
            if (X.Frame != CurrentFrame)
            {
                X.Frame = CurrentFrame.GetComponent<Image>().sprite;
            }
        }
        Calculate();

    }

    void Calculate()
    {
        foreach (Scene X in Scenes)
        {
            foreach (Char x in X.Diologue)
            {
                X.FrameTime += .6f;
            }
        }
    }

    void Update()
    {
        //UI set up for the current Sceenes data types
        LoadScene();
        Textout();
        // subtract by current's time and check if it is below zero if so change the scene
        Scenes[CurrentScene].FrameTime -= .052f;
        if (Scenes[CurrentScene].FrameTime <= 0)
        {
            New();
        }
        
    }
    public void Back()
    {
        CurrentScene--;
        LoadScene();
        Calculate();
    }
    void LoadScene()
    {
        Display.text = Scenes[CurrentScene].Diologue;
        NameDisplay.text = Scenes[CurrentScene].Name;
        CurrentFrame.GetComponent<Image>().sprite = Scenes[CurrentScene].Frame;
    }
    void New()
    {
        //change the scene as long as there are more
        if (CurrentScene < Scenes.Length - 1)
        {
            CurrentScene += 1;
        }
        else
        {
            Handler.SetActive(false);
            //set info here
            gameObject.SetActive(false);
        }
    }

    public void Click()
    {
        print("Pressed");
        New();
    }

    IEnumerator Textout()
    {
        for (int i = 0; i < Scenes[CurrentScene].Diologue.Length; i++)
        {
            CurrentText = Scenes[CurrentScene].Diologue;
            yield return new WaitForSeconds(deley);
        }
    }
}
