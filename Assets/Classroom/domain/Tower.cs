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

    /*
     * Attempts to put the given piece on the stack.
     * Returns true if the current top piece is bigger, otherwise false.
     *
     * I'm not a big fan of this logic.. May revisit this in the future.
     */
    public bool putPiece(TowerPiece piece)
    {
      if (pieces.Count == 0)
      {
        pieces.Push(piece);
        return true;
      }
      else if (pieces.Peek().getWeight() > piece.getWeight())
      {
        pieces.Push(piece);
        return true;
      }

      return false;
    }

    public TowerPiece pickUpTopPiece()
    {
      return pieces.Pop();
    }

    public Tower Clone()
    {
      Stack<TowerPiece> piecesCopy = new Stack<TowerPiece>();
      Tower clone = new Tower();

      foreach (TowerPiece piece in pieces.Reverse())
      {
        clone.putPiece(new TowerPiece(piece.getWeight()));
      }

      return clone;
    }

    /* Returns true if other tower contains the same tower pieces of same weight */
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

    public override string ToString()
    {
      string items = "";

      foreach (TowerPiece piece in pieces)
      {
        items += piece.ToString() + " ";
      }

      return "[ " + items + "]";
    }

    /* PUBLIC HELPERS */

    public int getNumberOfPieces()
    {
      return pieces.Count;
    }

    public bool hasPieces()
    {
      return pieces.Count() > 0;
    }

    public TowerPiece peekAtTopPiece()
    {
      return pieces.Peek();
    }

    public bool hasPieceWithWeight(int weight)
    {
      foreach (TowerPiece piece in pieces)
      {
        if (piece.getWeight() == weight)
        {
          return true;
        }
      }

      return false;
    }
  }
}