using System;
using System.Collections.Generic;
using UnityEngineInternal;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Security.Cryptography;
using System.IO.Pipes;

public class Leaderboard {

  private TreeNode forestFloor;
  // forestFloor has ID of 0
  private int IDFactory;

  // of size max 10
  private LeaderboardEntry[] leaderboard;

  public Leaderboard() {
    string marketplacejson = null;
    string leaderboardjson = null;
    forestFloor = JsonUtility.FromJson<TreeNode> (marketplacejson);
    leaderboard = JsonUtility.FromJson<LeaderboardHolder> (leaderboardjson).entries;
    IDFactory = forestFloor.getLargestID ();
  }

  public void storeEntry(string script, int parentID, string nickname, RaceStats stats) {
    IDFactory += 1;
    forestFloor.addChild (script, parentID, nickname, stats, IDFactory);
    int p = 9;
    while (leaderboard [p] == null)
      p--;
    if(stats.time < leaderboard [p].stats.time) {
      LeaderboardEntry newentry = new LeaderboardEntry (nickname, IDFactory, script, stats);
      leaderboard [p] = newentry;
      p--;
      while (p >= 0 && newentry.stats.time < leaderboard [p].stats.time) {
        LeaderboardEntry temp = leaderboard [p];
        leaderboard [p] = leaderboard [p + 1];
        leaderboard [p + 1] = temp;
        p--;
      }
    }
  }

  public LeaderboardEntry[] getTopTen() {
    return leaderboard;
  }

  public void saveToFile() {
    string marketplacejson = JsonUtility.ToJson (forestFloor);
    LeaderboardHolder h = new LeaderboardHolder ();
    h.entries = leaderboard;
    string leaderboardjson = JsonUtility.ToJson (h);
  }
}

[System.Serializable]
class TreeNode {

  private TextObject text;
  private string script;
  private RaceStats stats;
  private int ID;
  private TreeNode[] children;

  public TreeNode() {
    text = new TextObject ();
    children = new TreeNode[0];
  }

  public TreeNode(string script, string nickname, RaceStats stats, int newID) {
    this.script = script;
    text = new TextObject ();
    text.name = nickname;
    text.desc = newID;
    this.ID = newID;
    this.stats = stats;
    children = new TreeNode[0];
  }

  public bool addChild(string script, int parentID, string nickname, int time, int newID) {
    if(ID == parentID) {
      TreeNode[] newarr = new TreeNode[children.Length + 1];
      for (int x = 0; x < children.Length; x++)
        newarr [x] = children [x];
      newarr [children.Length] = new TreeNode (script, nickname, time, newID);
      children = newarr;
      return true;
    } else
      foreach (TreeNode c in children)
        if(c.addChild (script, parentID, nickname, time, newID))
          return true;
    return false;
  }

  public int getLargestID() {
    if(children.Length == 0)
      return ID;
    int r = 0;
    foreach (TreeNode c in children)
      r = Math.Max (c.getLargestID (), r);
    return r;
  }
}

[System.Serializable]
class TextObject {
  public string name;
  // represents ID
  public int desc;
}

[System.Serializable]
class LeaderboardHolder {
  public LeaderboardEntry[] entries;
}