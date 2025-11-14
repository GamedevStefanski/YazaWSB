using UnityEngine;

public class MovementScriptRanged : MonoBehaviour
{
    [Header("Prêdkoœæ ruchu")]
    public float moveSpeed = 1f;

    private bool canMove = true;
    private Rigidbody2D rb;
    private string ownTag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownTag = gameObject.tag;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            // ruch w kierunku, w który obrócony jest obiekt
            rb.MovePosition(rb.position + (Vector2)transform.right * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != ownTag && collision.isTrigger == false)
        {
            canMove = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        canMove = true;
    }
}