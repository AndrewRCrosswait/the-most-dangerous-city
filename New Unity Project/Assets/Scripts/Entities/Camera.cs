using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 Distance;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (objectToFollow == null) return;

        float dist = Vector2.Distance(transform.position, objectToFollow.transform.position);

        if (dist > 0.5f)
        {
            Vector3 MoveTo = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, transform.position.z);

            float speed = (dist + 10) * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, MoveTo, speed);
        }
    }
}