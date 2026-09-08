using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPort1 : MonoBehaviour
{
    private Animator modelAni;
    private string doorStateStr;
    public bool isRun { get; set; }
    private bool isShield;
    private bool isCheckRay;
    public ElevatorRoom elevatorRoomC { get; set; }
    private Transform rayATra;
    private Transform rayBTra;
    private void Awake()
    {
        modelAni = this.transform.Find("Door").GetComponent<Animator>();
        doorStateStr = "Close";
        isRun = false;
        isShield = false;
        isCheckRay = false;
        elevatorRoomC = null;
        rayATra = modelAni.transform.Find("A/RayA");
        rayBTra = modelAni.transform.Find("B/RayB");
    }
    private void Update()
    {
        if (isCheckRay)
        {
            CheckRay();
        }
    }
    public void ToOpen(ElevatorRoom theElevatorRoomC)
    {
        if (isRun) return;
        if (!modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorPort_Door_Close")) return;
        if (doorStateStr == "Open") return;
        isRun = true;
        elevatorRoomC = theElevatorRoomC;
        StartCoroutine(CheckDoorStateIEnumerator());
    }
    private IEnumerator CheckDoorStateIEnumerator()
    {
        modelAni.ResetTrigger(doorStateStr);
        modelAni.Update(0);
        doorStateStr = "Open";
        modelAni.SetTrigger(doorStateStr);
        bool tempIsWaitElevatorRoomDoor = false;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (!tempIsWaitElevatorRoomDoor)
            {
                if (modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorPort_Door_Open"))
                {
                    //现在打开电梯房门
                    elevatorRoomC.ToOpen();
                    tempIsWaitElevatorRoomDoor = true;
                }
            }
            else
            {
                if (elevatorRoomC.CheckDoor())
                {
                    //把电梯端的门也关上
                    yield return new WaitForSeconds(0.2f);
                    break;
                }
            }
        }
        bool tempIsFinish = false;
        while (!tempIsFinish)
        {
            isShield = false;
            isCheckRay = true;
            if (doorStateStr != "Close")
            {
                modelAni.ResetTrigger(doorStateStr);
                modelAni.Update(0);
                doorStateStr = "Close";
                modelAni.SetTrigger(doorStateStr);
            }
            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                //检查遮挡
                if (isShield)
                {
                    //重新进入开门状态
                    if (modelAni.speed != -1f)
                    {
                        modelAni.speed = -1f;
                        yield return new WaitForSeconds(2f);
                        modelAni.speed = 1f;
                        break;
                    }
                }
                else
                {
                    if (modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorPort_Door_Close"))
                    {
                        //成功把门关上
                        tempIsFinish = true;
                        break;
                    }
                }
            }
        }
        isRun = false;
        isShield = false;
        isCheckRay = false;
    }
    public bool CheckDoor()
    {
        bool returnBool = false;
        returnBool = (modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorPort_Door_Close"));
        return returnBool;
    }
    private void CheckRay()
    {
        Debug.DrawRay(rayATra.position + new Vector3(0.1f, 0f, 0f), -rayATra.up * 2.5f, Color.red);
        Debug.DrawRay(rayBTra.position + new Vector3(-0.1f, 0f, 0f), -rayBTra.up * 2.5f, Color.red);
        if (!isShield)
        {
            if (Physics.Raycast(rayATra.position + new Vector3(0.1f, 0f, 0f), -rayATra.up, 2.5f))
            {
                isShield = true;
            }
        }
        if (!isShield)
        {
            if (Physics.Raycast(rayBTra.position + new Vector3(-0.1f, 0f, 0f), -rayBTra.up, 2.5f))
            {
                isShield = true;
            }
        }
    }
}
