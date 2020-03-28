using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndRotateEffect : MonoBehaviour
{
    public float minScaleX;
    public float maxScaleX;
    public float speed = 1f;
    bool isIncrement = true;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime * 2f, 0);

        if (isIncrement)
        {
            if (gameObject.transform.localScale.x <= maxScaleX)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * speed;
            }
            else
            {
                isIncrement = false;
            }
        }
        else
        {
            if (gameObject.transform.localScale.x >= minScaleX)
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * speed;
            }
            else
            {
                isIncrement = true;
            }
        }
    }
}
