using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using State;
using Event;
using UserInput;

public class GameInterfaceRunnerEndToEndTest
{
  TowerApplication application;
  GameBoardState startState;

  [SetUp]
  public void before()
  {
    application = new TowerApplication();
    startState = application.getCurrentSceneState();
  }

  [Test]
  public void GameInterfaceRunnerSimplePasses()
  {
    // Use the Assert class to test conditions.
  }

  // A UnityTest behaves like a coroutine in PlayMode
  // and allows you to yield null to skip a frame in EditMode
  [UnityTest]
  public IEnumerator GameInterfaceRunnerWithEnumeratorPasses()
  {
    // Use the Assert class to test conditions.
    // yield to skip a frame
    yield return null;
  }

  [Test]
  public void picksUpPiece_whenPickingTopPieceOfATower()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = Substitute.For<TowerApplication>();
    application.getCurrentSceneState().Returns(startState);

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);
    input.touchPiece(0);

    application.Received().pickUp(0);
    genericListener.Received().piecePickedUp(0);
  }

  [Test]
  public void pieceChangesTower_whenPickingAndMovingTopPieceOfATower()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = new TowerApplication();
    GameBoardState startState = application.getCurrentSceneState();

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);
    input.touchPiece(0);
    input.enteredDropZoneForTower(1);
    input.releaseCurrentInput();

    genericListener.Received().piecePickedUp(0);
    genericListener.Received().pieceWasPlacedAtTower(1);

    Assert.That(!application.getCurrentSceneState().Equals(startState));
  }

  [Test]
  public void pieceChangesTower_whenPickingUpAndMovingInAndOutOfDropZone()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = new TowerApplication();
    GameBoardState startState = application.getCurrentSceneState();

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);
    input.touchPiece(0);

    input.enteredDropZoneForTower(1);
    input.leftDropZone();
    input.enteredDropZoneForTower(2);

    input.releaseCurrentInput();

    genericListener.Received().piecePickedUp(0);
    genericListener.Received().pieceWasPlacedAtTower(2);

    Assert.That(!application.getCurrentSceneState().Equals(startState));
  }

  [Test]
  public void pieceIsReturned_whenPickedUpAndMovedToTowerWithLighterTopPiece()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = new TowerApplication();
    GameBoardState startState = application.getCurrentSceneState();

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);

    input.touchPiece(0);
    input.enteredDropZoneForTower(1);
    input.releaseCurrentInput();

    input.touchPiece(1);
    input.enteredDropZoneForTower(1);
    input.releaseCurrentInput();

    genericListener.Received(1).piecePickedUp(0);
    genericListener.Received(1).piecePickedUp(1);
    genericListener.Received(1).pieceWasPlacedAtTower(1);
    genericListener.Received(1).pieceCouldNotBePlaced(1);
  }

  [Test]
  public void pickedUpPieceReturns_whenPickingAndMovingTopPieceOutsideOfAnyDropZone()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = new TowerApplication();
    GameBoardState startState = application.getCurrentSceneState();

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);
    input.touchPiece(0);
    input.releaseCurrentInput();

    genericListener.Received().piecePickedUp(0);
    genericListener.Received().pieceWasDroppedOutsideOfDropZone();

    Assert.That(application.getCurrentSceneState().Equals(startState));
  }

  [Test]
  public void failsToPickUpPiece_whenTouchingPieceIsNotTop()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = new TowerApplication();

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);
    input.touchPiece(1);

    genericListener.Received().pieceCouldNotBeMoved(1);
  }

  private class MockInputManager : InputManager
  {
    private List<InputSubscriber> subscribers;

    public MockInputManager()
    {
      subscribers = new List<InputSubscriber>();
    }

    public override void registerListener(InputSubscriber listener)
    {
      subscribers.Add(listener);
    }

    public override void enteredDropZoneForTower(int towerIndex)
    {
      foreach (InputSubscriber subscriber in subscribers)
      {
        subscriber.enteredDropZoneForTower(towerIndex);
      }
    }

    public override void releaseCurrentInput()
    {
      foreach (InputSubscriber subscriber in subscribers)
      {
        subscriber.releaseCurrentInput();
      }
    }

    public override void touchPiece(int weight)
    {
      foreach (InputSubscriber subscriber in subscribers)
      {
        subscriber.touchedPiece(weight);
      }
    }

    public override void leftDropZone()
    {
      foreach (InputSubscriber subscriber in subscribers)
      {
        subscriber.leftDropZone();
      }
    }
  }
}
