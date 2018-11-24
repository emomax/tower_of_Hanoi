using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : MonoBehaviour {
  [SerializeField] private InputManager input;
  [SerializeField] private GameInterfaceRunner runner;

  [SerializeField] private GameInterfaceEventListener graphicsHandler;
  [SerializeField] private GameInterfaceEventListener soundHandler;

  public GameRunner(InputManager input, GameInterfaceRunner runner,
                    GameInterfaceEventListener graphicsHandler,
                    GameInterfaceEventListener soundHandler) {
    this.input = input;
    this.runner = runner;
    this.graphicsHandler = graphicsHandler;
    this.soundHandler = soundHandler;

    registerListeners();
  }

  public void Start() {
    registerListeners();
  }

  private void registerListeners()
  {
    runner.registerListener(graphicsHandler);
    runner.registerListener(soundHandler);

    input.registerListener(runner);
  }
}
