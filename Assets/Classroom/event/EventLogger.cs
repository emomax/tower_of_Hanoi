using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLogger : GameInterfaceEventListener
{
  public override void pieceCouldNotBeMoved(int weight)
  {
    Debug.Log("Piece '" + weight + "' could not be moved!");
  }

  public override void pieceCouldNotBePlaced(int currentTowerIndex)
  {
    Debug.Log("Failed to place held piece at tower '" + currentTowerIndex + "'!");
  }

  public override void pieceHoveredDropZone(int tower)
  {
    Debug.Log("Currenty held piece entered drop zone '" + tower + "'!");
  }

  public override void pieceLeftDropZone(int tower)
  {
    Debug.Log("Currenty held piece left drop zone '" + tower + "'!");
  }

  public override void piecePickedUp(int weight)
  {
    Debug.Log("Piece '" + weight + "' was picked up!");
  }

  public override void pieceWasDroppedOutsideOfDropZone()
  {
    Debug.Log("Currently held piece was dropped!");
  }

  public override void pieceWasPlacedAtTower(int tower)
  {
    Debug.Log("Currently held piece was put down at tower number '" + tower + "'");
  }
}
