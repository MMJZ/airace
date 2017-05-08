using System;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.UI;

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

  public float getPositionX() {
    return state.position.x;
  }

  public float getPositionZ() {
    return state.position.z;
  }

  public float getVelocityX() {
    return state.velocity.x * 3.5f;
  }

  public float getVelocityZ() {
    return state.velocity.z * 3.5f;
  }

  public float getSpeed() {
    return state.getSpeed ();
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

  public Node lastNode() {
    return state.lastNode.clone ();
  }

  public Node nextNode() {
    return state.nextNode.clone ();
  }

  public Node nodeAfter() {
    return state.nodeAfter.clone ();
  }

  public float getDistanceToLeftSide() {
    return state.getDistanceToLeftSide ();
  }

  public float getDistanceToRightSide() {
    return state.getDistanceToRightSide ();
  }

  public float getAngleBetweenPoints(float x1, float z1, float x2, float z2) {
    float r = 90 - Mathf.Atan2 (z2 - z1, x2 - x1) * 180 / (float)Math.PI;
    return r < 0 ? r + 360 : r;
  }

  public float getAngleToNextNode() {
    return getAngleBetweenPoints (state.position.x, state.position.z, state.nextNode.position.x, state.nextNode.position.z);
  }

  public float getDistanceToNextNode() {
    return Vector3.Distance (state.position, state.nextNode.position);
  }
}
