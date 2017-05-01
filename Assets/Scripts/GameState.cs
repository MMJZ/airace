using System;
using UnityEngine;

public class GameState {

  public float turnAngle, tiltAngle, maxSafeTurningAngle, facingAngle;
  public Vector3 position, velocity;
  public Node lastNode, nextNode, nodeAfter;
  public bool enteredNewSegment;
  public float distanceToLeftSide, distanceToRightSide;

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
    this.distanceToLeftSide = 0;
    this.distanceToRightSide = 0;
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

  public float getDistanceToLeftSide() {
    Vector2 A = new Vector2 (lastNode.leftSide.x, lastNode.leftSide.z);
    Vector2 B = new Vector2 (nextNode.leftSide.x, nextNode.leftSide.z);
    Vector2 W = A - B;
    Vector2 C = new Vector2 (-W.y, W.x);
    Vector2 P = new Vector2 (position.x, position.z);
    float n = (W.x * P.y - W.y * P.x + W.y * A.x - W.x * A.y) / (W.y * C.x - W.x * C.y);
    return (n * C).magnitude;
  }

  public float getDistanceToRightSide() {
    Vector2 A = new Vector2 (lastNode.rightSide.x, lastNode.rightSide.z);
    Vector2 B = new Vector2 (nextNode.rightSide.x, nextNode.rightSide.z);
    Vector2 W = A - B;
    Vector2 C = new Vector2 (W.y, -W.x);
    Vector2 P = new Vector2 (position.x, position.z);
    float n = (W.x * P.y - W.y * P.x + W.y * A.x - W.x * A.y) / (W.y * C.x - W.x * C.y);
    return (n * C).magnitude;
  }
}
