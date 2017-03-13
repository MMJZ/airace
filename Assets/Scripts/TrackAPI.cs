
public class TrackAPI {
		
  public Node lastNode, nextNode, nodeAfter;

  internal void Update(GameState state) {
    this.lastNode = state.lastNode;
    this.nextNode = state.nextNode;
    this.nodeAfter = state.nodeAfter;
    //TODO complete
  }

}

