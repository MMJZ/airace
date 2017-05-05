using System;
using UnityEngine;
using System.Xml;

public class GameState {

  public float turnAngle, tiltAngle, maxSafeTurningAngle, facingAngle;
  public Vector3 position, velocity;
  public Node lastNode, nextNode, nodeAfter;
  public bool enteredNewSegment;

  public GameState(Track track) {
    this.turnAngle = 0;
    this.tiltAngle = 0;
    this.maxSafeTurningAngle = 0;
    this.facingAngle = 0;
    this.position = new Vector3 (0, 0, 0);
    this.velocity = new Vector3 (0, 0, 0);
    this.lastNode = track.nodes [0];
    this.nextNode = track.nodes [1];
    this.nodeAfter = track.nodes [2];
    this.enteredNewSegment = false;
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

  public void newSegment(Track track, int newLast) {
    lastNode = track.nodes [newLast];
    nextNode = track.nodes [(newLast + 1) % track.nodes.Length];
    nodeAfter = track.nodes [(newLast + 2) % track.nodes.Length];
    enteredNewSegment = true;
  }

  public float getDistanceToLeftSide() {
    Vector2 A = new Vector2 (lastNode.leftSide.x, lastNode.leftSide.z);
    Vector2 B = new Vector2 (nextNode.leftSide.x, nextNode.leftSide.z);
    Vector2 W = A - B;
    Vector2 C = new Vector2 (-W.y, W.x);
    Vector2 P = new Vector2 (position.x, position.z);
    float n = (W.x * P.y - W.y * P.x + W.y * A.x - W.x * A.y) / (W.y * C.x - W.x * C.y);
    float r = (n * C).magnitude;
    return float.IsNaN (r) ? Vector3.Distance (position, lastNode.leftSide) : r;
  }

  public float getDistanceToRightSide() {
    Vector2 A = new Vector2 (lastNode.rightSide.x, lastNode.rightSide.z);
    Vector2 B = new Vector2 (nextNode.rightSide.x, nextNode.rightSide.z);
    Vector2 W = A - B;
    Vector2 C = new Vector2 (W.y, -W.x);
    Vector2 P = new Vector2 (position.x, position.z);
    float n = (W.x * P.y - W.y * P.x + W.y * A.x - W.x * A.y) / (W.y * C.x - W.x * C.y);
    float r = (n * C).magnitude;
    return float.IsNaN (r) ? Vector3.Distance (position, lastNode.rightSide) : r;
  }

  public float getSpeed() {
    return velocity.magnitude * 3.5f;
  }

  public float getAngleBetweenPoints(float x1, float y1, float x2, float y2) {
    float r = 90 - Mathf.Atan2 (y2 - y1, x2 - x1) * 180 / (float)Math.PI;
    return r < 0 ? r + 360 : r;
  }
}
