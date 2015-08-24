using UnityEngine;
using System.Collections;

public class Monster_ControllerState : MonoBehaviour {

	Animator monsterController;				// **** IMPORTANT ****
	public GameObject monsterModel; 		// please attach gameobject with the animator component to this script in the inspector
	public enum MonsterPose{ Stand, Crawl, Crab, HeadStand, Reach};
	
	public MonsterPose monsterPose;
	int minimumPoses = 1;
	int maximumPoses = 6;
	// Use this for initialization
	void Awake () {
		monsterController = monsterModel.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		// *** Austin's test code ***
		// you can delete this if you have another condition for changing poses


	}
	public void GetRandomPose(int num){
		switch(num){
			case 1:
				monsterPose = MonsterPose.Stand;
				break;
			case 2:
				monsterPose = MonsterPose.Crawl;
			
				break;
			case 3:
				monsterPose = MonsterPose.Crab;
			
				break;
			case 4:
				monsterPose = MonsterPose.HeadStand;
			
				break;
			
			case 5:
				monsterPose = MonsterPose.Reach;			
				break;
				
		}
	
	}
	public void SwitchPose(MonsterPose newPose){
		switch(newPose){
		
		case MonsterPose.Stand:
			monsterController.SetTrigger("Stand");
			break;
			
		case MonsterPose.Crawl:				
				monsterController.SetTrigger("Crawl");
			break;
			
		case MonsterPose.Crab:				
				monsterController.SetTrigger("Crab");
			break;
			
		case MonsterPose.HeadStand:				
				monsterController.SetTrigger("HeadStand");
			break;
			
		case MonsterPose.Reach:
				monsterController.SetTrigger("Reach");
			break;
		
		
		}
	
	}
	public void RandomPose(){
		GetRandomPose(Random.Range(minimumPoses, maximumPoses));
		
		SwitchPose(monsterPose);
	}
}
