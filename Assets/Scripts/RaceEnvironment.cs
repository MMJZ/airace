using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.UI;

namespace UnityStandardAssets.Vehicles.Car {
  public class RaceEnvironment : MonoBehaviour {

    //TODO: Maybe turn this into an array, so we can have multiple cars at the same time?
    [SerializeField] private GameObject car;
    private Racer racer;
    private Track track;
    private int timer;

    // Tracks
    private Track[] tracks = new Track[] {
      new Track (new Node (7, 0, 0, 5), new Node (100, 0, 0, 9), new Node (300, 20, 0, 37), new Node (500, 100, 330, 27), new Node (650, 200, 0, 20)),
      new Track (new Node (0, 0, 90, 2), new Node (0, 90, 90, 5)),
      new Track (new Node (0, 0, 45, 14.142), new Node (0, 120, 315, 14.142), new Node (20, 140, 315, 14.142), new Node (140, 140, 225, 14.142), new Node (160, 120, 225, 14.142), new Node (160, 0, 135, 14.142), new Node (140, -20, 135, 14.142), new Node (20, -20, 45, 14.142)),
      new Track (new Node (0, 0, 0, 9), new Node (5, 50, 315, 8), new Node (15, 57, 305, 9), new Node (25, 60, 295, 10), new Node (80, 60, 205, 10), new Node (90, 57, 215, 9), new Node (100, 50, 225, 8), new Node (105, 30, 200, 9), new Node (130, -20, 180, 10), new Node (120, -35, 120, 9), new Node (55, -35, 110, 9), new Node (35, -45, 90, 8), new Node (15, -35, 45, 9)),
      new Track (new Node (0, 0, 0, 9), new Node (0, 500, 0, 9), new Node (5, 510, 315, 10), new Node (15, 515, 270, 11), new Node (25, 510, 225, 9), new Node (30, 500, 180, 9), new Node (30, 300, 180, 9), new Node (40, 290, 225, 8), new Node (50, 280, 270, 9), new Node (60, 270, 225, 8), new Node (70, 260, 180, 9), new Node (70, 180, 180, 9), new Node (60, 170, 135, 8), new Node (50, 160, 90, 9), new Node (40, 150, 135, 8), new Node (30, 140, 180, 9), new Node (30, 0, 180, 9), new Node (25, -10, 135, 10), new Node (15, -15, 90, 11), new Node (5, -10, 45, 10)),
      new Track (new Node (0, 0, 0, 9), new Node (0, 50, 0, 9), new Node (5, 60, 315, 8), new Node (10, 65, 270, 9), new Node (60, 65, 270, 9), new Node (65, 70, 315, 9), new Node (70, 75, 0, 11), new Node (65, 80, 45, 9), new Node (60, 85, 90, 8), new Node (-40, 85, 90, 9), new Node (-45, 90, 45, 9), new Node (-50, 100, 0, 11), new Node (-45, 110, 315, 10), new Node (-35, 120, 270, 9), new Node (150, 120, 270, 9), new Node (155, 115, 225, 9), new Node (165, 110, 180, 9), new Node (165, 40, 180, 9), new Node (160, 35, 135, 9), new Node (140, 30, 90, 9), new Node (60, 30, 90, 9), new Node (50, 25, 135, 8), new Node (40, 15, 180, 9), new Node (40, -75, 180, 9), new Node (35, -80, 135, 10), new Node (20, -90, 90, 9), new Node (-60, -90, 90, 9), new Node (-65, -85, 30, 10), new Node (-70, -80, 0, 9), new Node (-70, 50, 0, 9), new Node (-65, 55, 315, 8), new Node (-60, 60, 270, 10), new Node (-50, 50, 225, 9), new Node (-45, 45, 180, 9), new Node (-45, -40, 180, 9), new Node (-40, -45, 225, 9), new Node (-35, -50, 270, 11), new Node (-30, -45, 315, 9), new Node (-25, -40, 0, 9)),
      new Track (new Node (0, 0, 0, 9), new Node (0, 200, 0, 9), new Node (10, 210, 315, 9), new Node (20, 220, 0, 9), new Node (10, 230, 45, 9), new Node (0, 240, 0, 9), new Node (10, 250, 315, 9), new Node (20, 260, 0, 10), new Node (10, 270, 45, 9), new Node (0, 280, 0, 10), new Node (10, 290, 315, 9), new Node (20, 300, 270, 9), new Node (450, 300, 270, 9), new Node (460, 290, 225, 8), new Node (470, 280, 270, 9), new Node (690, 280, 270, 9), new Node (700, 270, 225, 9), new Node (705, 260, 180, 11), new Node (700, 250, 135, 9), new Node (690, 240, 90, 9), new Node (50, 240, 90, 9), new Node (45, 230, 135, 8), new Node (40, 220, 180, 9), new Node (45, 210, 225, 8), new Node (50, 200, 180, 9), new Node (45, 190, 135, 8), new Node (40, 180, 180, 9), new Node (45, 170, 225, 8), new Node (50, 160, 270, 9), new Node (350, 160, 270, 9), new Node (355, 165, 315, 8), new Node (360, 170, 0, 9), new Node (365, 175, 315, 9), new Node (370, 180, 270, 10), new Node (375, 175, 225, 9), new Node (380, 170, 270, 9), new Node (990, 170, 270, 9), new Node (995, 165, 225, 9), new Node (1000, 160, 180, 10), new Node (1000, 140, 180, 10), new Node (995, 135, 135, 9), new Node (990, 130, 90, 9), new Node (30, 130, 90, 9), new Node (25, 125, 135, 9), new Node (20, 120, 180, 10), new Node (25, 115, 225, 9), new Node (30, 110, 270, 9), new Node (990, 110, 270, 9), new Node (995, 105, 225, 8), new Node (1000, 100, 180, 9), new Node (1000, -150, 180, 9), new Node (995, -155, 135, 9), new Node (990, -160, 90, 9), new Node (500, -160, 90, 9), new Node (495, -165, 135, 9), new Node (490, -170, 180, 10), new Node (495, -175, 225, 9), new Node (500, -180, 180, 10), new Node (495, -185, 135, 9), new Node (490, -190, 90, 9), new Node (60, -190, 90, 9), new Node (55, -185, 45, 9), new Node (50, -180, 0, 10), new Node (45, -175, 45, 9), new Node (40, -170, 90, 10), new Node (35, -165, 45, 9), new Node (30, -160, 0, 10), new Node (25, -155, 45, 9), new Node (20, -150, 90, 10), new Node (15, -145, 45, 9), new Node (10, -140, 0, 10), new Node (5, -135, 45, 9), new Node (0, -130, 0, 9))
    };

