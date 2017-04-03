using System;

public class Point {

	public double x, z;

	public Point(double x, double z) {
		this.x = x;
		this.z = z;
	}

	public double distanceTo(Point other) {
		return Math.Sqrt(Math.Pow(this.x - other.x, 2.0) + Math.Pow(this.z - other.z, 2.0));
	}
}
