using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace domain
{
  public class TowerPiece : MonoBehaviour
  {
    [SerializeField] private int weight;
    [SerializeField] private SpriteRenderer backside;
    [SerializeField] private SpriteRenderer frontside;
    [SerializeField] private SpriteRenderer face;

    public TowerPiece(int weight) {
      this.weight = weight;
    }

    public int getWeight() {
      return weight;
    }

    public override bool Equals(object other) {
      if (other == null) {
        return false;
      }

      TowerPiece otherPiece = other as TowerPiece;
      return weight == otherPiece.weight;
    }

    public override string ToString()
    {
      return "" + weight;
    }

    /* Seeing as we know that there are not more than
     * 6 layers - 1 for background, 1 for pillars, and
     * 1 for each of the pieces, this way we really put
     * render current piece ontop of everything.
     * Without this, it looks wonky as the backside is
     * behind the pillar..
     *  */
    public void showInFrontOfEverything() {
      backside.sortingOrder += 10;
      frontside.sortingOrder += 10;
      face.sortingOrder += 10;
    }

    /* Background has order 0, pillars 2 and thusly
     * the backside have the order 1.
     * The magic-number 10 is for layer 3 + number of
     * tower pieces available * 2 - the "lighter" the piece,
     * the higher render priority.
     */
    public void shownAsOnTopOfPillar() {
      backside.sortingOrder = 1;
      frontside.sortingOrder = 10 - weight;
      face.sortingOrder = 10 - (weight - 1);
    }

    public void slowBreathing()
    {
      GetComponent<Animator>().Play("idle");
    }

    public void excitedPickUp()
    {
      GetComponent<Animator>().Play("grabbed");
    }

    public void wiggle()
    {
      GetComponent<Animator>().Play("wiggle");
    }

    public void temporarilySad()
    {
      GetComponent<Animator>().Play("temporarily_sad");
    }
  }
}