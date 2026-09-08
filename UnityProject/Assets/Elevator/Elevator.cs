using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Elevator : MonoBehaviour {
	public static Elevator thisC;
	public List<ElevatorPort> listElevatorPortC{ get; set;}
	public ElevatorRoom elevatorRoomC{ get; set;}
	public int targetLayerIndex{ get; set;}
	public int moveDir{ get; set;}
	public List<int> listAimLayerIndex{ get; set;}//记录电梯内激活楼层
	public Dictionary<int,int[]> dicLayerIndexAndMoveDirs{ get; set;}//记录电梯外激活楼层
	public bool isWait{ get; set;}
    public ChangeNumber c;
    public AudioSource music;
    public AudioSource music1;
    public AudioSource AudioSource { get; private set; }
    private void Awake(){
		thisC = this;
		listElevatorPortC = new List<ElevatorPort> ();
		foreach(Transform loopTra in this.transform.Find("ElevatorPorts")){
			listElevatorPortC.Add(loopTra.GetComponent<ElevatorPort>());
		}
		elevatorRoomC=this.transform.Find("ElevatorRoom").GetComponent<ElevatorRoom>();
		targetLayerIndex = 0;
		moveDir = 0;
		listAimLayerIndex = new List<int> ();
		dicLayerIndexAndMoveDirs = new Dictionary<int, int[]> ();
		isWait = false;
	}
	private void Start(){
		//楼梯房复位
		elevatorRoomC.transform.position=listElevatorPortC[targetLayerIndex].transform.position;
	}
	private void Update(){
		if(isWait){
			ElevatorPort tempElevatorPortC=listElevatorPortC[targetLayerIndex];
			if(tempElevatorPortC.CheckDoor()){
				//电梯内移除
				if(listAimLayerIndex.Contains(targetLayerIndex)){
					listAimLayerIndex.Remove(targetLayerIndex);
				}
				//电梯外移除
				if(dicLayerIndexAndMoveDirs.ContainsKey(targetLayerIndex)){
					if(moveDir==1){
						if(dicLayerIndexAndMoveDirs[targetLayerIndex][0]==1){
							dicLayerIndexAndMoveDirs[targetLayerIndex][0]=0;
						}
					}
					else if(moveDir==-1){
						if(dicLayerIndexAndMoveDirs[targetLayerIndex][1]==1){
							dicLayerIndexAndMoveDirs[targetLayerIndex][1]=0;
						}
					}
					if(dicLayerIndexAndMoveDirs[targetLayerIndex][0]==0&&dicLayerIndexAndMoveDirs[targetLayerIndex][1]==0){
						dicLayerIndexAndMoveDirs.Remove(targetLayerIndex);
					}
				}
				isWait=false;
				ToMove();
                
			}
			else{
				return;
			}
		}
		//正在移动中
		if(moveDir!=0){
            //所有电梯门已关闭
            
            if (CheckAllDoor()){
				//获取目标电梯端
				ElevatorPort tempElevatorPortC=listElevatorPortC[targetLayerIndex];
				float tempDis=Vector3.Distance(elevatorRoomC.transform.position,tempElevatorPortC.transform.position);
                
                //电梯房移动到目标电梯端
                if (tempDis>=0.2f){
					elevatorRoomC.transform.position=Vector3.MoveTowards(elevatorRoomC.transform.position,tempElevatorPortC.transform.position,Time.deltaTime*1.5f);
                    
                }
				//移动完成
				else{
					//如果该楼层被激活且是同方向
					if(listAimLayerIndex.Contains(targetLayerIndex)||(dicLayerIndexAndMoveDirs.ContainsKey(targetLayerIndex)&&
					   ((moveDir==1&&dicLayerIndexAndMoveDirs[targetLayerIndex][0]==1)||(moveDir==-1&&dicLayerIndexAndMoveDirs[targetLayerIndex][1]==1)))){
						//位置校正并执行开门
						elevatorRoomC.transform.position=tempElevatorPortC.transform.position;
						tempElevatorPortC.ToOpen(elevatorRoomC);
						isWait=true;
                        music.Play();
                        if(this.AudioSource.isPlaying)
                        {
                            music1.Play();
                        }
                    }
					else{
						ToMove();

					}
				}
			}
		}
	}
	private void ToMove(){
		if(listAimLayerIndex.Count==0&&dicLayerIndexAndMoveDirs.Count==0){
			moveDir=0;
		}
		else{
            
            //已激活楼层排序
            listAimLayerIndex.Sort ();
			//目标楼层递增
			if(moveDir==1){
				ToAdd();
			}
			//目标楼层递减
			else if(moveDir==-1){
				ToSub();
			}
		}
	}
	private void ToAdd(){
		if(moveDir==1){
			//获取最高层
			int tempMaxLayerIndex=-1;
			if(listAimLayerIndex.Count>=1){
				tempMaxLayerIndex=listAimLayerIndex[listAimLayerIndex.Count-1];
			}
			if(dicLayerIndexAndMoveDirs.Count>=1){
				foreach(int loopLayerIndex in dicLayerIndexAndMoveDirs.Keys){
					if(tempMaxLayerIndex==-1||loopLayerIndex>=tempMaxLayerIndex){
						tempMaxLayerIndex=loopLayerIndex;
					}
				}
			}
			if(tempMaxLayerIndex!=-1&&targetLayerIndex>=tempMaxLayerIndex){
				//还存在其他激活楼层
				if(listAimLayerIndex.Count>=1||dicLayerIndexAndMoveDirs.Count>=1){
					moveDir=-1;
				}
			}
			//未到顶层
			else{
				//逐层递增
				targetLayerIndex+=1;
			}
		}
	}
	private void ToSub(){
		if(moveDir==-1){
			//获取最底层
			int tempMinLayerIndex=-1;
			if(listAimLayerIndex.Count>=1){
				tempMinLayerIndex=listAimLayerIndex[0];
			}
			if(dicLayerIndexAndMoveDirs.Count>=1){
				foreach(int loopLayerIndex in dicLayerIndexAndMoveDirs.Keys){
					if(tempMinLayerIndex==-1||loopLayerIndex<=tempMinLayerIndex){
						tempMinLayerIndex=loopLayerIndex;
					}
				}
			}
			if(tempMinLayerIndex!=-1&&targetLayerIndex<=tempMinLayerIndex){
				//还存在其他激活楼层
				if(listAimLayerIndex.Count>=1||dicLayerIndexAndMoveDirs.Count>=1){
					moveDir=1;
				}
			}
			//未到底层
			else{
				//逐层递减
				targetLayerIndex-=1;
			}
		}
	}
	//从电梯内点击楼层
	public void InSidePutDown(int theLayerIndex,bool theBool){
		if(theBool){
			if(!listAimLayerIndex.Contains(theLayerIndex)){
				listAimLayerIndex.Add(theLayerIndex);
				SetMoveDir(theLayerIndex);
			}
		}
		else{
			if(listAimLayerIndex.Contains(theLayerIndex)){
				listAimLayerIndex.Remove(theLayerIndex);
			}
		}
	}
	//从电梯外点击楼层
	public void OutSidePutDown(int theLayerIndex,int theMoveDir,bool theBool){
		if(theBool){
			if(!dicLayerIndexAndMoveDirs.ContainsKey(theLayerIndex)){
				dicLayerIndexAndMoveDirs.Add(theLayerIndex,new int[]{0,0});
				SetMoveDir(theLayerIndex);
			}
			if(theMoveDir==1){
				dicLayerIndexAndMoveDirs[theLayerIndex][0]=1;
			}
			else if(theMoveDir==-1){
				dicLayerIndexAndMoveDirs[theLayerIndex][1]=1;
			}
		}
		else{
			if(dicLayerIndexAndMoveDirs.ContainsKey(theLayerIndex)){
				if(theMoveDir==1){
					dicLayerIndexAndMoveDirs[theLayerIndex][0]=0;
				}
				else if(theMoveDir==-1){
					dicLayerIndexAndMoveDirs[theLayerIndex][1]=0;
				}
			}
			if(dicLayerIndexAndMoveDirs[theLayerIndex][0]==0&&dicLayerIndexAndMoveDirs[theLayerIndex][1]==0){
				dicLayerIndexAndMoveDirs.Remove(theLayerIndex);
			}
		}
	}
	private void SetMoveDir(int theLayerIndex){
		//根据第一次点击的楼层，确定移动方向
		int tempLayerIndex = -1;
		if((listAimLayerIndex.Count==1&&dicLayerIndexAndMoveDirs.Count==0)||
		   (listAimLayerIndex.Count==0&&dicLayerIndexAndMoveDirs.Count==1)){
			tempLayerIndex=theLayerIndex;
		}
		if(tempLayerIndex!=-1){
			if(targetLayerIndex<=tempLayerIndex){
				moveDir=1;
			}
			else{
				moveDir=-1;
			}
		}
	}
	private bool CheckAllDoor(){
		//检查所有门是否已关闭
		bool returnIsClose=true;
		if(returnIsClose){
			foreach(ElevatorPort loopElevatorPortC in listElevatorPortC){
				if(!loopElevatorPortC.CheckDoor()){
					returnIsClose=false;
					break;
				}
			}
		}
		if(returnIsClose){
			if(!elevatorRoomC.CheckDoor()){
				returnIsClose=false;
			}
		}
		return returnIsClose;
	}
}
