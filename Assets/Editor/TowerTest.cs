﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Domain;

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

  [Test]
  public void placingPieceFails_whenCurrentTopPieceIsSmaller()
  {
    Tower tower = new Tower();
    TowerPiece smallPiece = new TowerPiece(0);
    TowerPiece piece = new TowerPiece(1);

    tower.putPiece(smallPiece);
    tower.putPiece(piece);

    Assert.That(piece.getWeight() != tower.pickUpTopPiece().getWeight());
  }

  [Test]
  public void TowerEqualsTest() {
    Tower anEmptyTower = new Tower();
    Tower anotherEmptyTower = new Tower();

    Assert.That(anEmptyTower.Equals(anotherEmptyTower));
  }

  [Test]
  public void TowerUnequalsTest() {
    Tower aPopulatedTower = new Tower();
    Tower aDifferentlyPopulatedTower = new Tower();

    aPopulatedTower.putPiece(new TowerPiece(0));
    aDifferentlyPopulatedTower.putPiece(new TowerPiece(1));

    Assert.That(!aPopulatedTower.Equals(aDifferentlyPopulatedTower));
  }

  [Test]
  public void TowerContainsPieceTest() {
    Tower aPopulatedTower = new Tower();

    aPopulatedTower.putPiece(new TowerPiece(0));
    aPopulatedTower.putPiece(new TowerPiece(1));

    Assert.That(aPopulatedTower.hasPieceWithWeight(0));
  }

  [Test]
  public void TowerDoesNotContainPieceTest()
  {
    Tower aPopulatedTower = new Tower();
    Assert.That(!aPopulatedTower.hasPieceWithWeight(0));
  }
}