    // Racers
    private class Racer {
      
      private Simulator simulator;
      private GameState state;
      private GameObject car;
      private CarController controller;
      private Track track;
      private int currentNode = 1;

      public Racer(GameObject c, string script, GameState t, Track track) {
        car = c;
        this.track = track;
        state = t;
        car.transform.position += new Vector3 ((float)t.lastNode.position.x, 0, (float)t.lastNode.position.z);
        car.transform.Rotate (0, (float)t.lastNode.theta, 0);
        controller = car.GetComponent<CarController> ();
        try {
          simulator = new Simulator (script);
          simulator.start (state);
        } catch (NLua.Exceptions.LuaException e) {
          Debug.Log (e.ToString ());
        }
      }

      public void update() {
        try {
          CarAction action = simulator.update (state);
          controller.Move (controller.CurrentSteerAngle / 15 + action.turn * 0.05f, action.accel, 0, 0);
          state.setTurnAngle (controller.CurrentSteerAngle);
          state.setPosition (car.transform.position);
          state.setVelocity (controller.CurrentVelocity);
          state.setFacingAngle (car.transform.rotation.y * 180 / (float)Math.PI);
          if(Vector3.Distance (track.nodes [currentNode].position, car.transform.position) < track.nodes [currentNode].width)
            state.newSegment (track.nodes [(currentNode++ + 2) % track.nodes.Length]);
        } catch (NLua.Exceptions.LuaException e) {
          Debug.Log (e.ToString ());
        }
      }
    }

