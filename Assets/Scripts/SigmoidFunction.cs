using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SigmoidFunction : IActivationFunction {
  private double coefficient;

  public SigmoidFunction(double coefficient) {
    this.coefficient = coefficient;
  }
  public double CalculateOutput(double input) {
    return (1 / (1 + Math.Exp(-input * coefficient)));
  }
}
