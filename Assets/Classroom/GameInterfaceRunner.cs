using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterfaceRunner : MonoBehaviour, InputSubscriber
{
  [SerializeField] private TowerApplication application;
  private List<GameInterfaceEventListener> listeners;

  int currentTowerIndex = -1;
  int lastTowerIndex = -1;
  bool currentlyHoldingPiece = false;

  public GameInterfaceRunner(TowerApplication application)
  {
    this.application = application;
    this.listeners = new List<GameInterfaceEventListener>();
  }

  public void Awake()
  {
    listeners = new List<GameInterfaceEventListener>();

    currentTowerIndex = -1;
    lastTowerIndex = -1;
  }

  public void touchedPiece(int weight)
  {
    GameBoardState currentState = application.getCurrentSceneState();

    if (currentPieceIsMovable(weight, currentState))
    {
      lastTowerIndex = getTowerIndexForPiece(weight, currentState);
      application.pickUp(lastTowerIndex);
      currentlyHoldingPiece = true;

      publishPickedUpEvent(weight);
    }
    else
    {
      publishCouldNotPickUpPieceEvent(weight);
    }
  }

  // TODO this is .. very long. Break this up.
  public void releaseCurrentInput()
  {
    if (!currentlyHoldingPiece)
    { // Didn't have any piece attached to pointer
      return;
    }

    if (currentTowerIndex != -1)
    {
      bool couldPlacePiece = application.putDown(currentTowerIndex);

      if (!couldPlacePiece)
      {
        application.putDown(lastTowerIndex);
        currentTowerIndex = lastTowerIndex;

        foreach (GameInterfaceEventListener listener in listeners)
        {
          listener.pieceCouldNotBePlaced(lastTowerIndex);
        }
      }

      foreach (GameInterfaceEventListener listener in listeners)
      {
        listener.pieceWasPlacedAtTower(currentTowerIndex);
      }

      currentTowerIndex = -1;
      lastTowerIndex = -1;
    }
    else
    {
      application.putDown(lastTowerIndex);
      lastTowerIndex = -1;

      foreach (GameInterfaceEventListener listener in listeners)
      {
        listener.pieceWasDroppedOutsideOfDropZone();
      }
    }

    currentlyHoldingPiece = false;
  }

  public void enteredDropZoneForTower(int towerIndex)
  {
    currentTowerIndex = towerIndex;

    if (currentlyHoldingPiece)
    {
      foreach (GameInterfaceEventListener listener in listeners)
      {
        listener.pieceHoveredDropZone(towerIndex);
      }
    }
  }

  public void leftDropZone()
  {
    if (currentlyHoldingPiece)
    {
      foreach (GameInterfaceEventListener listener in listeners)
      {
        listener.pieceLeftDropZone(currentTowerIndex);
      }
    }
    currentTowerIndex = -1;
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
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.piecePickedUp(weight);
    }
  }

  private bool currentPieceIsMovable(int weight, GameBoardState currentState)
  {
    return currentState.canMovePieceWithWeight(weight);
  }

  private int getTowerIndexForPiece(int weight, GameBoardState currentState)
  {
    return currentState.getTowerIndexForPiece(weight);
  }

  public void registerListener(GameInterfaceEventListener listener)
  {
    listeners.Add(listener);
  }
}
