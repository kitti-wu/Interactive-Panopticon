using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public static Buttons thisC;
    private bool flag=true;
    private bool flag1 = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Ray ray;
    RaycastHit hit;
    GameObject obj;
   
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
                //Elevator.thisC.InSidePutDown(0, true);
                //Elevator1.thisC1.InSidePutDown(0, true);
                //通过名字
                if (obj.CompareTag("First"))
                {
                    Elevator.thisC.InSidePutDown(0, true);
                }
                if (obj.CompareTag("Second"))
                {
                    Elevator.thisC.InSidePutDown(1, true);
                }
                if (obj.CompareTag("Third"))
                {
                    Elevator.thisC.InSidePutDown(2, true);
                }
                if (obj.CompareTag("1"))
                {
                    Elevator1.thisC1.InSidePutDown(0, true);
                }
                if (obj.CompareTag("2"))
                {
                    Elevator1.thisC1.InSidePutDown(1, true);
                }
                if (obj.CompareTag("3"))
                {
                    Elevator1.thisC1.InSidePutDown(2, true);
                }
                if (obj.name.Equals("上"))
                {
                    Debug.Log("dd");
                    Elevator.thisC.OutSidePutDown(0, 1, true);
                }
                if (obj.name.Equals("上1"))
                {
                    Debug.Log("1");
                    Elevator1.thisC1.OutSidePutDown(0, 1, true);
                }
                if (obj.name.Equals("up1"))
                {
                    Elevator.thisC.OutSidePutDown(1, 1, true);
                }
                if (obj.name.Equals("up2"))
                {
                    Elevator1.thisC1.OutSidePutDown(1, 1, true);
                }
                if (obj.name.Equals("up3"))
                {
                    Elevator1.thisC1.OutSidePutDown(2, 1, true);
                }
                if (obj.name.Equals("Up2"))
                {
                    Elevator.thisC.OutSidePutDown(2, 1, true);
                }
                if (obj.name.Equals("Up3"))
                {
                    Elevator1.thisC1.OutSidePutDown(0, 1, true);
                }
                if (obj.name.Equals("Down"))
                {
                    Elevator.thisC.OutSidePutDown(0, -1, true);
                }
                if (obj.name.Equals("down1"))
                {
                    Elevator.thisC.OutSidePutDown(1, -1, true);
                }
                if (obj.name.Equals("down2"))
                {
                    Elevator1.thisC1.OutSidePutDown(1, -1, true);
                }
                if (obj.name.Equals("down3"))
                {
                    Elevator1.thisC1.OutSidePutDown(2, -1, true);
                }
                if (obj.name.Equals("下2"))
                {
                    Debug.Log("1");
                    Elevator.thisC.OutSidePutDown(2, -1, true);
                }
                if(obj.name.Equals("alarmingController"))
                {
                    if (flag1)
                    {
                        GameObject.Find("alarming").GetComponent<HightLED>().enabled = true;
                        Debug.Log("11");
                        flag1 = false;
                    }
                    else
                    {
                        GameObject.Find("alarming").GetComponent<HightLED>().enabled = false;
                        Debug.Log("22");
                        flag1 = true;
                    }
                }
                if(obj.name.Equals("victorian_bed_mattres_bed_mattres_material_0"))
                {
                    Human.human.lie();
                }
                
            }
        }
    }
}
