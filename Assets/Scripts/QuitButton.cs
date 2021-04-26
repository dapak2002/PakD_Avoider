using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(2.34f, -3.75f, 0f);
    }

    private void OnMouseUp()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
