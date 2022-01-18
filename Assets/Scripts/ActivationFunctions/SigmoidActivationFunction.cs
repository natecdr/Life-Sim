using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SigmoidActivationFunction : IActivationFunction {
  private double coefficient;

  public SigmoidActivationFunction(double coefficient) {
    this.coefficient = coefficient;
  }
  public double CalculateOutput(double input) {
    return (1 / (1 + Math.Exp(-input * coefficient)));
  }
}
