using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
  /* This is abstract to avoid custom serializing of interfaces -
   and still be able to use it in the Unity Editor.
   It is however an interface in all but type */
  public abstract class GameInterfaceEventListener : MonoBehaviour
  {
    public abstract void piecePickedUp(int weight);
    public abstract void pieceCouldNotBeMoved(int weight);

    public abstract void pieceWasPlacedAtTower(int tower);
    public abstract void pieceWasDroppedOutsideOfDropZone();

    public abstract void pieceHoveredDropZone(int tower);
    public abstract void pieceLeftDropZone(int tower);

    public abstract void pieceCouldNotBePlaced(int currentTowerIndex);
  }
}