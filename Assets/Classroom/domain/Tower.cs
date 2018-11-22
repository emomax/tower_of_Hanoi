using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace domain
{
  public class Tower : MonoBehaviour
  {
    Stack<TowerPiece> pieces;

    public Tower()
    {
      pieces = new Stack<TowerPiece>();
    }

    public void putPiece(TowerPiece piece)
    {
      if (pieces.Count == 0)
      {
        pieces.Push(piece);
        Debug.Log("Put a piece with weight: " + piece.getWeight());
      }
      else if (pieces.Peek().getWeight() > piece.getWeight())
      {
        Debug.Log("Put a piece with weight: " + piece.getWeight());
        pieces.Push(piece);
      }
    }

    public TowerPiece pickUpTopPiece()
    {
      return pieces.Pop();
    }

    public override bool Equals(object other)
    {
      Tower otherTower = other as Tower;

      if (pieces.Count != otherTower.pieces.Count)
      {
        return false;
      }

      int currentPieceIndex = 0;
      foreach (TowerPiece piece in pieces)
      {
        if (!piece.Equals(otherTower.pieces.ElementAt(currentPieceIndex)))
        {
          return false;
        }
      }

      return true;
    }
  }
}