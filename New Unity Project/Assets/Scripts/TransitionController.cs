using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public Vector2 CameraChange;
    public Vector3 PlayerChange;
    private CameraMovementBehavior c;
    public GameObject C;

    // Start is called before the first frame update
    void Start()
    {
        c = C.GetComponent<CameraMovementBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            c.MinPosition += CameraChange;
            c.MaxPosition += CameraChange;
            collision.transform.position += PlayerChange;
        }
    }
}
