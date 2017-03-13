public class Track {

  public readonly Node[] nodes;

  public Track(params Node[] nodes) {
    this.nodes = nodes;
    // TODO add starting and finishing area
  }

  public Node getNode(int n) {
    return nodes [n];
  }
}