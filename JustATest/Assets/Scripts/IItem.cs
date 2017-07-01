using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem {
    bool Grab(Transform origin);
    bool Release();
}
