using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooTrigger : MonoBehaviour
{
    private door m_Door;
    private door1 m_Door1;
    private bool enterCollide = false;
    Collider c;
    // Start is called before the first frame update
    void Start()
    {
        m_Door = GameObject.Find("bigdoorshaft").GetComponent<door>();
        m_Door1 = GameObject.Find("bigdoorshaft1").GetComponent<door1>();
       // m_Door= GameObject.Find("doorshaft").GetComponent<door>();
    }


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
        c = collider;
        enterCollide = true;
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("exit");
        enterCollide = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (enterCollide)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("rotate");
                if (m_Door.GetIsOpen())
                {
                    m_Door.CloseDoorMethod();
                    m_Door1.CloseDoorMethod();

                }
                else
                {
                    m_Door.OpenDoorMethod();
                    m_Door1.OpenDoorMethod();
                }
            }
        }

    }
}
