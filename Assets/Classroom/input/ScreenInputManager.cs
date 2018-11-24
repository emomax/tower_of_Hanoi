using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is abstract to avoid custom serializing of interfaces -
   It is however an interface in all but type */
public class ScreenInputManager : InputManager
{
  [SerializeField] private List<InputSubscriber> subscribers = new List<InputSubscriber>();

  // Unity game entry point for this class
  public void Start() {
  }

  public override void enteredDropZoneForTower(int towerIndex)
  {
    foreach (InputSubscriber subscriber in subscribers)
    {
      subscriber.enteredDropZoneForTower(towerIndex);
    }
  }

  public override void leftDropZone()
  {
    foreach (InputSubscriber subscriber in subscribers)
    {
      subscriber.leftDropZone();
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

  public override void registerListener(InputSubscriber listener)
  {
    subscribers.Add(listener);
  }

  public void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      releaseCurrentInput();
    }
  }
}