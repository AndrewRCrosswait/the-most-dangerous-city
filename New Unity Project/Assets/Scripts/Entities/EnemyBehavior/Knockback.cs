using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D Enemy = other.GetComponent<Rigidbody2D>();
            if (Enemy != null)
            {
                
                Enemy.isKinematic = false;
                Vector2 difference = Enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                Enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(HitFeedback(other));
                StartCoroutine(KnockCo(Enemy));
                

            }
        }
    }
    private IEnumerator HitFeedback(Collider2D collider2D)
    {
        // Code to change player character red when hit.     

        collider2D.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        collider2D.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator KnockCo(Rigidbody2D Enemy)
    {
        if(Enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            Enemy.velocity = Vector2.zero;
            Enemy.isKinematic = true;
        }
        Enemy.bodyType = RigidbodyType2D.Dynamic;
    }


}
