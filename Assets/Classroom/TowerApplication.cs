using System;
using System.Collections;
using System.Collections.Generic;
using domain;
using UnityEngine;

public class TowerApplication : MonoBehaviour {

  List<Tower> towers;

  TowerPiece currentlyHeldPiece = null; // TODO Maybe make a domain representation?
  private const int DEFAULT_SIZE = 4;

  public TowerApplication() : this(DEFAULT_SIZE) { }

  public TowerApplication(int numberOfTowerPieces) {
    towers = new List<Tower>();

    // TODO Clarify these loops
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

  public void putDown(int indexOfTower)
  {
    towers[indexOfTower].putPiece(currentlyHeldPiece);
    currentlyHeldPiece = null;
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
