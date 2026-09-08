using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doortrigger1 : MonoBehaviour
{
    private door1 m_Door;
    private door1 m_Door1;
    private bool enterCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Door = GameObject.Find("doorshaft").GetComponent<door1>();
        m_Door1 = GameObject.Find("doorshaft02").GetComponent<door1>();
    }
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
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
            if (Input.GetKeyDown(KeyCode.K))
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
