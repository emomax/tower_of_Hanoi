using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

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
  public void towerPieceMoved_afterUserInput() {
    TowerApplication application = new TowerApplication();
    GameBoardState startState = application.getCurrentSceneState();

    application.pickUp(0);
    application.putDown(1);

    GameBoardState state = application.getCurrentSceneState();
    Assert.That(state.Equals(startState));
  }

}
