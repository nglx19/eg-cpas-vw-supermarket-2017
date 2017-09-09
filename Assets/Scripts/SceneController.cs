using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour {

	public float autoLoadNextLevelAfter;

	void Start () {
		if (autoLoadNextLevelAfter == 0) {
			Debug.Log ("Level auto load disabled");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

	public void LoadLevel(string name){
		Debug.Log ("New scene to load: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	
	public void LoadNextLevel() {
        int currInd = SceneManager.GetActiveScene().buildIndex;
        if (currInd < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currInd + 1);
            SceneManager.UnloadSceneAsync(currInd);
        }
        
	}
}
