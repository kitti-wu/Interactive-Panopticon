using UnityEngine;
using System.Collections;

public class UElevator : MonoBehaviour {
	public static UElevator thisC;
	private void Awake(){
		thisC=this;
	}
	private void Update(){
		if(Time.frameCount%20==0){
			this.transform.Find("MoveDirText").GetComponent<UnityEngine.UI.Text>().text=Elevator.thisC.moveDir.ToString();
			this.transform.Find("LayerIndexText").GetComponent<UnityEngine.UI.Text>().text=(Elevator.thisC.targetLayerIndex+1).ToString();
			foreach(Transform loopTra in this.transform.Find("OutSide")){
				int tempLayerIndex=loopTra.GetSiblingIndex();
				if(Elevator.thisC.dicLayerIndexAndMoveDirs.ContainsKey(tempLayerIndex)){
					int[] tempMoveDirs=Elevator.thisC.dicLayerIndexAndMoveDirs[tempLayerIndex];
					loopTra.GetChild(1).GetComponent<UnityEngine.UI.Toggle>().isOn=(tempMoveDirs[0]==1);
					loopTra.GetChild(0).GetComponent<UnityEngine.UI.Toggle>().isOn=(tempMoveDirs[1]==1);
				}
				else{
					loopTra.GetChild(1).GetComponent<UnityEngine.UI.Toggle>().isOn=false;
					loopTra.GetChild(0).GetComponent<UnityEngine.UI.Toggle>().isOn=false;
				}
			}
			foreach(Transform loopTra in this.transform.Find("InSide")){
				int tempLayerIndex=loopTra.GetSiblingIndex();
				if(Elevator.thisC.listAimLayerIndex.Contains(tempLayerIndex)){
					loopTra.GetChild(0).GetComponent<UnityEngine.UI.Toggle>().isOn=true;
				}
				else{
					loopTra.GetChild(0).GetComponent<UnityEngine.UI.Toggle>().isOn=false;
				}
			}
		}
	}
	private void LateUpdate(){
		GameObject tempClick = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		if(tempClick!=null&&tempClick.transform.IsChildOf(this.transform)){
			if(Input.GetMouseButtonUp(0)){
				if(tempClick.transform.parent.parent.name=="OutSide"){
					if(tempClick.name=="DownToggle"){
						int tempLayerIndex=tempClick.transform.parent.GetSiblingIndex();
						Elevator.thisC.OutSidePutDown(tempLayerIndex,-1,tempClick.GetComponent<UnityEngine.UI.Toggle>().isOn);
					}
					else if(tempClick.name=="UpToggle"){
						int tempLayerIndex=tempClick.transform.parent.GetSiblingIndex();
						Elevator.thisC.OutSidePutDown(tempLayerIndex,1,tempClick.GetComponent<UnityEngine.UI.Toggle>().isOn);
					}
				}
				else if(tempClick.transform.parent.parent.name=="InSide"){
					if(tempClick.name=="Toggle"){
						int tempLayerIndex=tempClick.transform.parent.GetSiblingIndex();
						Elevator.thisC.InSidePutDown(tempLayerIndex,tempClick.GetComponent<UnityEngine.UI.Toggle>().isOn);
					}
				}
			}
		}
	}
}
