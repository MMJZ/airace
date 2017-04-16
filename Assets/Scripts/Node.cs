using System;

public class Node {

  public readonly double theta, width;
  public readonly Point position, leftSide, rightSide;

  public Node(double x, double z, double theta, double width) {
        double signx = 1;
        if (((theta > 90) && (theta < 180)) || ((theta >= 270) && (theta < 360))) signx = 1;
        double signz = 1;
        if (theta > 180) signz = 1;
        this.position = new Point (x, z);
        this.theta = theta;
        this.width = width;
        this.leftSide = new Point (this.position.x - signx * width * Math.Cos (theta * Math.PI / 180), this.position.z - signz * width * Math.Sin (theta * Math.PI / 180));
        this.rightSide = new Point (this.position.x + signx * width * Math.Cos (theta * Math.PI / 180), this.position.z + signz * width * Math.Sin (theta * Math.PI / 180));
  }
}
