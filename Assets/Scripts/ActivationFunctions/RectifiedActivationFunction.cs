using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RectifiedActivationFunction: IActivationFunction {
  public double CalculateOutput(double input) {
    return Math.Max(0, input);
  }
}
