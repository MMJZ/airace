using System;
using UnityEngine;

public class Node {

  public readonly float theta, width;
  public readonly Vector3 position, leftSide, rightSide;

  //  public Node(double x, double z, double theta, double width) {
  //    double signx = 1;
  //    if(((theta > 90) && (theta < 180)) || ((theta >= 270) && (theta < 360)))
  //      signx = 1;
  //    double signz = 1;
  //    if(theta > 180)
  //      signz = 1;
  //
  //    this.leftSide = new Point (this.position.x - signx * width * Math.Cos (theta * Math.PI / 180), this.position.z - signz * width * Math.Sin (theta * Math.PI / 180));
  //    this.rightSide = new Point (this.position.x + signx * width * Math.Cos (theta * Math.PI / 180), this.position.z + signz * width * Math.Sin (theta * Math.PI / 180));
  //  }

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

  //  public Node(Vector3 position, float theta, float width, Vector3 leftSide, Vector3 rightSide) {
  //    this.position = new Vector3 (position.x, position.y, position.z);
  //    this.theta = theta;
  //    this.width = width;
  //    this.leftSide = new Vector3 (leftSide.x, leftSide.y, leftSide.z);
  //    this.rightSide = new Vector3 (rightSide.x, rightSide.y, rightSide.z);
  //  }
  //
  //  public Node clone() {
  //    return new Node (position, theta, width, leftSide, rightSide);
  //  }
}
