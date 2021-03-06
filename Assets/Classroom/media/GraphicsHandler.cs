﻿using System.Collections;
using System.Collections.Generic;
using Domain;
using Event;
using State;
using UnityEngine;

namespace Media
{
  public class GraphicsHandler : GameInterfaceEventListener
  {
    [SerializeField] private TowerPiece smallest;
    [SerializeField] private TowerPiece small;
    [SerializeField] private TowerPiece big;
    [SerializeField] private TowerPiece biggest;

    [SerializeField] private GameObject dustFromFallingParticles;
    [SerializeField] private GameObject smokePuff;

    // In order to find new positions
    [SerializeField] private TowerApplication application;

    TowerPiece currentPiece;

    private float animationStartTime;

    private Vector2 pieceStartPosition;
    private List<TowerPiece> pieces;

    private int currentTower = -1;
    private List<Vector2> dropZonePosition;

    public GraphicsHandler()
    {
      pieces = populateList();
    }

    public void Awake()
    {
      pieces = populateList();
      currentTower = -1;
      dropZonePosition = new List<Vector2>();
      dropZonePosition.Add(new Vector2(-246f, 214f));
      dropZonePosition.Add(new Vector2(2f, 214f));
      dropZonePosition.Add(new Vector2(246f, 214f));
    }

    private List<TowerPiece> populateList()
    {
      List<TowerPiece> pieces = new List<TowerPiece>();
      pieces.Add(smallest);
      pieces.Add(small);
      pieces.Add(big);
      pieces.Add(biggest);

      return pieces;
    }

    public override void pieceCouldNotBeMoved(int weight)
    {
      pieces[weight].wiggle();
    }

    public override void pieceHoveredDropZone(int tower)
    {
      currentTower = tower;
      animationStartTime = Time.time;
      currentPiece.shownAsOnTopPieceOfPillar();
    }

    public override void pieceLeftDropZone(int tower)
    {
      currentTower = -1;
      animationStartTime = Time.time;
      currentPiece.showInFrontOfEverything();
    }

    public override void piecePickedUp(int weight)
    {
      currentPiece = pieces[weight];
      currentPiece.showInFrontOfEverything();
      currentPiece.excitedPickUp();
      pieceStartPosition = currentPiece.transform.position;
    }

    public override void pieceWasDroppedOutsideOfDropZone()
    {
      Vector2 positionWherePieceWasDropped = new Vector2(currentPiece.transform.position.x,
                                                        currentPiece.transform.position.y);

      Instantiate(smokePuff,
                  positionWherePieceWasDropped,
                  currentPiece.transform.rotation);

      Instantiate(smokePuff,
                  currentPiece.transform.position,
                  currentPiece.transform.rotation,
                  currentPiece.transform);

      currentPiece.transform.position = pieceStartPosition;
      currentPiece.shownAsOnTopPieceOfPillar();
      currentPiece.slowBreathing();
      currentPiece = null;
    }

    public override void pieceCouldNotBePlaced(int currentTowerIndex)
    {
      Vector2 positionWherePieceWasDropped = new Vector2(currentPiece.transform.position.x,
                                                        currentPiece.transform.position.y);

      Instantiate(smokePuff,
                  positionWherePieceWasDropped,
                  currentPiece.transform.rotation);

      Instantiate(smokePuff,
              currentPiece.transform.position,
              currentPiece.transform.rotation,
              currentPiece.transform);

      currentPiece.transform.position = pieceStartPosition;
      currentPiece.shownAsOnTopPieceOfPillar();
      currentPiece.temporarilySad();
      currentPiece = null;
      currentTower = -1;
    }

    public override void pieceWasPlacedAtTower(int tower)
    {
      GameBoardState state = application.getCurrentSceneState();
      currentPiece.transform.localPosition = getPositionAtNewTower(state, tower);
      currentPiece.shownAsOnTopPieceOfPillar();
      currentPiece.slowBreathing();

      bool pieceChangedTower = (currentTower != -1);
      if (pieceChangedTower) // We fell down from dropzone
      {
        Debug.Log("Changed tower, show particles! ");
        Instantiate(dustFromFallingParticles,
                    currentPiece.transform.position,
                    currentPiece.transform.rotation,
                    currentPiece.transform);
      }
      else
      {
        Debug.Log("Went back, show smoke! ");
        Instantiate(smokePuff,
              currentPiece.transform.position,
              currentPiece.transform.rotation,
              currentPiece.transform);
      }

      currentPiece = null;
      currentTower = -1;
    }

    public void Update()
    {
      if (currentPiece != null)
      {
        float animationDuration = 0.3f;

        if (currentTower != -1)
        {
          currentPiece.transform.localPosition = getTweenedPosition(currentPiece.transform.localPosition, dropZonePosition[currentTower], animationDuration);
        }
        else if (Input.GetMouseButton(0))
        {
          Vector2 goalDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          currentPiece.transform.position = getTweenedPosition(currentPiece.transform.position, goalDestination, animationDuration);
        }
      }
    }

    private Vector2 getTweenedPosition(Vector2 startPosition, Vector2 goalPosition, float animationDuration)
    {
      float animationTimePassed = (Time.time - animationStartTime) / animationDuration;
      float smoothX = Mathf.SmoothStep(startPosition.x, goalPosition.x, animationTimePassed);
      float smoothY = Mathf.SmoothStep(startPosition.y, goalPosition.y, animationTimePassed);

      return new Vector2(smoothX, smoothY);
    }

    private Vector2 getPositionAtNewTower(GameBoardState state, int tower)
    {
      int numberOfPieces = state.getNumberOfPiecesForTower(tower);
      const int PIECE_HEIGHT = 65;

      return new Vector2(dropZonePosition[tower].x, -120 + (PIECE_HEIGHT * (numberOfPieces - 1)));
    }
  }
}