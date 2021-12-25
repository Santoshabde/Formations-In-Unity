using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShape
{
    void Form(Transform objTransform, List<GameObject> indicatorsList);

    int AmountToExpand(int amount);
}
