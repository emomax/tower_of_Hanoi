using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace domain
{
  public class TowerPiece : MonoBehaviour
  {
    [SerializeField] private int weight;

    public TowerPiece(int weight) {
      this.weight = weight;
    }

    public int getWeight() {
      return weight;
    }

    public override bool Equals(object other) {
      if (other == null) {
        return false;
      }

      TowerPiece otherPiece = other as TowerPiece;
      return weight == otherPiece.weight;
    }


    public override string ToString()
    {
      return "" + weight;
    }
  }
}