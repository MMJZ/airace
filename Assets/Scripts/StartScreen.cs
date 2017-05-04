using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class StartScreen : MonoBehaviour {

  private void StartRace(SocketIOEvent e){
    Debug.Log ("gotem");
    Debug.Log (e.data);

  }
  private SocketIOComponent socket;

  public void Start() 
  {
    GameObject go = GameObject.Find("SocketIO");
    socket = go.GetComponent<SocketIOComponent>();

    socket.Emit ("ResultToServer");

    socket.On("aaa", TestBoop);


	}
	
  public void TestOpen(SocketIOEvent e)
  {
    Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
  }
  public void TestBoop(SocketIOEvent e)
  {
    Debug.Log("[SocketIO] Script received: " + e.name + " " + e.data);

    if (e.data == null) { return; }

    Debug.Log(
      "#####################################################" +
      "THIS: " + e.data.GetField("this").str +
      "#####################################################"
    );
  }
	// Update is called once per frame
	void Update () {
    
		
	}
}
