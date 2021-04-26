using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool detected = false;
    private int timer = 0;
    public PlayerMovement pm;
    public EnemyController enemy1;
    public EnemyController enemy2;
    public EnemyController enemy3;
    public Sprite inactiveSprite;
    public Sprite detectedSprite;
    public Vector3 startpos;
    public Vector3 corner2;
    public Vector3 corner3;
    public Vector3 corner4;
    public EnemyType enemyType;
    private bool facingright = true;

    public enum EnemyType
    {
        Line,
        Box
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetEnemy();
    }

    void Update()
    {
        timer++;
        CheckDistanceToPlayer();
        if (detected)
        {
            ChasePlayer();
        }
        else if (enemyType == EnemyType.Line)
        {
            if (timer > 360)
            {
                if ((transform.position - startpos).sqrMagnitude < 0.01f)
                {
                    StopAllCoroutines();
                    StartCoroutine(MoveTowards(transform.position, corner2, 3f));
                    timer = 0;
                }
                else if ((transform.position - corner2).sqrMagnitude < 0.01f)
                {
                    StopAllCoroutines();
                    StartCoroutine(MoveTowards(transform.position, startpos , 3f));
                    timer = 0;
                }
            }
        }
        else if (enemyType == EnemyType.Box)
        {
            if (timer > 360)
            {
                if ((transform.position - startpos).sqrMagnitude < 0.01f)
                {
                    StopAllCoroutines();
                    StartCoroutine(MoveTowards(transform.position, corner2, 3f));
                    timer = 0;
                }
                else if ((transform.position - corner2).sqrMagnitude < 0.01f)
                {
                    StopAllCoroutines();
                    StartCoroutine(MoveTowards(transform.position, corner3, 3f));
                    timer = 0;
                }
                else if ((transform.position - corner3).sqrMagnitude < 0.01f)
                {
                    StopAllCoroutines();
                    StartCoroutine(MoveTowards(transform.position, corner4, 3f));
                    timer = 0;
                }
                else if ((transform.position - corner4).sqrMagnitude < 0.01f)
                {
                    StopAllCoroutines();
                    StartCoroutine(MoveTowards(transform.position, startpos, 3f));
                    timer = 0;
                }
            }
        }
    }

    void ChasePlayer()
    {
        Debug.Log("You are being chased!");
        StopAllCoroutines();
        this.transform.position = Vector3.MoveTowards(this.transform.position, pm.transform.position, .005f);
        DirectionCheck(pm.transform.position);
    }

    void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(pm.transform.position, this.transform.position);
        if (distance < 4)
        {
            detected = true;
            this.GetComponent<SpriteRenderer>().sprite = detectedSprite;
            ChasePlayer();
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
            detected = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<PlayerMovement>().ResetPlayer();
            enemy1.ResetEnemy();
            enemy2.ResetEnemy();
            enemy3.ResetEnemy();
        }
    }

    private void ResetEnemy()
    {
        detected = false;
        transform.position = startpos;
        facingright = true;
        Vector3 theScale = transform.localScale;
        theScale.x = 1;
        transform.localScale = theScale;
    }

    IEnumerator MoveTowards(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            DirectionCheck(destination);
            this.transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void DirectionCheck(Vector3 destination)
    {
        if (destination.x > transform.position.x && !facingright)
        {
            facingright = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (destination.x < transform.position.x && facingright)
        {
            facingright = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}


