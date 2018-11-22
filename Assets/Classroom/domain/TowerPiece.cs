using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace domain
{
  public class TowerPiece : MonoBehaviour
  {
    private readonly int weight;

    public TowerPiece(int weight) {
      this.weight = weight;
    }

    public int getWeight() {
      return weight;
    }
  }
}