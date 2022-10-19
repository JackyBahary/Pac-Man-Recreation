using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public Tweener tweener;
    private string lastInput = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Storing Inputs into "lastInput" variable
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = "w";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = "a";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = "s";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = "d";
        }

        //Lerping Code
        if (!tweener.TweenExists(transform))
        {
            //Vector3 targetPos = transform.position;
            //targetPos.x = targetPos.x + 5;
            //tweener.AddTween(transform, transform.position, targetPos, 10);
            //Debug.Log("Going Again.");
            if (lastInput.Equals("d"))
            {
                Vector3 targetPos = transform.position;
                targetPos.x = targetPos.x + 1;
                tweener.AddTween(transform, transform.position, targetPos, 1);
                Debug.Log("Going D.");
            }
        }
    }
}
