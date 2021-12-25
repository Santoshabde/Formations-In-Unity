using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLineFormation : IShape
{
    public float length = 0;

    public void Form(Transform objTransform, List<GameObject> indicatorsList)
    {
        Vector3 spawnPosition = objTransform.position;
        int lengthMultiple = 1;

        indicatorsList[0].transform.position = spawnPosition;

        for (int i = 1; i < indicatorsList.Count; i++)
        {
            spawnPosition = -objTransform.forward * length * lengthMultiple;
            indicatorsList[i].transform.position = spawnPosition + objTransform.position;
            lengthMultiple += 1;
        }
    }

    public int AmountToExpand(int amount)
    {
        return amount;
    }
}
