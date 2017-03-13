using System;


public class GameState {

  public double turnAngle, tiltAngle, maxSafeTurningAngle, px, pz, vx, vz;
  public Node lastNode, nextNode, nodeAfter;
  public bool deadCar, finishedCar, enteredNewSegment;
  public int timer;

  public GameState() {
    // TODO finish constructor, add other info
  }
}
