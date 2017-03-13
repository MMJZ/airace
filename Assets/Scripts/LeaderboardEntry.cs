using System;

public class LeaderboardEntry {
	
  public readonly string name;
  public readonly int time;

  public LeaderboardEntry(string name, int time) {
    this.name = name;
    this.time = time;
  }
}