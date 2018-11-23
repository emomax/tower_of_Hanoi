using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameInterfaceEventListener : MonoBehaviour {
  public abstract void piecePickedUp(int weight);
  public abstract void pieceCouldNotBeMoved(int weight);
}
