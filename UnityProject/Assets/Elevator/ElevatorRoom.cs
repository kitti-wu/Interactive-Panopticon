using UnityEngine;
using System.Collections;

public class ElevatorRoom : MonoBehaviour {
	private Animator modelAni;
	private string doorStateStr;
	public bool isRun{ get; set;}
	private bool isShield;
	private bool isCheckRay;
	private Transform rayATra;
	private Transform rayBTra;
	private void Awake(){
		modelAni=this.transform.Find("Door").GetComponent<Animator>();
		doorStateStr = "Close";
		isRun = false;
		isShield = false;
		isCheckRay = false;
		rayATra = modelAni.transform.Find ("A/RayA");
		rayBTra = modelAni.transform.Find ("B/RayB");
	}
	private void Update(){
		if(isCheckRay){
			CheckRay();
		}
	}
	public void ToOpen(){
		if(isRun)return;
		if(!modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorRoom_Door_Close"))return;
		if(doorStateStr=="Open")return;
		isRun = true;
		StartCoroutine (CheckDoorStateIEnumerator());
	}
	private IEnumerator CheckDoorStateIEnumerator(){
		modelAni.ResetTrigger (doorStateStr);
		modelAni.Update (0);
		doorStateStr = "Open";
		modelAni.SetTrigger (doorStateStr);
		while(true){
			yield return new WaitForSeconds (0.2f);
			if(modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorRoom_Door_Open")){
				//数秒后关门
				yield return new WaitForSeconds (2f);
				break;
			}
		}
		bool tempIsFinish = false;
		while(!tempIsFinish){
			isShield=false;
			isCheckRay=true;
			if(doorStateStr!="Close"){
				modelAni.ResetTrigger (doorStateStr);
				modelAni.Update (0);
				doorStateStr = "Close";
				modelAni.SetTrigger (doorStateStr);
			}
			while(true){
				yield return new WaitForSeconds (0.02f);
				//检查遮挡
				if(isShield){
					//重新进入开门状态
					if(modelAni.speed!=-1f){
						modelAni.speed=-1f;
						yield return new WaitForSeconds (2f);
						modelAni.speed=1f;
						break;
					}
				}
				else{
					if(modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorRoom_Door_Close")){
						//成功把门关上
						tempIsFinish=true;
						break;
					}
				}
			}
		}
		isRun = false;
		isShield = false;
		isCheckRay = false;
	}
	public bool CheckDoor(){
		bool returnBool = false;
		returnBool = (modelAni.GetCurrentAnimatorStateInfo(0).IsName("ElevatorRoom_Door_Close"));
		return returnBool;
	}
	private void CheckRay(){
		Debug.DrawRay (rayATra.position+new Vector3(0.1f,0f,0f),-rayATra.up*2.5f,Color.red);
		Debug.DrawRay (rayBTra.position+new Vector3(-0.1f,0f,0f),-rayBTra.up*2.5f,Color.red);
		if(!isShield){
			if(Physics.Raycast(rayATra.position+new Vector3(0.1f,0f,0f),-rayATra.up,2.5f)){
				isShield=true;
			}
		}
		if(!isShield){
			if(Physics.Raycast(rayBTra.position+new Vector3(-0.1f,0f,0f),-rayBTra.up,2.5f)){
				isShield=true;
			}
		}
	}
}
