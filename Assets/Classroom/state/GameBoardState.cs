using System;
using System.Collections;
using System.Collections.Generic;
using domain;
using UnityEngine;

public class GameBoardState : MonoBehaviour
{
  private List<Tower> towers;

  public GameBoardState(List<Tower> towers)
  {
    this.towers = towers;
  }

  public override bool Equals(object other)
  {
    GameBoardState otherState = other as GameBoardState;

    for (int i = 0; i < towers.Count; i++)
    {
      if (!towers[i].Equals(otherState.towers[i]))
      {
        return false;
      }
    }

    return true;
  }

  public override string ToString()
  {
    string towersOutput = "";

    foreach (Tower tower in towers)
    {
      towersOutput += tower.ToString() + " ";
    }

    return "{ " + towersOutput + "}";
  }

  public bool canMovePieceWithWeight(int weight)
  {
    foreach (Tower tower in towers) {
      if (tower.hasPieces() && tower.peekAtTopPiece().getWeight() == weight) {
        return true;
      }
    }

    return false;
  }

  public int getTowerIndexForPiece(int weight)
  {
    for (int i = 0; i < towers.Count; i++)
    {
      if (towers[i].hasPieceWithWeight(weight))
      {
        return i;
      }
    }

    throw new UnityException("Piece with weight '" + weight + "' did not exist. WEIRD.");
  }

  public int getNumberOfPiecesForTower(int tower)
  {
    return towers[tower].getNumberOfPieces();
  }
}
