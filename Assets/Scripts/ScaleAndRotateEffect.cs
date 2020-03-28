using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndRotateEffect : MonoBehaviour
{
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
            if (gameObject.transform.localScale.x <= 0.3f)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            }
            else
            {
                isIncrement = false;
            }
        }
        else
        {
            if (gameObject.transform.localScale.x >= 0.2f)
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            }
            else
            {
                isIncrement = true;
            }
        }
    }
}
