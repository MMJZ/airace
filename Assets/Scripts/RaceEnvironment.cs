using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SocketIO;

namespace UnityStandardAssets.Vehicles.Car {
  public class RaceEnvironment : MonoBehaviour {

    public static RaceEnvironment instance;
    public Text timeText, speedText;
    private Racer[] racers;
    private int timer = 0;
    private bool isTrial;
    public static int parentScriptID;
    public static RaceStats stats;

    class Racer {
      
      protected Simulator simulator;
      protected GameState state;
      protected GameObject car;
      protected GameObject carFront;
      protected CarController controller;
      protected Track track;
      protected int lastNode = 0, latestVisitedNode = 0;
      protected bool passedStartingGate = false, finishedRace = false;

      public Racer(int position, string script, Track track) {
        car = GameObject.FindGameObjectWithTag ("Car"); // TODO fix this
        carFront = GameObject.FindGameObjectWithTag ("carFront");
        RacerData data = GameObject.FindGameObjectWithTag ("CarBody").AddComponent <RacerData> ();
        data.id = position;
        this.track = track;
        state = new GameState (track);
        car.transform.Rotate (0, (float)state.lastNode.theta, 0);
        controller = car.GetComponent<CarController> ();
        if(position == -1) {
          car.transform.position += new Vector3 (state.lastNode.position.x, 0, state.lastNode.position.z);
        } else {
          bool onLeftSide = position % 2 == 0;
          car.transform.position += new Vector3 (state.lastNode.position.x, 0, state.lastNode.position.z);
        }
        try {
          simulator = new Simulator (script);
          simulator.start (state);
        } catch (NLua.Exceptions.LuaException e) {
          Debug.Log ("LUA ERROR");
          Debug.Log (e.ToString ());
        }
      }

      public virtual bool update() {

        try {
          CarAction action = simulator.update (state);
          controller.Move (controller.CurrentSteerAngle / 20 + action.turn * 0.05f, action.accel, 0, 0);
          state.setTurnAngle (controller.CurrentSteerAngle);
          state.setPosition (car.transform.position);
          state.setPosition (carFront.transform.position);
          state.setVelocity (controller.CurrentVelocity);
          state.setFacingAngle (car.transform.eulerAngles.y);
        } catch (NLua.Exceptions.LuaException e) {
          Debug.Log ("LUA ERROR");
          Debug.Log (e.ToString ());
        }
        return finishedRace;
      }

      public void passedGate(int gateid) {

        if(!passedStartingGate) {
          passedStartingGate = true;
          return;
        }
        if(gateid - latestVisitedNode == 1) {
          latestVisitedNode += 1;
          if(latestVisitedNode == 0 && passedStartingGate) {
            finishedRace = true;
          }
        }
        lastNode = gateid;
        state.newSegment (track, lastNode);
      }
    }

    class TrialRacer : Racer {
      private RaceStats stats;

      public TrialRacer(string script, Track track, RaceStats stats) : base (-1, script, track) {
        this.stats = stats;
      }

      public float getSpeed() {
        return state.getSpeed ();
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
      speedText.text = "X";

      Track track = StartScreen.tracks [StartScreen.trackNumber];
      string script = StartScreen.script;
      isTrial = StartScreen.isTrial;
      timer = 0;

      if(isTrial) {

        // generate car object
        stats = new RaceStats ();
        TrialRacer racer = new TrialRacer (script, track, stats);
        racers = new Racer[]{ racer };
      } else {
        int noRacers = StartScreen.noRacers;
        string secondscript = StartScreen.secondscript;
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
      speedText.text = string.Format ("{0:N2}", ((TrialRacer)racers [0]).getSpeed ()) + "kmh";
      if(isTrial) {
        if(racers [0].update ()) {
          JSONObject result = new JSONObject ();
          result.AddField ("time", timer);
          result.AddField ("maxSpeed", stats.maxSpeed);
          StartScreen.socket.Emit ("finishedRun", result);
          SceneManager.LoadScene ("Start");
        }
      } else {
        for (int x = 0; x < racers.Length; x++) {
          if(racers [x].update ()) {
            bool leftSide = x % 2 == 0;
          }
        }
      }
    }
  }
}