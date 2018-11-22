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
      pieces.Push(piece);
    }

    public TowerPiece pickUpTopPiece()
    {
      return pieces.Pop();
    }
  }
}