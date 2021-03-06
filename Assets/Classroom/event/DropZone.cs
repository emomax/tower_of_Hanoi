﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
  public class DropZone : MonoBehaviour
  {

    [SerializeField] InputManager input;
    [SerializeField] int towerIndex;

    void OnMouseEnter()
    {
      input.enteredDropZoneForTower(towerIndex);
    }

    void OnMouseExit()
    {
      input.leftDropZone();
    }
  }
}