using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputPublisher
{
  void register(InputSubscriber listener);
}
