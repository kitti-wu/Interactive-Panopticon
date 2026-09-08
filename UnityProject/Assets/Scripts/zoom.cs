using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    GameObject obj;
    public Transform player1Transform;
    public Vector3 position;
    public Transform Cam_transform;
    private bool flag = false;
    void Start()
    {
        Cam_transform = Camera.main.transform;

    }
    void Update()
    {
        //检测用户点击鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            flag = true;
            //Debug.Log("点击鼠标左键");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //记录点击位置
                position = Camera.main.transform.position;
                //Debug.Log(hit.collider.gameObject.name);
                Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                obj = hit.collider.gameObject;
                //通过tag检测物体
                if (obj.CompareTag("GameController"))
                {
                    //Debug.Log("flag" + flag);
                    //Cam_transform.transform.Translate(0,1,0);
                    Cam_transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //debug.log("退出" + flag);
            if (flag == true)
            {
                Cam_transform.position = position;
            }
        }
    }
}
