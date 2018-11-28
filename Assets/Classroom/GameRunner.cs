using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;

/* The entry point for the game. Here we set up
   the rule engine (application) and the interface
   runner, with respective top-level listeners */
public class GameRunner : MonoBehaviour {
  [SerializeField] private InputManager input;
  [SerializeField] private GameInterfaceRunner runner;

  [SerializeField] private List<GameInterfaceEventListener> gameInterfaceListeners;

  public GameRunner(InputManager input, GameInterfaceRunner runner,
                    List<GameInterfaceEventListener> listeners) {
    this.input = input;
    this.runner = runner;
    this.gameInterfaceListeners = listeners;

    registerListeners();
  }

  public void Start() {
    registerListeners();
  }

  private void registerListeners()
  {
    foreach (var listener in gameInterfaceListeners) {
      runner.registerListener(listener);
    }

    input.registerListener(runner);
  }
}
