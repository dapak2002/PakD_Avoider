using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public KeyManager key;
    public float speed;
    private bool facingright = true;
    public bool hasKey = false;
    private float ClickTime;
    private bool doubleClicked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-6, -2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - ClickTime;
            if (timeSinceLastClick <= .2f)
            {
                doubleClicked = true;
            }
            ClickTime = Time.time;

            if (doubleClicked)
            {
                Debug.Log("Dash!");
                speed = 15f;
                Invoke("ResetSpeed", 1.5f);
            }

            if (mouseInSpace.x > transform.position.x && !facingright)
            {
                facingright = true;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            else if (mouseInSpace.x < transform.position.x && facingright)
            {
                facingright = false;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            StopAllCoroutines();
            StartCoroutine(MoveTowards(transform.position, mouseInSpace, speed));
        }
    }

    IEnumerator MoveTowards(Vector3 start, Vector3 destination, float speed)
    {
        while((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void ResetSpeed()
    {
        speed = 7f;
        doubleClicked = false;
    }

    public void ResetPlayer()
    {
        Debug.Log("You have been caught!");
        transform.position = new Vector3(-6, -2, 0);
        hasKey = false;
        key.resetKey();
        StopAllCoroutines();
    }

    public void WinGame()
    {
        SceneManager.LoadScene("EndScene");
    }
}
