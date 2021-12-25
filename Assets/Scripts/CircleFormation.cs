using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFormation : IShape
{
    public float radius;

    public void Form(Transform objTransform, List<GameObject> indicatorsList)
    {
        int i = 0;
        Vector3 forward = (objTransform.forward) * radius;
        indicatorsList[i].transform.position = forward;

        float degreesToIncrease = 360f / indicatorsList.Count;

        float currentDegree = 0;
        while (currentDegree < 360)
        {
            currentDegree += degreesToIncrease;

            Vector3 instantiationPoint = Quaternion.AngleAxis(currentDegree, Vector3.up) * (forward);
            if(i < indicatorsList.Count)
            indicatorsList[i].transform.position = instantiationPoint + objTransform.position;

            i += 1;
        }
    }

    public int AmountToExpand(int amount)
    {
        return amount;   
    }

}
