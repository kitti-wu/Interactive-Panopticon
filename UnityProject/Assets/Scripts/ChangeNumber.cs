using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class ChangeNumber : MonoBehaviour
{
    public Texture one;
    public Texture two;

    public Texture2D myTexture;
    private string serialNumber;
    private int number = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        number = Elevator.thisC.targetLayerIndex + 1;
        serialNumber = Convert.ToString(number);//变量类型转换
        myTexture = (Texture2D)Resources.Load(serialNumber);//使用Resources.Load动态加载当前图像
        GetComponent<Renderer>().material.mainTexture = myTexture;//将当前模型纹理进行修改
    }
    public void Change()
    {
        number = Elevator.thisC.targetLayerIndex + 1;
        serialNumber = Convert.ToString(number);//变量类型转换
        myTexture = (Texture2D)Resources.Load(serialNumber);//使用Resources.Load动态加载当前图像
        GetComponent<Renderer>().material.mainTexture = myTexture;//将当前模型纹理进行修改
    }
}
