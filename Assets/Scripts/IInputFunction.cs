using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInputFunction {
  double CalculateInput(List<ISynapse> inputSynapses);
}
