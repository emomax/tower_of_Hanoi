using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterfaceRunner : MonoBehaviour, InputSubscriber
{
  private TowerApplication application;

  public GameInterfaceRunner(TowerApplication application)
  {
    this.application = application;
  }

  public void touchedPiece(int weight)
  {
    GameBoardState currentState = application.getCurrentSceneState();

    if (currentPieceIsMovable(weight, currentState))
    {
      application.pickUp(weight);
    }
  }

  private bool currentPieceIsMovable(int weight, GameBoardState currentState)
  {
    return true;
  }
}
