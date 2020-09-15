using UnityEngine;


public class Player : Entity
{
    public Animator Anime => gameObject.GetComponent<Animator>();
    public GameObject DialogueBox, Blood;
    public SceneManagementHandler SMH;
    public Sprite DeadSprite;
    public float Timer = 5f;

    public Animator Anim;

    public int NpcInRange { get; private set; } = 0; // For counting number of NPCs in circle around playerTransform.

    // Start is called before the first frame update
    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if (!DialogueBox.activeSelf)
        {
            transform.position = transform.position + Movement * Speed * Time.deltaTime;
        }
        if (!Alive())
        {
            Timer -= .1f;
            if (Timer <= 0)
            {
                SMH.LoadCurrentScene();
            }
            Speed = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = DeadSprite;
        }
        if (Movement != Vector3.zero)
        {
            Anim.SetFloat("xInput", Movement.x);
            Anim.SetFloat("yInput", Movement.y);
            Anim.SetBool("isMoving", true);
        }
        else
        {
            Anim.SetBool("isMoving", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health -= collision.gameObject.GetComponent<Entity>().Attack;
            Instantiate(Blood, gameObject.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "Character")
        {
            NpcInRange++;
            Debug.Log($"NPCs who entered Circle: {NpcInRange}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            NpcInRange--;
            Debug.Log($"NPCs who entered Circle: {NpcInRange}");
        }
    }
}
