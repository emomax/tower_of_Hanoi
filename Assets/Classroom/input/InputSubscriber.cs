using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputSubscriber
{
  void touchedPiece(int weight);
  void releaseCurrentInput();

  void enteredDropZoneForTower(int towerIndex);
  void leftDropZone();
}
