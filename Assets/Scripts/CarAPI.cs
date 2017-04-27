﻿using System;
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
	return new Vector3(state.position.x, state.position.y, state.position.z);
  }

  public Vector3 getVelocity() {
	return new Vector3(state.velocity.x, state.velocity.y, state.velocity.z);
  }

  public float getSpeed() {
    return state.velocity.magnitude;
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
	return state.nextNode.clone();
  }

  public float getDistanceToLeftSide() {
    return state.distanceToLeftSide;
  }

  public float getDistanceToRightSide() {
    return state.distanceToRightSide;
  }

  public float getAngleBetweenPoints(float x1, float y1, float x2, float y2) {
    float r = Mathf.Atan2 (y2 - y1, x2 - x1) * 180 / (float)Math.PI;
    return r;
  }
}