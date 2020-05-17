using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public string info, RoomName;
    public Vector2 CameraChange;
    public Vector3 PlayerChange;
    private CameraMovementBehavior MovementBehavior;
    public GameObject TargetCamera;

    // Start is called before the first frame update
    void Start()
    {
        MovementBehavior = TargetCamera.GetComponent<CameraMovementBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            MovementBehavior.MinPosition += CameraChange;
            MovementBehavior.MaxPosition += CameraChange;
            collision.transform.position += PlayerChange;
        }
    }
}
