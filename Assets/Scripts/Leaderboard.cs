using System;
using System.Collections.Generic;
using UnityEngineInternal;

public class Leaderboard {

  private TreeNode forestFloor;
  // forestFloor has ID of 0
  private int IDFactory;

  public Leaderboard() {
    IDFactory = 1;
    // TODO build data structure from file
    forestFloor = new TreeNode ();
  }

  public void storeEntry(string script, int parentID, string nickname, int time) {
    IDFactory += 1;
    forestFloor.addChild (script, parentID, nickname, time, IDFactory);
  }

  public LeaderboardEntry[] getTopTen() {
    //TODO organise tree such that top ten can be generated for comparing results 
    return null;
  }

  public void saveToFile() {
    //TODO save data structure to file
  }
}

class TreeNode {

  private string script, nickname;
  private int time, ID;
  private List<TreeNode> children;

  public TreeNode() {
    time = ID = 0;
    script = nickname = null;
  }

  public TreeNode(string script, string nickname, int time, int newID) {
    this.script = script;
    this.nickname = nickname;
    this.time = time;
    this.ID = newID;
  }

  public bool addChild(string script, int parentID, string nickname, int time, int newID) {
    if(ID == parentID) {
      children.Add (new TreeNode (script, nickname, time, newID));
      return true;
    } else
      foreach (TreeNode c in children)
        if(c.addChild (script, parentID, nickname, time, newID))
          return true;
    return false;
  }
}