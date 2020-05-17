using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : MonoBehaviour
{
    public SceneManagementHandler SMH;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("touchy");
            SMH.current++;
            SMH.LoadCurrentScene();
        }
    }
}
