using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public PlayerMovement pm;

    void Start()
    {
        transform.position = new Vector3(-5.98f, -5.55f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>().hasKey)
        {
            collision.gameObject.GetComponent<PlayerMovement>().WinGame();
        }
    }
}
