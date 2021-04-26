using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public PlayerMovement pm;
    private bool followPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(18.71f, 11.69f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pm.transform.position, .02f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerMovement>().hasKey = true;
        followPlayer = true;
        this.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator MoveTowards(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void resetKey()
    {
        transform.position = new Vector3(18.71f, 11.69f, 0f);
        this.GetComponent<Collider2D>().enabled = true;
        followPlayer = false;
    }
}
