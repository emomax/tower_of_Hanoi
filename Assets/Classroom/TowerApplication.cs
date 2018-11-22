using System;
using System.Collections;
using System.Collections.Generic;
using domain;
using UnityEngine;

public class TowerApplication : MonoBehaviour {

  List<Tower> towers;

  TowerPiece currentlyHeldPiece = null;

  public TowerApplication() : this(4) { }

  public TowerApplication(int numberOfTowerPieces) {
    towers = new List<Tower>();

    for (int i = 0; i < numberOfTowerPieces - 1; i++) {
      towers.Add(new Tower());
    }

    for (int i = 0; i < numberOfTowerPieces; i++) {
      towers[0].putPiece(new TowerPiece(i));
    }
  }

  public void pickUp(int indexOfTower)
  {
    currentlyHeldPiece = towers[indexOfTower].pickUpTopPiece();
  }

  public void putDown(int indexOfTower)
  {
    towers[indexOfTower].putPiece(currentlyHeldPiece);
    currentlyHeldPiece = null;
  }

  public GameBoardState getCurrentSceneState()
  {
    return new GameBoardState(towers);
  }
}
