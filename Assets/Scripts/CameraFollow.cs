using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerMovement pm;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            pm.transform.position.x,
            pm.transform.position.y,
            this.transform.position.z);
    }
}
