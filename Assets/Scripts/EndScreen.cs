using System;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {
  public Text state, time, position;
  
  public EndScreen(GameState current) {
	time.text = current.timer.ToString();
	State(current);
	position.text = "";
	  //TODO link leaderboard position
  }
  
  private void State(GameState current) {
	if (current.deadCar) {state.text = "Car is dead!";};
	if (current.finishedCar) {state.text = " Car completed the track safely!";};
  }
}