using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UserInput;
using State;
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

    // For some reason, Unity ignores default values so we re-set them upon waking up.
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

  /* This means either that user stopped touching, or
   * that the left mouse button was released. Either way,
   * we are interested as we might be holding stuff.
   */
  public void releaseCurrentInput()
  {
    if (!currentlyHoldingPiece)
    { // Didn't have any piece attached to pointer - nothing to do
      return;
    }

    bool hoveringOverDropzone = currentTowerIndex != -1;
    if (hoveringOverDropzone)
    {
      placeTowerPiece();
    }
    else
    {
      returnTowerPieceToLastTower();
    }

    currentTowerIndex = -1;
    lastTowerIndex = -1;
    currentlyHoldingPiece = false;
  }

  private void placeTowerPiece()
  {
    bool couldPlacePiece = application.putDown(currentTowerIndex);

    if (couldPlacePiece)
    {
      publishPiecePlacedAtTowerEvent(currentTowerIndex);
    }
    else
    {
      publishPieceCouldNotBePlacedEvent(currentTowerIndex);
      application.putDown(lastTowerIndex);
    }
  }

  private void returnTowerPieceToLastTower()
  {
    application.putDown(lastTowerIndex);

    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceWasDroppedOutsideOfDropZone();
    }
  }

  public void enteredDropZoneForTower(int towerIndex)
  {
    currentTowerIndex = towerIndex;

    if (currentlyHoldingPiece)
    {
      publishPieceEnteredDropZoneEvent(towerIndex);
    }
  }

  public void leftDropZone()
  {
    if (currentlyHoldingPiece)
    {
      publishPieceLeftDropZoneEvent(currentTowerIndex);

    }
    currentTowerIndex = -1;
  }


  /* HELPER METHODS */

  /* This will return false if there are other pieces on top of this piece */
  private bool currentPieceIsMovable(int weight, GameBoardState currentState)
  {
    return currentState.canMovePieceWithWeight(weight);
  }

  private int getTowerIndexForPiece(int weight, GameBoardState currentState)
  {
    return currentState.getTowerIndexForPiece(weight);
  }

  /* EVENT HANDLING */

  private void publishPickedUpEvent(int weight)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.piecePickedUp(weight);
    }
  }

  private void publishCouldNotPickUpPieceEvent(int weight)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceCouldNotBeMoved(weight);
    }
  }

  private void publishPiecePlacedAtTowerEvent(int towerIndex)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceWasPlacedAtTower(towerIndex);
    }
  }

  private void publishPieceCouldNotBePlacedEvent(int currentTowerIndex)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceCouldNotBePlaced(currentTowerIndex);
    }
  }

  private void publishPieceEnteredDropZoneEvent(int towerIndex)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceHoveredDropZone(towerIndex);
    }
  }

  private void publishPieceLeftDropZoneEvent(int abandonedTower)
  {
    foreach (GameInterfaceEventListener listener in listeners)
    {
      listener.pieceLeftDropZone(abandonedTower);
    }
  }

  public void registerListener(GameInterfaceEventListener listener)
  {
    listeners.Add(listener);
  }
}
