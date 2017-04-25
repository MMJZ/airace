using System;
using UnityEngine;
using UnityEditorInternal;

public class CarAPI {

  private int accel, turn;
  private GameState state;

  public CarAPI(GameState state) {
    this.state = state;
  }

  internal CarAction getActionAndReset() {
    CarAction r = new CarAction (accel, turn);
    accel = turn = 0;
    return r;
  }

  // methods that the script calls
  public void accelerate() {
    accel = 1;
  }

  public void decelerate() {
    accel = -1;
  }

  public void turnRight() {
    turn = 1;
  }

  public void turnLeft() {
    turn = -1;
  }

  public Vector3 getPosition() {
    return state.position;
  }

  public Vector3 getVelocity() {
    return state.velocity;
  }

  public float getTurnAngle() {
    return state.turnAngle;
  }

  public float getTiltAngle() {
    return state.tiltAngle;
  }

  public float getFacingAngle() {
    return state.facingAngle;
  }

  public Node getNextNode() {
    // change – node isn't protected
    return state.nextNode;
  }

  public float getDistanceToLeftSide() {
    return state.getDistanceToLeftSide ();
  }

  public float getDistanceToRightSide() {
    return state.getDistanceToRightSide ();
  }

  public float getAngleBetweenPoints(float x1, float y1, float x2, float y2) {
    float r = Mathf.Atan2 (y2 - y1, x2 - x1) * 180 / (float)Math.PI;
    Debug.Log (r);
    return r;
  }
}