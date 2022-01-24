using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISynapse {
    double weight {get; set;}
    double GetOutput();
}
