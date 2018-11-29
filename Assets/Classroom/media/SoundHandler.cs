using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

namespace Media
{
  public class SoundHandler : GameInterfaceEventListener
  {
    [SerializeField] private AudioClip backgroundMusic;

    [SerializeField] private AudioClip towerPiecePlacedAtTower;
    [SerializeField] private AudioClip towerPiecePlacedAtTowerAlternative2;
    [SerializeField] private AudioClip towerPiecePlacedAtTowerAlternative3;

    [SerializeField] private AudioClip towerPiecePickedUp;
    [SerializeField] private AudioClip towerPieceDropped;
    [SerializeField] private AudioClip towerPieceFailedToBePickedUp;

    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource soundEffectsSource;

    public void Start() {
      backgroundSource.loop = true;
      backgroundSource.clip = backgroundMusic;
      backgroundSource.volume = 0.9f;
      backgroundSource.Play();
    }

    public override void pieceCouldNotBeMoved(int weight)
    {
      soundEffectsSource.PlayOneShot(towerPieceFailedToBePickedUp, 0.6f);
    }

    public override void pieceCouldNotBePlaced(int currentTowerIndex)
    {
      soundEffectsSource.PlayOneShot(towerPieceDropped);
    }

    public override void pieceHoveredDropZone(int tower)
    {
      // Ignore?
    }

    public override void pieceLeftDropZone(int tower)
    {
      // Ignore?
    }

    public override void piecePickedUp(int weight)
    {
      soundEffectsSource.PlayOneShot(towerPiecePickedUp);
    }

    public override void pieceWasDroppedOutsideOfDropZone()
    {
      soundEffectsSource.PlayOneShot(towerPieceDropped);
    }

    public override void pieceWasPlacedAtTower(int tower)
    {
      soundEffectsSource.PlayOneShot(towerPieceDropped);
      soundEffectsSource.PlayOneShot(getRandomRinging(), 0.3f);
    }

    private AudioClip getRandomRinging() {
      int randomVersion = Random.Range(0, 3);

      switch (randomVersion) {
        case 0:
          return towerPiecePlacedAtTower;
        case 1:
          return towerPiecePlacedAtTowerAlternative2;
        case 2:
          return towerPiecePlacedAtTowerAlternative3;
        default:
          throw new UnityException("Unmapped sound! There are only 3 possible ones at this point!");
      }
    }
  }
}