using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//basically what script does is define a function to be used to load another scene
//found some 1-hour tutorial on the internet about building menus in Unity
//used same variable/ class names for ease of understanding
public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex) {
			
		SceneManager.LoadScene (sceneIndex);
	}
}
