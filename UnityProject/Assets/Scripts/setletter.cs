using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setletter : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject gameObject1;
    public GameObject gameObject2;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        gameObject1.SetActive(true);
        gameObject2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButtonClick()
    {
        Debug.Log("111");
        gameObject.SetActive(true);
        gameObject1.SetActive(false);
        gameObject2.SetActive(true);
    }
}
