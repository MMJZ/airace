using System;

[System.Serializable]
public class LeaderboardEntry {
	
  public string name;
  public int ID;
  public string script;
  public RaceStats stats;

  public LeaderboardEntry(string name, int ID, string script, RaceStats stats) {
    this.name = name;
    this.ID = ID;
    this.stats = stats;
    this.script = script;
  }
}