    void Awake() {
      // TODO get this info from main menu script object
      string script = @"
        function Update()
          car:accelerate()
          local pos = car:getPosition()
          local nod = car:getNextNode().position
          local angle = car:getAngleBetweenPoints(pos.x, pos.z, nod.x, nod.z)
          if angle > 0 then
            car:turnLeft()
          else
            car:turnRight()
          end
        end";
      string nickname = "";
      int trackNum = 4;
      int racetype = 0;

//      if angle > 0 then
//      if angle - face > 180 then
//        turn = true
//          end
//      else
//        if face - angle > 180 then
//          turn = false
//        else
//          turn = true
//          end
//          end

      // Build objects to represent track
      track = tracks [trackNum];
      for (int i = 0; i < track.nodes.Length - 1; i += 1) {
        Vector3 left = track.nodes [i].leftSide;
        Vector3 right = track.nodes [i].rightSide;
        Vector3 left1 = track.nodes [(i + 1) % (track.nodes.Length)].leftSide;
        Vector3 right1 = track.nodes [(i + 1) % (track.nodes.Length)].rightSide;
        float lheight = left.z - left1.z;
        float lwidth = left.x - left1.x;
        float rheight = right.z - right1.z;
        float rwidth = right.x - right1.x;

        float ldiff = lwidth == 0 ? 0 : lheight / lwidth;
        float rdiff = rwidth == 0 ? 0 : rheight / rwidth;
        float ldiffrev = lheight == 0 ? 0 : lwidth / lheight;
        float rdiffrev = rheight == 0 ? 0 : rwidth / rheight;

        Color red = new Color (1, 0, 0);

        for (float j = left.x; j <= left1.x; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 (j, 0, (left.z + ((j - left.x) * ldiff)));
        }
        for (float j = left1.x; j <= left.x; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 (j, 0, (left1.z + ((j - left1.x) * ldiff)));
        }
        for (float j = left.z; j <= left1.z; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 ((left.x + ((j - left.z) * ldiffrev)), 0, (float)j);
        }
        for (float j = left1.z; j <= left.z; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 ((left1.x + ((j - left1.z) * ldiffrev)), 0, (float)j);
        }
        for (float j = right.x; j <= right1.x; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 (j, 0, (float)(right.z + ((j - right.x) * rdiff)));
        }
        for (float j = right1.x; j <= right.x; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 (j, 0, (float)(right1.z + ((j - right1.x) * rdiff)));
        }
        for (float j = right.z; j <= right1.z; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 ((right.x + ((j - right.z) * rdiffrev)), 0, (float)j);
        }
        for (float j = right1.z; j <= right.z; j += 1) {
          GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
          cube.GetComponent<Renderer> ().material.color = red;
          cube.transform.position = new Vector3 ((right1.x + ((j - right1.z) * rdiffrev)), 0, (float)j);
        }
      }

      int numCars = 1;
      car = GameObject.FindGameObjectWithTag ("Car");
      GameState state = new GameState (track.nodes [0], track.nodes [1], track.nodes [2], timer);
      racer = new Racer (car, script, state, track);
      timer = 0;
    }

    void FixedUpdate() {
      timer += 1;
      // update timer display
      bool gameover = false;
      if(gameover) {
        // TODO: flash end
        // TODO: stop update from being called
        // TODO: change scene to end menu
      } else {          
        racer.update ();
      }
    }
  }
}