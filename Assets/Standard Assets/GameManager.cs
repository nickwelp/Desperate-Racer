using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour{
		private bool paused;
		public StateType state;

		public static GameManager instance; 
		public static GameManager Instance {
			get{
				if(instance==null) {
					instance = new GameManager();
				}
			return instance;	
			}
		}
		
		// Add your game mananger members here
		public void Pause(bool paused) {
			if(paused){
				paused = true;
				Time.timeScale = 0f;
				paused = true;
			}else{
				Time.timeScale = 4.0f;
			}			
		}
		void Start(){
			state = StateType.MENU; 	
		}


	}

public enum StateType
{
	DEFAULT,      //Fall-back state, should never happen
	RACING,      //RACE IS UNDERWAY
	PRACTICE,    //Free drive mode
	TRAINING,      //WORKING WITH NEURAL NET
	COUNTDOWN,		//3 2 1 before race starts
	PAUSED, 
	CHECKMAP,       //Analyzing map for drive ability, check if need to reload
	LOADMAP,     //Loading Map from save state
	SAVEMAP,     //Save Map to save space
	MENU,
	OPTIONS,
	LOBBY
};
//