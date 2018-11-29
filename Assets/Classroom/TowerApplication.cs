using System;
using System.Collections;
using System.Collections.Generic;
using Domain;
using State;
using UnityEngine;

public class TowerApplication : MonoBehaviour {

  List<Tower> towers;

  TowerPiece currentlyHeldPiece = null;
  private const int DEFAULT_SIZE = 4;

  public TowerApplication() : this(DEFAULT_SIZE) { }

  public TowerApplication(int numberOfTowerPieces) {
    towers = new List<Tower>();

    for (int i = 0; i < numberOfTowerPieces - 1; i++) {
      towers.Add(new Tower());
    }

    for (int i = numberOfTowerPieces - 1; i >= 0; i--) {
      towers[0].putPiece(new TowerPiece(i));
    }
  }

  public void pickUp(int indexOfTower)
  {
    currentlyHeldPiece = towers[indexOfTower].pickUpTopPiece();
  }

  public bool putDown(int indexOfTower)
  {
    bool couldPlacePiece = towers[indexOfTower].putPiece(currentlyHeldPiece);

    if (couldPlacePiece) {
      currentlyHeldPiece = null;
    }

    return couldPlacePiece;
  }

  public virtual GameBoardState getCurrentSceneState()
  {
    List<Tower> towersCopy = new List<Tower>();

    foreach (Tower tower in towers) {
      towersCopy.Add(tower.Clone());
    }

    return new GameBoardState(towersCopy);
  }
}
