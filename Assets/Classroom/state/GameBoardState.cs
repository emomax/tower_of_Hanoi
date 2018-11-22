using System.Collections;
using System.Collections.Generic;
using domain;
using UnityEngine;

public class GameBoardState : MonoBehaviour {
  private List<Tower> towers;

  public GameBoardState(List<Tower> towers)
  {
    this.towers = towers;
  }

  public override bool Equals(object other) {
    GameBoardState otherState = other as GameBoardState;

    for (int i = 0; i < towers.Count; i++) {
      if (!towers[i].Equals(otherState.towers[i])) {
        return false;
      }
    }

    return true;
  }
}
