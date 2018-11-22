using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace domain
{
  public class Tower : MonoBehaviour
  {
    Stack<TowerPiece> pieces;

    public Tower() {
      pieces = new Stack<TowerPiece>();
    }

    public void putPiece(TowerPiece piece)
    {
      if (pieces.Count == 0) {
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
  }
}