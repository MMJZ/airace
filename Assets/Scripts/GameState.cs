using System;
using UnityEngine;

public class GameState {

  public float turnAngle, tiltAngle, maxSafeTurningAngle, facingAngle;
  public Vector3 position, velocity;
  public Node lastNode, nextNode, nodeAfter;
  public bool deadCar, finishedCar, enteredNewSegment;
  public int timer;

  public GameState(Node lastNode, Node nextNode, Node nodeAfter, int timer) {
    this.turnAngle = 0;
    this.tiltAngle = 0;
    this.maxSafeTurningAngle = 0;
    this.facingAngle = 0;
    this.position = new Vector3 (0, 0, 0);
    this.velocity = new Vector3 (0, 0, 0);
    this.lastNode = lastNode;
    this.nextNode = nextNode;
    this.nodeAfter = nodeAfter;
    this.deadCar = false;
    this.finishedCar = false;
    this.enteredNewSegment = false;
    this.timer = timer;
  }

  public void setTurnAngle(float turnAngle) {
    this.turnAngle = turnAngle;
  }

  public void setTiltAngle(float tiltAngle) {
    this.tiltAngle = tiltAngle;
  }

  public void setFacingAngle(float facingAngle) {
    this.facingAngle = facingAngle;
  }

  public void setMaxSafeTurningAngle(float maxSafeTurningAngle) {
    this.maxSafeTurningAngle = maxSafeTurningAngle;
  }

  public void setPosition(Vector3 pos) {
    position.Set (pos.x, pos.y, pos.z);
  }

  public void setVelocity(Vector3 vel) {
    velocity.Set (vel.x, vel.y, vel.z);
  }

  public void newSegment(Node newNode) {
    lastNode = nextNode;
    nextNode = nodeAfter;
    nodeAfter = newNode;
    enteredNewSegment = true;
    Debug.Log ("new node");
  }

  public void notifyDead() {
    deadCar = true;
  }

  public void notifyFinished() {
    finishedCar = true;
  }

  public float getDistanceToLeftSide() {
    return Vector3.Distance (position, nextNode.leftSide);
  }

  public float getDistanceToRightSide() {
    return Vector3.Distance (position, nextNode.rightSide);
  }
}
