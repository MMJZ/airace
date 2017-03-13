
public class CarAPI {

  // possible actions
  private int accel, turn;

  // accessible information – move to get methods?
  public Point position, velocity;
  public double turnAngle, tiltAngle, distanceToLeftSide, distanceToRightSide;

  internal void Update(GameState gameState) {
    position.x = gameState.px;// save on needing to make new Point object
    // TODO complete
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
}