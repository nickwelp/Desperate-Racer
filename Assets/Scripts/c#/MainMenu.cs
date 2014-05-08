using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public string Level1;
	public string Level2;
    public GameManager gamemanager;

 //   GameManager gamemanager = _GameManager.Instance.GetComponent<GameManager>();
    

//	public class GUITest : MonoBehaviour {
		
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), Level1)) {
            gamemanager.ChangeState("CHECKMAP");
            Application.LoadLevel(Level1);
            
		}

		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), Level2)) {
            gamemanager.ChangeState("CHECKMAP");
            Application.LoadLevel(Level2);
		}
	}
    void Awake() {
        gamemanager.ChangeState("Menu");
    }
}