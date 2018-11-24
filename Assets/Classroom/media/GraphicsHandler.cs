using System.Collections;
using System.Collections.Generic;
using domain;
using UnityEngine;

public class GraphicsHandler : GameInterfaceEventListener
{
  [SerializeField] private TowerPiece smallest;
  [SerializeField] private TowerPiece small;
  [SerializeField] private TowerPiece big;
  [SerializeField] private TowerPiece biggest;

  TowerPiece currentPiece;
  private Vector2 pieceStartPosition;

  private List<TowerPiece> pieces;

  public GraphicsHandler() {
    pieces = populateList();
  }

  public void Awake() {
    pieces = populateList();
  }

  private List<TowerPiece> populateList() {
    List<TowerPiece> pieces = new List<TowerPiece>();
    pieces.Add(smallest);
    pieces.Add(small);
    pieces.Add(big);
    pieces.Add(biggest);

    return pieces;
  }

  public override void pieceCouldNotBeMoved(int weight)
  {
    // TODO
  }

  public override void piecePickedUp(int weight)
  {
    currentPiece = pieces[weight];
    pieceStartPosition = currentPiece.transform.position;
  }

  public override void pieceWasDroppedOutsideOfDropZone()
  {
    currentPiece.transform.position = pieceStartPosition;
    currentPiece = null;
  }

  public override void pieceWasPlacedAtTower(int tower)
  {
    // TODO
  }

  public void Update() {
    if (Input.GetMouseButton(0)) {
      if (currentPiece != null) {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPiece.transform.position = newPosition;
      }
    }
  }
}
