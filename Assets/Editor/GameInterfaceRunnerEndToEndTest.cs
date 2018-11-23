using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;

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
  public void failsToPickUpPiece_whenTouchingPieceIsNotTop()
  {
    // This is a substitute for possibly a sound handler, graphics handler, data collection system etc.
    GameInterfaceEventListener genericListener = Substitute.For<GameInterfaceEventListener>();

    InputManager input = new MockInputManager();
    TowerApplication application = Substitute.For<TowerApplication>();
    application.getCurrentSceneState().Returns(startState);

    GameInterfaceRunner interfaceRunner = new GameInterfaceRunner(application);
    interfaceRunner.registerListener(genericListener);

    input.registerListener(interfaceRunner);
    input.touchPiece(1);

    application.DidNotReceive().pickUp(Arg.Any<int>());
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

    public override void touchPiece(int weight)
    {
      foreach (InputSubscriber subscriber in subscribers)
      {
        subscriber.touchedPiece(weight);
      }
    }
  }
}
