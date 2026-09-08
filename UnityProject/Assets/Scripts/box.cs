using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    GameObject obj;
   
    private bool isCoverOpen;
    public bool isSwitch01Clicked;//射灯
    public bool isSwitch02Clicked;//一楼灯光
    public bool isSwitch03Clicked;//二楼灯光
    public bool isSwitch04Clicked;//三四楼灯光
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    void Start()
    {
        isCoverOpen = false;
        isSwitch01Clicked = false;
        isSwitch02Clicked = false;
        isSwitch03Clicked = false;
        isSwitch04Clicked = false;
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("点击鼠标左键");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                //箱体遮罩开关
                if (obj.name == "cover")
                {
                    if (isCoverOpen == false)
                    {
                        obj.transform.Rotate(new Vector3(0, 0,120));
                        isCoverOpen = true;
                    }
                    else
                    {
                        obj.transform.Rotate(new Vector3(0, 0,-120));
                        isCoverOpen = false;
                    }
                }
                //射灯
                if (obj.name == "light.001")
                {
                    
                    if (!isSwitch01Clicked)
                    {
                        light1.SetActive(true);
                        isSwitch01Clicked = true;
                    }
                    else
                    {
                        light1.SetActive(false);
                        isSwitch01Clicked = false;
                    }

                }
                if (obj.name == "light.002")
                {
                    if (!isSwitch02Clicked)
                    {
                        light2.SetActive(true);
                        isSwitch02Clicked = true;
                    }
                    else
                    {
                        light2.SetActive(false);
                        isSwitch02Clicked = false;
                    }
                }
                if (obj.name == "light.003")
                {
                    if (!isSwitch03Clicked)
                    {
                        light3.SetActive(true);
                        isSwitch03Clicked = true;
                    }
                    else
                    {
                        light3.SetActive(false);
                        isSwitch03Clicked = false;
                    }
                }
                if (obj.name == "light.004")
                {
                    if (!isSwitch04Clicked)
                    {
                        light4.SetActive(true);
                        isSwitch04Clicked = true;
                    }
                    else
                    {
                        light4.SetActive(false);
                        isSwitch04Clicked = false;
                    }
                }
            }
        }
    }
}