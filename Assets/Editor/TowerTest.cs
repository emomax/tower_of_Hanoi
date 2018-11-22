using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using domain;

public class TowerTest
{
  [Test]
  public void TowerTestSimplePasses()
  {
    // Use the Assert class to test conditions.
  }

  // A UnityTest behaves like a coroutine in PlayMode
  // and allows you to yield null to skip a frame in EditMode
  [UnityTest]
  public IEnumerator TowerTestWithEnumeratorPasses()
  {
    // Use the Assert class to test conditions.
    // yield to skip a frame
    yield return null;
  }

  [Test]
  public void putsTowerPieceOnTop_whenReceivingNewTowerPiece()
  {
    Tower tower = new Tower();
    TowerPiece piece = new TowerPiece(0);

    tower.putPiece(piece);

    Assert.That(piece.getWeight() == tower.pickUpTopPiece().getWeight());
  }
}
