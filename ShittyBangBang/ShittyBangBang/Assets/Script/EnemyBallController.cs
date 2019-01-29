using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallController : MonoBehaviour
{
    public float force = 10.0f;
    private Rigidbody2D rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("pfPlayerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = player.transform.position - this.transform.position;
        rb.AddForce(new Vector2(moveDir.x, 0.0f).normalized * force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<SpriteAnimatorCycler>())
        {
            Debug.Log("BALL HIT By PROJECTILE");
            GameObject.Destroy(collision.gameObject);
            Pop();
        }
    }

    private void Pop()
    {
        if (this.transform.localScale.x > 0.3)
        {
            GameObject leftChild = (GameObject)Instantiate(this.gameObject);
            leftChild.transform.localScale = this.transform.localScale * 0.5f;
            leftChild.transform.position = this.transform.position + Vector3.left * 0.1f;
            GameObject rightChild = (GameObject)Instantiate(this.gameObject);
            rightChild.transform.localScale = this.transform.localScale * 0.5f;
            rightChild.transform.position = this.transform.position + Vector3.right * 0.1f;
        }
        GameObject.Destroy(this.gameObject);
    }
}
