using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour{
		private bool paused;
		
		public static GameManager instance; 
		public static GameManager Instance {
			get{
				if(instance==null) {
					instance = new GameManager();
				}
			return instance;	
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
            GAMESTART,
            CHECKMAP,       //Analyzing map for drive ability, check if need to reload
            LOADMAP,     //Loading Map from save state
            SAVEMAP,     //Save Map to save space
            MENU,
            OPTIONS,
            LOBBY
        };
        public StateType state;

        void Awake()
        {
            
            DontDestroyOnLoad(transform.gameObject);
        }
		// Add your game mananger members here
		public void Pause(bool paused) {
            
            if(paused){
                ChangeState("PAUSED");
				Time.timeScale = 0f;
			}else{
                ChangeState("RACING");
				Time.timeScale = 1.0f;
			}			
		}
		void Start(){
			//state = StateType.MENU; 	
            if (state == StateType.GAMESTART) {
             //   state = StateType.MENU;
            //    Application.LoadLevel("_main menu");
            }
		}
        void OnGUI()
        {
            switch (state)
            {
                case StateType.PAUSED:
                    if (GUI.Button(new Rect(20, 40, 80, 20), "Resume"))
                    {
                        ChangeState("RACING");
                        Pause(false);
                    }
                    break;
                case StateType.RACING:
                    if (GUI.Button(new Rect(20, 40, 80, 20), "Pause"))
                    {
                        ChangeState("PAUSED");
                        Pause(true);
                    }
                    break;
                case StateType.CHECKMAP:
                    GUI.Box(new Rect(10, 10, 300, 290), "Our drones are looking for new race tracks as we speak. Light on cops, is key. The reality warping effects of the Cthulu settled in New York City also effect the intergrity of reality in the storm drains we race, even down here, in Texas.");         
                    break;
                default:
                    break;
            }
        }
        public void ChangeState(string stateString) {
            switch (stateString)
                {
                    case "DEFAULT": state = StateType.DEFAULT; break;
                    case "RACING": state = StateType.RACING; break;
                    case "PRACTICE": state = StateType.PRACTICE; break;
                    case "TRAINING": state = StateType.TRAINING; break;
                    case "COUNTDOWN": state = StateType.COUNTDOWN; break;
                    case "PAUSED": state = StateType.PAUSED; break;
                    case "GAMESTART": state = StateType.GAMESTART; break;
                    case "CHECKMAP": state = StateType.CHECKMAP; break;
                    case "LOADMAP": state = StateType.LOADMAP; break;
                    case "SAVEMAP": state = StateType.SAVEMAP; break;
                    case "MENU": state = StateType.MENU; break;
                    case "OPTIONS": state = StateType.OPTIONS; break;
                    case "LOBBY": state = StateType.LOBBY; break; 
                    default:
                        state = StateType.DEFAULT;break;
                }
            
        
        }
	}
//