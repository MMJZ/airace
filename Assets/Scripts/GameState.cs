using System;


public class GameState {

	public double turnAngle, tiltAngle, maxSafeTurningAngle, px, pz, vx, vz;
	public Node lastNode, nextNode, nodeAfter;
	public bool deadCar, finishedCar, enteredNewSegment;
	public int timer;

	public GameState(Node lastNode, Node nextNode, Node nodeAfter, int timer) {
		this.turnAngle = 0.0;
		this.tiltAngle = 0.0;
		this.maxSafeTurningAngle = 0.0;
		this.px = 0.0;
		this.pz = 0.0;
		this.vx = 0.0;
		this.vz = 0.0;
		this.lastNode = lastNode;
		this.nextNode = nextNode;
		this.nodeAfter = nodeAfter;
		this.deadCar = false;
		this.finishedCar = false;
		this.enteredNewSegment = false;
		this.timer = timer;
	}

	public void setTurnAngle(double turnAngle) {
		this.turnAngle = turnAngle;
	}

	public void setTiltAngle(double tiltAngle) {
		this.tiltAngle = tiltAngle;
	}

	public void setMaxSafeTurningAngle(double maxSafeTurningAngle) {
		this.maxSafeTurningAngle = maxSafeTurningAngle;
	}

	public void setPosition(Point position) {
		px = position.x;
		pz = position.z;
	}

	public void setPositionX(double px) {
		this.px = px;
	}

	public void setPositionZ(double pz) {
		this.pz = pz;
	}

	public void setVelocity(Point velocity) {
		vx = velocity.x;
		vz = velocity.z;
	}

	public void setVelocityX(double vx) {
		this.vx = vx;
	}

	public void setVelocityZ(double vz) {
		this.vz = vz;
	}

	public void notifyDead() {
		deadCar = true;
	}

	public void notifyFinished() {
		finishedCar = true;
	}

	public void notifyEnteredNewSegment() {
		enteredNewSegment = true;
	}

	public double getDistanceToLeftSide() {
		return lastNode.position.distanceTo(lastNode.leftSide);
	}

	public double getDistanceToRightSide() {
		return lastNode.position.distanceTo(lastNode.rightSide);
	}
}
