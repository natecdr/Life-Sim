using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LinearActivationFunction: IActivationFunction {
  public double CalculateOutput(double input) {
    return input;
  }
}
