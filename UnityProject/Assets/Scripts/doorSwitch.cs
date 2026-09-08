using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSwitch : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    GameObject obj;
    private bool isClick01 = false;
    private bool isClick02 = false;
    private bool isClickedLight = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                Debug.Log("点击对象的名字是"+obj.name);
                //二楼的门
                if (obj.name.Equals("doorshaft"))
                {
                    if (!isClick01)
                    {
                        obj.transform.Rotate(new Vector3(0, 90, 0));
                        isClick01 = true;
                    }
                    else
                    {
                        obj.transform.Rotate(new Vector3(0, -90, 0));
                        isClick01 = false;
                    }
                }
                //一楼的门
                if (obj.name.Equals("doorshaft02"))
                {
                    if (!isClick02)
                    {
                        obj.transform.Rotate(new Vector3(0, 90, 0));
                        isClick02 = true;
                    }
                    else
                    {
                        obj.transform.Rotate(new Vector3(0, -90, 0));
                        isClick02 = false;
                    }
                }
                //灯开关
                if (obj.name.Equals("Switch.001"))
                {
                    if (!isClickedLight)
                    {
                        obj.transform.Rotate(new Vector3(0, 30, 0));
                        isClickedLight = true;
                    }
                    else
                    {
                        obj.transform.Rotate(new Vector3(0, -30, 0));
                        isClickedLight = false;
                    }
                }
            }
        }
    }
}