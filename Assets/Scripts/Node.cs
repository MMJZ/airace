using System;

public class Node {

  public readonly double theta, width;
  public readonly Point position, leftSide, rightSide;

  public Node(double x, double z, double theta, double width) {
    this.position = new Point (x, z);
    this.theta = theta;
    this.width = width;
    this.leftSide = new Point (width * Math.Cos (theta - Math.PI / 2), width * Math.Sin (theta - Math.PI / 2));
    this.rightSide = new Point (width * Math.Cos (theta + Math.PI / 2), width * Math.Sin (theta + Math.PI / 2));
  }
}
