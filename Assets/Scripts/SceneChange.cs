using System.Collections;
using UnityEngine;

public class SceneChange : MonoBehaviour {
  public void ChangeScene(string toScene) { Application.LoadLevel(toScene); }
}