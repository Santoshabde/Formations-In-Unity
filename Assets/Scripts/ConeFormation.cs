using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeFormation : IShape
{
    public float angle;
    public float slantHeight;

    public void Form(Transform objTransform, List<GameObject> indicatorsList)
    {
        Vector3 spawnPosition = objTransform.position;
        indicatorsList[0].transform.position = spawnPosition;

        int PositiveSideIndex = 1;
        int negativeSideIndex = 1;

        bool coneSide = true;
        for (int i = 1; i < indicatorsList.Count; i++)
        {
            if(coneSide)
            {
                indicatorsList[i].transform.position = ((Quaternion.AngleAxis(angle, Vector3.up) * -objTransform.forward) * slantHeight * PositiveSideIndex) + objTransform.position;
                PositiveSideIndex += 1;
                coneSide = false;
            }
            else
            {
                indicatorsList[i].transform.position = ((Quaternion.AngleAxis(-angle, Vector3.up) * -objTransform.forward) * slantHeight * negativeSideIndex) + objTransform.position;
                negativeSideIndex += 1;
                coneSide = true;
            }
        }
    }

    public int AmountToExpand(int amount)
    {
        return amount;
    }
}
