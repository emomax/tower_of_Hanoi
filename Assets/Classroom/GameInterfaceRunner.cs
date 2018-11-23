﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterfaceRunner : MonoBehaviour, InputSubscriber
{
  private TowerApplication application;
  private List<GameInterfaceEventListener> listeners;

  int currentTowerIndex = -1;

  public GameInterfaceRunner(TowerApplication application)
  {
    this.application = application;
    this.listeners = new List<GameInterfaceEventListener>();
  }

  public void touchedPiece(int weight)
  {
    GameBoardState currentState = application.getCurrentSceneState();

    if (currentPieceIsMovable(weight, currentState))
    {
      application.pickUp(weight);
      publishPickedUpEvent(weight);
    }
    else {
      publishCouldNotPickUpPieceEvent(weight);
    }
  }

  private void publishCouldNotPickUpPieceEvent(int weight)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceCouldNotBeMoved(weight);
    }
  }

  private void publishPickedUpEvent(int weight)
  {
    foreach (GameInterfaceEventListener listener in listeners) {
      listener.piecePickedUp(weight);
    }
  }

  private bool currentPieceIsMovable(int weight, GameBoardState currentState)
  {
    return currentState.canMovePieceWithWeight(weight);
  }

  public void registerListener(GameInterfaceEventListener listener)
  {
    listeners.Add(listener);
  }

  public void enteredDropZoneForTower(int towerIndex)
  {
    currentTowerIndex = towerIndex;
  }

  public void releaseCurrentInput()
  {
    application.putDown(currentTowerIndex);

    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceWasPlacedAtTower(currentTowerIndex);
    }

    currentTowerIndex = -1;
  }
}
