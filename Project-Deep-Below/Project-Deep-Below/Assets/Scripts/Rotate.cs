using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Camera cam;
    private bool test = false;
    // Update is called once per frame
    void Update()
    {
        if(cam.isActiveAndEnabled && !test)
        {
            RotatePlayer(10);
        }
        else if(test && !cam.isActiveAndEnabled)
        {
            RotatePlayer(-10);
        }
    }

    private void RotatePlayer(float degrees)
    {
        this.transform.Rotate(new Vector3(0, degrees, 0));
        test = !test;
    }
}
