using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageHandler
{
    void hit(GameObject hitter, float val);
}
