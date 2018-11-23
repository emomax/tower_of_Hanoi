using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputSubscriber
{
  void touchedPiece(int weight);
  void enteredDropZoneForTower(int towerIndex);
  void releaseCurrentInput();
}
