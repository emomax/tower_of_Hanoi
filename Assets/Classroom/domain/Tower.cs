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
      }
      else if (pieces.Peek().getWeight() > piece.getWeight())
      {
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
        if (!piece.Equals(otherTower.pieces.ElementAt(currentPieceIndex++)))
        {
          return false;
        }
      }

      return true;
    }

    public override string ToString() {
      string items = "";

      foreach (TowerPiece piece in pieces) {
        items += piece.ToString() + " ";
      }

      return "[ " + items + "]";
    }

    public Tower Clone() {
      Stack<TowerPiece> piecesCopy = new Stack<TowerPiece>();
      Tower clone = new Tower();

      foreach (TowerPiece piece in pieces.Reverse()) {
        clone.putPiece(new TowerPiece(piece.getWeight()));
      }

      return clone;
    }
  }
}