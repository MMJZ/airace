using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace UnityStandardAssets.Vehicles.Car {
  public class RaceEnvironment : MonoBehaviour {

    [SerializeField] private GameObject car;


    private class Racer{
      private Simulator simulator;
      private GameState state;
      private GameObject car;
      private CarController controller;

      public Racer(GameObject c, string script, GameState t){
        car = c;
        controller = car.GetComponent<CarController> ();
        state = t;
        try {
          simulator = new Simulator();
          simulator.loadScript (script);
          simulator.start(t);
        } catch (NLua.Exceptions.LuaException e) {
          return; // show error
        }
      }
      public void update(){
        try {
          CarAction action = simulator.update (state);
          controller.Move (action.turn, action.accel, action.accel, 0);
          state.setTurnAngle (controller.CurrentSteerAngle);
          //TODO: Should change these to Vector3 in GameState
          state.setPositionX (car.transform.position.x);
          state.setPositionZ (car.transform.position.z);
          state.setVelocityX (controller.CurrentVelocity.x);
          state.setVelocityZ (controller.CurrentVelocity.z);
        } catch (NLua.Exceptions.LuaException e) {
          return; // show error
        }
      }

    }

    //TODO: Maybe turn this into an array, so we can have multiple cars at the same time?
    private Racer racer;

    //TODO: Make some tracks
    private Track[] tracks = new Track[] {
      new Track (new Node (0, 0, 0, 0), new Node (0, 100, 0, 0), new Node (0, 200, 0, 0), new Node (0, 300, 0, 0), new Node (0, 400, 0, 0), new Node (0, 500, 0, 0)),
      new Track (new Node (0, 0, 0, 0), new Node (0, 1, 0, 0))
    };

    private int numCars = 1;
    private Track track;
    private int timer;

    void Awake() {
      // TODO get this info from main menu script object
      string script = "";
      string nickname = "";
      int trackNum = 0;
      int racetype = 0;
      timer = 0;
      //TODO: Leaderboard is bugged currently, uncomment when fixed
      //Leaderboard leaderboard = new Leaderboard();

      //TODO: figure out what to do here
      if(racetype == 0) {
        numCars = 1;
      }

      car = GameObject.FindGameObjectWithTag ("Car");
      track = tracks [trackNum];
      // TODO: build objects to represent track
     


      GameState state = new GameState(track.nodes[track.nodes.Length-1], track.nodes[0], track.nodes[1],timer);
      racer = new Racer (car, script, state);

    }

    void FixedUpdate() {
      timer += 1;
      // update timer display
      bool gameover = false;
      if(gameover) {
        // TODO: flash end
        // TODO: stop update from being called
        // TODO: change scene to end menu
      } 
      else {          
        racer.update ();
      }
    }
  }
}