using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StepActivationFunction : IActivationFunction
{
  private double treshold;

  public StepActivationFunction(double treshold)
  {
    this.treshold = treshold;
  }

  public double CalculateOutput(double input)
  {
    return Convert.ToDouble(input > this.treshold);
  }
}