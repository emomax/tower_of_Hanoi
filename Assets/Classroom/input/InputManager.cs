﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is abstract to avoid custom serializing of interfaces -
   It is however an interface in all but type */
public abstract class InputManager : MonoBehaviour
{
  public abstract void registerListener(InputSubscriber listener);

  public abstract void touchPiece(int weight);
  public abstract void releaseCurrentInput();

  public abstract void enteredDropZoneForTower(int towerIndex);
  public abstract void leftDropZone();
}
