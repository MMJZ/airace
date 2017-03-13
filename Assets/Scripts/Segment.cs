using System;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Segment {

  public readonly Node nodeFrom, nodeTo;
  public readonly Point bottomLeft, bottomRight, topLeft, topRight;

  public Segment(Node de, Node to) {
    this.nodeFrom = de;
    this.nodeTo = to;
    bottomLeft = de.leftSide;
    bottomRight = de.rightSide;
    topLeft = to.leftSide;
    topRight = to.rightSide;
  }
}