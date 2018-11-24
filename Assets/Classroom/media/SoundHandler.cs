using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : GameInterfaceEventListener
{
  public override void pieceCouldNotBeMoved(int weight)
  {
    // Play denied sound
  }

  public override void piecePickedUp(int weight)
  {
    // Play happy picked up sound
  }

  public override void pieceWasDroppedOutsideOfDropZone()
  {
    // Play piece was lost sound
  }

  public override void pieceWasPlacedAtTower(int tower)
  {
    // Play piece was placed at tower
  }
}
