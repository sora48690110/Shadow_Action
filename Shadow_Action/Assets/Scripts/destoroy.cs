using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Destroy(gameObject);
            gameObject.transform.Rotate(new Vector3(0, 0, 5));
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }
}
