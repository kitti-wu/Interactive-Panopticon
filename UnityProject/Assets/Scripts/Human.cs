using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {
    public AudioSource walk;
    Vector3 stair = new Vector3(-0.2f, 0.25f,0.25f);
    public Vector3 L = new Vector3(-0.2f, 12f, 0.1f);
    public Vector3 L1 = new Vector3(0.2f, 25f, -0.2f);
    public static Human human;
    Vector3 t;
    private void Start()
    {
        human = this;
        t = this.transform.position;
    }
    private void Update(){
		Vector3 tempMoveDir = Input.GetAxis ("Horizontal")*this.transform.right+Input.GetAxis ("Vertical")*this.transform.forward;
       

        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            walk.Play();
        }
        else
        {
            walk.Pause();
        }

            if (tempMoveDir!=Vector3.zero){
			this.transform.position+=tempMoveDir*Time.deltaTime*2f;           
		}
		if(Input.GetMouseButton(1)){
           
            this.transform.Rotate (Vector3.up*Input.GetAxis("Mouse X")*3f);
            //this.transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * 2f);
        }
        if (Input.GetKey(KeyCode.G))
        {
            this.transform.position = L1;
            this.transform.Rotate(270, 180, 0);
            
        }
        if (Input.GetKey(KeyCode.O))
        {
            t.y = 6.69f;
            t.x = this.transform.position.x;
            t.z = this.transform.position.z;
            this.transform.position = t;
        }


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("stair"))
        {
            this.transform.position += stair;
        }
    }
    public void lie()
    {
        this.transform.Rotate(-90, 0,0);
        this.transform.position = L;
    }
}
