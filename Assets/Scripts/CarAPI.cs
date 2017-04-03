using System;

public class CarAPI {

	// possible actions
	private int accel, turn;

	// accessible information
	private Point position, velocity;
	private double turnAngle, tiltAngle, distanceToLeftSide, distanceToRightSide;

	internal void Update(GameState gameState) {
		position.x = gameState.px; // save on needing to make new Point object
		position.z = gameState.pz;
		velocity.x = gameState.px;
		velocity.z = gameState.pz;
		turnAngle = Math.Min(gameState.turnAngle, gameState.maxSafeTurningAngle);
		tiltAngle = gameState.tiltAngle;
		distanceToLeftSide = gameState.getDistanceToLeftSide ();
		distanceToRightSide = gameState.getDistanceToRightSide ();
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
		
	public Point getPosition() {
		return position;
	}

	public Point getVelocity() {
		return velocity;
	}

	public double getTurnAngle() {
		return turnAngle;
	}

	public double getTiltAngle() {
		return tiltAngle;
	}

	public double getDistanceToLeftSide() {
		return distanceToLeftSide;
	}

	public double getDistanceToRightSide() {
		return distanceToRightSide;
	}
}