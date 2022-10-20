using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private new Camera camera;
    private float screenWidth;
    private float screenHeight;
    private float borderX;
    private float borderY;
    public GameObject cherry;
    public Tweener tweener;
    private GameObject cherryReference;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cherryMovement());
        camera = gameObject.GetComponent<Camera>();
        screenWidth = camera.ViewportToWorldPoint(new Vector3(1, 1, gameObject.GetComponent<Camera>().nearClipPlane)).x;
        screenHeight = camera.ViewportToWorldPoint(new Vector3(1, 1, gameObject.GetComponent<Camera>().nearClipPlane)).y;
        borderX = screenWidth + 1;
        borderY = screenHeight + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (cherryReference != null)
        {
            if (tweener.tweenEnded)
            {
                Destroy(cherryReference);
            }
        }
    }

    IEnumerator cherryMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            int choice = Random.Range(1, 5); //1 is top, 2 is left, 3 is right, 4 is down
            if (choice == 1) //Top
            {
                float topX = Random.Range(-borderX, borderX + 1);
                float topY = borderY;
                cherryReference = Instantiate(cherry, new Vector3(topX, topY, 0), Quaternion.identity);
                Vector3 targetPos = cherryReference.transform.position;
                targetPos.x = -targetPos.x;
                targetPos.y = -targetPos.y;
                tweener.AddTween(cherryReference.transform, cherryReference.transform.position, targetPos, 7);
            }
            else if (choice == 2) //Left
            {
                float leftX = -borderX;
                float leftY = Random.Range(-borderY, borderY + 1);
                cherryReference = Instantiate(cherry, new Vector3(leftX, leftY, 0), Quaternion.identity);
                Vector3 targetPos = cherryReference.transform.position;
                targetPos.x = -targetPos.x;
                targetPos.y = -targetPos.y;
                tweener.AddTween(cherryReference.transform, cherryReference.transform.position, targetPos, 7);
            }
            else if (choice == 3) //Right
            {
                float rightX = borderX;
                float rightY = Random.Range(-borderY, borderY + 1);
                cherryReference = Instantiate(cherry, new Vector3(rightX, rightY, 0), Quaternion.identity);
                Vector3 targetPos = cherryReference.transform.position;
                targetPos.x = -targetPos.x;
                targetPos.y = -targetPos.y;
                tweener.AddTween(cherryReference.transform, cherryReference.transform.position, targetPos, 7);
            }
            else if (choice == 4) //Down
            {
                float downX = Random.Range(-borderX, borderX + 1);
                float downY = -borderY;
                cherryReference = Instantiate(cherry, new Vector3(downX, downY, 0), Quaternion.identity);
                Vector3 targetPos = cherryReference.transform.position;
                targetPos.x = -targetPos.x;
                targetPos.y = -targetPos.y;
                tweener.AddTween(cherryReference.transform, cherryReference.transform.position, targetPos, 7);
            }
        }
    }
}
