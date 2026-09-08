using UnityEngine;

/// <summary>
/// 指示灯.
/// </summary>
public class HightLED : MonoBehaviour
{
    private Material mat;
    public HightLED thisC;
    private int i = 0;
    public AudioSource music;
    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        thisC = this;
        InvokeRepeating("delayOpen", 1, 1);
        SetEmission(mat, false);
        music.Play();
    }
    float nextTime;
    float rate = 0.08f;
    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + rate;
            //做某事的代码
            SetEmission(mat, false);
            i++;
        }
        if (Time.time > nextTime)
        {
            nextTime = Time.time + rate;
            //做某事的代码
            SetEmission(mat, true);
        }
       
        
        
    }

    //private void Update()
    //{
        
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {           
    //        while (i >= 0)
    //        {
    //            InvokeRepeating("delayOpen", 1, 1);
    //            SetEmission(mat, false);
    //            i--;
    //        }

    //    }
       
    //}
    void delayOpen()
    {
        SetEmission(mat, true);
    }


    public void SetEmission(Material mat, bool emissionOn)
    {
        if (emissionOn)
        {
            mat.EnableKeyword("_EMISSION");
        }
        else
        {
            mat.DisableKeyword("_EMISSION");
        }
    }
}
