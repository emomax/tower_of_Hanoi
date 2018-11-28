using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
  /* One of the parts of a tower in the application
     It is important to understand that there will only
     exist one piece per weight, from
     (0 -> (number of total pieces - 1))

     They could therefore, in practice, be used as IDs.
     */
  public class TowerPiece : MonoBehaviour
  {
    [SerializeField] private int weight;

    /* Seeing as these are only use for graphics, and given
       chosen the domain model these won't interfere and doesn't
       need intialization in the constructor. This could however
       be split up to separate concerns of logic and looks,
       but doesn't hurt too much, so I'll keep it for now. */
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
    public void shownAsOnTopPieceOfPillar() {
      backside.sortingOrder = 1;
      frontside.sortingOrder = 10 - weight;
      face.sortingOrder = 10 - (weight - 1);
    }

    public override string ToString()
    {
      return "" + weight;
    }

    /* GRAPHICAL HELPERS */

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