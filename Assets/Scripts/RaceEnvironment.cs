using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class RaceEnvironment : MonoBehaviour {

  private Track[] tracks = new Track[] {
    new Track (new Node (0, 0, 0, 0), new Node (0, 1, 0, 0)),
    new Track (new Node (0, 0, 0, 0), new Node (0, 1, 0, 0))
  };

  private int nocars = 1;
  private Simulator[] simulators;
  private Track track;
  private int timer;

  void Awake() {

    // TODO get this info from main menu script object
    string script = null;
    string nickname = null;
    int trackno = 0;
    int racetype = 0;
    Leaderboard leaderboard = null;

    if(racetype == 0) {
      nocars = 1;
    }

    simulators = new Simulator[nocars];
    for (int x = 0; x < nocars; x++)
      try {
        simulators [x] = new Simulator ();
        simulators [x].loadScript (script);
      } catch (NLua.Exceptions.LuaException e) {
        return; // show error
      }
    track = tracks [trackno];
    // build objects to represent track
    // build car object(s)
  }

  void Start() {
    timer = 0;
    // generate first game state
    GameState state = null;
    foreach (Simulator s in simulators)
      s.start (state);
  }

  void FixedUpdate() {
    timer += 1;
    GameState state = null;
    // generate game state from car
    // update timer display
    bool gameover = false;
    if(gameover) {
      // flash end
      // stop update from being called
      // change scene to end menu
    } else {
      foreach (Simulator s in simulators) {
        CarAction action = s.update (state);
        // apply action to car object
      }
    }
  }
}
