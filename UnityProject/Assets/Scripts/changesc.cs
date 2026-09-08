using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class changesc : MonoBehaviour
{
    public GameObject changebtn;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(News);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void News()
    {
        SceneManager.LoadScene("Area light");
    }
}
