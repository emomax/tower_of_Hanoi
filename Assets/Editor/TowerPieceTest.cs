using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Domain;

public class TowerPieceTest
{

  [Test]
  public void TowerPieceTestSimplePasses()
  {
    // Use the Assert class to test conditions.
  }

  // A UnityTest behaves like a coroutine in PlayMode
  // and allows you to yield null to skip a frame in EditMode
  [UnityTest]
  public IEnumerator TowerPieceTestWithEnumeratorPasses()
  {
    // Use the Assert class to test conditions.
    // yield to skip a frame
    yield return null;
  }

  [Test]
  public void TowerPieceEqualsTest()
  {
    TowerPiece aPiece = new TowerPiece(0);
    TowerPiece anotherPieceWithSameWeight = new TowerPiece(0);

    Assert.That(aPiece.Equals(anotherPieceWithSameWeight));
  }

  [Test]
  public void TowerPieceUnequalsTest()
  {
    TowerPiece aPiece = new TowerPiece(0);
    TowerPiece aDifferentPiece = new TowerPiece(1);

    Assert.That(!aPiece.Equals(aDifferentPiece));

  }
}
