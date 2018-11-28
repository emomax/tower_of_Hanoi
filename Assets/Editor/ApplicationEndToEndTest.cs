using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using Domain;
using System.Collections.Generic;
using State;

public class ApplicationEndToEndTest
{
  [Test]
  public void ApplicationEndToEndTestSimplePasses()
  {
    // Use the Assert class to test conditions.
  }

  // A UnityTest behaves like a coroutine in PlayMode
  // and allows you to yield null to skip a frame in EditMode
  [UnityTest]
  public IEnumerator ApplicationEndToEndTestWithEnumeratorPasses()
  {
    // Use the Assert class to test conditions.
    // yield to skip a frame
    yield return null;
  }

  [Test]
  public void towerPieceMoved_afterUserInput()
  {
    TowerApplication application = new TowerApplication();
    GameBoardState endState = application.getCurrentSceneState();
    Debug.Log("endstate: " + endState.ToString());

    application.pickUp(0);
    application.putDown(1);

    Assert.That(!endState.Equals(application.getCurrentSceneState()));
  }

  [Test]
  public void finishedGame_whenTowerProperlyStackedFromLeftToRight()
  {
    TowerApplication application = new TowerApplication();
    GameBoardState endState = createEndState();

    // 0 -> 1
    application.pickUp(0);
    application.putDown(1);

    // 0 -> 2
    application.pickUp(0);
    application.putDown(2);

    // 1 -> 2
    application.pickUp(1);
    application.putDown(2);

    // 0 -> 1
    application.pickUp(0);
    application.putDown(1);

    // 2 -> 0
    application.pickUp(2);
    application.putDown(0);

    // 2 -> 1
    application.pickUp(2);
    application.putDown(1);

    // 0 -> 1
    application.pickUp(0);
    application.putDown(1);

    // 0 -> 2 - first base to the right
    application.pickUp(0);
    application.putDown(2);

    // 1 -> 2
    application.pickUp(1);
    application.putDown(2);

    // 1 -> 0
    application.pickUp(1);
    application.putDown(0);

    // 2 -> 0
    application.pickUp(2);
    application.putDown(0);

    // 1 -> 2 - second base to the right
    application.pickUp(1);
    application.putDown(2);

    // 0 -> 1
    application.pickUp(0);
    application.putDown(1);

    // 0 -> 2 - third base to the right
    application.pickUp(0);
    application.putDown(2);

    // 1 -> 2 - fourth base to the right
    application.pickUp(1);
    application.putDown(2);

    Assert.That(application.getCurrentSceneState().Equals(endState));
  }

  private GameBoardState createEndState()
  {
    Tower filledTower = new Tower();
    filledTower.putPiece(new TowerPiece(3));
    filledTower.putPiece(new TowerPiece(2));
    filledTower.putPiece(new TowerPiece(1));
    filledTower.putPiece(new TowerPiece(0));

    Tower emptyTower = new Tower();

    List<Tower> towers = new List<Tower>();
    towers.Add(emptyTower);
    towers.Add(emptyTower);
    towers.Add(filledTower);

    return new GameBoardState(towers);
  }
}
