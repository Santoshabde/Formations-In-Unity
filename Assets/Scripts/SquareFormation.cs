using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFormation : IShape
{
    public float sideLength;

    private int indicatorListCount = 0;
    public void Form(Transform objTransform, List<GameObject> indicatorsList)
    {
        indicatorListCount = indicatorsList.Count;
        int numberOnEachLine = (int)Mathf.Sqrt(indicatorsList.Count);
        int indicatorListIndex = 0;

        for (int j = 0; j < numberOnEachLine; j++)
        {
            Vector3 spawnPosition = objTransform.position + (objTransform.right * j * sideLength);
            int lengthMultiplePositive = 1;
            int lengthMultipleNegative = 1;

            indicatorsList[indicatorListIndex].transform.position = spawnPosition;
            indicatorListIndex += 1;

            for (int i = 1; i < numberOnEachLine; i++)
            {
                int sign = 1;
                if (numberOnEachLine % i == 0)
                {
                    sign = -1;

                    spawnPosition = (sign * objTransform.forward * sideLength * lengthMultiplePositive) + ((objTransform.right * j * sideLength));
                    indicatorsList[indicatorListIndex].transform.position = spawnPosition + objTransform.position;
                    lengthMultiplePositive += 1;
                }
                else
                {
                    spawnPosition = (sign * objTransform.forward * sideLength * lengthMultipleNegative) + ((objTransform.right * j * sideLength));
                    indicatorsList[indicatorListIndex].transform.position = spawnPosition + objTransform.position;
                    lengthMultipleNegative += 1;
                }

                indicatorListIndex += 1;
            }
        }    
    }

    public int AmountToExpand(int amount)
    {
        int toAdd = 0;
        int numberOfAgentsInEachRow = (int)Mathf.Sqrt(indicatorListCount);
        for (int i = 0; i < amount; i++)
        {
            toAdd += (numberOfAgentsInEachRow * 2) + 1;
            numberOfAgentsInEachRow += 1;
        }

        return toAdd;
    }
}
