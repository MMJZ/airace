using System;
using UnityEngine;

public class Node {

  public readonly float theta, width;
  public readonly Vector3 position, leftSide, rightSide;

  public Node(double x, double z, double theta, double width) {
    while (theta < 0)
      theta += 360;
    this.theta = (float)theta;
    this.width = (float)width;
    this.position = new Vector3 ((float)x, 0, (float)z);
    float rad = (float)(Math.PI * theta / 180);
    this.leftSide = new Vector3 ((float)(x - width * Math.Cos (rad)), 0, (float)(z - width * Math.Sin (rad)));
    this.rightSide = new Vector3 ((float)(x + width * Math.Cos (rad)), 0, (float)(z + width * Math.Sin (rad)));
  }

  public Node clone() {
    return new Node (position.x, position.z, theta, width);
  }
}
