using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Vehicles.Car {
  public class RaceEnvironment : MonoBehaviour {

    public static RaceEnvironment instance;

    public Text timeText;
    private Racer[] racers;
    private int timer = 0;
    private bool isTrial;
    public static int parentScriptID;
    public static RaceStats stats;

    class Racer {
      
      protected Simulator simulator;
      protected GameState state;
      protected GameObject car;
      protected CarController controller;
      protected Track track;
      protected int lastNode = 0, latestVisitedNode = 0;
      protected bool passedStartingGate = false, finishedRace = false;

      public Racer(int position, string script, Track track) {
        car = GameObject.FindGameObjectWithTag ("Car"); // TODO fix this
        RacerData data = GameObject.FindGameObjectWithTag ("CarBody").AddComponent <RacerData> ();
        data.id = position;
        this.track = track;
        state = new GameState (track);
        car.transform.Rotate (0, (float)state.lastNode.theta, 0);
        controller = car.GetComponent<CarController> ();
        if(position == -1) {
          car.transform.position += new Vector3 ((float)state.lastNode.position.x, 0, (float)state.lastNode.position.z);
        } else {
          bool onLeftSide = position % 2 == 0;
          car.transform.position += new Vector3 ((float)state.lastNode.position.x, 0, (float)state.lastNode.position.z);
        }
        try {
          simulator = new Simulator (script);
          simulator.start (state);
        } catch (NLua.Exceptions.LuaException e) {
          Debug.Log (e.ToString ());
        }
      }

      public virtual bool update() {

        //Debug.Log (state.getDistanceToLeftSide () + " " + state.getDistanceToRightSide ());

        try {
          CarAction action = simulator.update (state);
          controller.Move (controller.CurrentSteerAngle / 15 + action.turn * 0.05f, action.accel, 0, 0);
          state.setTurnAngle (controller.CurrentSteerAngle);
          state.setPosition (car.transform.position);
          state.setVelocity (controller.CurrentVelocity);
          state.setFacingAngle (car.transform.rotation.y * 180 / (float)Math.PI);
        } catch (NLua.Exceptions.LuaException e) {
          Debug.Log (e.ToString ());
        }
        return finishedRace;
      }

      public void passedGate(int gateid) {

        if(!passedStartingGate) {
          passedStartingGate = true;
          Debug.Log ("cancelled new node");
          return;
        }
        if(gateid - latestVisitedNode == 1) {
          latestVisitedNode += 1;
          if(latestVisitedNode == 0 && passedStartingGate) {
            finishedRace = true;
          }
        }
        lastNode = gateid;
        state.newSegment (track.nodes [(lastNode + 2) % track.nodes.Length]);
      }
    }

    class TrialRacer : Racer {
      private RaceStats stats;

      public TrialRacer(string script, Track track) : base (-1, script, track) {
        stats = new RaceStats ();
      }

      public int getParentScriptID() {
        return simulator.getParentScriptID ();
      }

      public RaceStats getStats() {
        return stats;
      }

      public override bool update() {
        if(state.velocity.magnitude > stats.maxSpeed)
          stats.maxSpeed = state.velocity.magnitude;
        return base.update ();
      }
    }

    void Start() {

      instance = this;

      timeText.text = "X";

      Track track = MainMenu.tracks [MainMenu.trackNumber];
      string script = MainMenu.script;
      isTrial = MainMenu.isTrial;
      timer = 0;

      if(isTrial) {

        // generate car object
        TrialRacer racer = new TrialRacer (script, track);
        parentScriptID = racer.getParentScriptID ();
        stats = racer.getStats ();
        racers = new Racer[]{ racer };
      } else {
        int noRacers = MainMenu.noRacers;
        string secondscript = MainMenu.secondscript;
        racers = new Racer[noRacers * 2];
        for (int x = 0; x < noRacers * 2; x++) {
          racers [x] = new Racer (x, x % 2 == 0 ? script : secondscript, track);
        }
      }
    }

    public void callback(int carid, int gateid) {
      racers [carid == -1 ? 0 : carid].passedGate (gateid);
    }

    void FixedUpdate() {
      timer += 1;
      timeText.text = timer.ToString ();
      if(isTrial) {
        if(racers [0].update ()) {
          stats.time = timer;
          SceneManager.LoadScene ("End Screen");
        }
      } else {
        foreach (Racer r in racers)
          if(r.update ()) {
            
          }
      }
    }
  }
}