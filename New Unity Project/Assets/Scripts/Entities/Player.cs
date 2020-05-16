using UnityEngine;


public class Player : Entity
{
    public Animator Anime => gameObject.GetComponent<Animator>();
    public int money, exp, Lvl;
    public GameObject intro, blood;
    public SceneManagementHandler SMH;
    public Sprite DeadSprite;
    public float timer = 5f;

    public Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if (!intro.activeSelf)
        {
            transform.position = transform.position + Movement * Speed * Time.deltaTime;
        }
        if (!Alive())
        {
            timer-= .1f;
            if (timer <= 0)
            {
                SMH.LoadCurrentScene();
            }
            Speed = 0;
            Debug.Log("destroyyyed:" + gameObject.name);
            gameObject.GetComponent<SpriteRenderer>().sprite = DeadSprite;
        }
        if(Movement != Vector3.zero)
        {
            anim.SetFloat("xInput", Movement.x);
            anim.SetFloat("yInput", Movement.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Health -= collision.gameObject.GetComponent<Entity>().Attack;
            Instantiate(blood, gameObject.transform.position, Quaternion.identity);
        }
    }
}
