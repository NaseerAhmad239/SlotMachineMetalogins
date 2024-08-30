using Mkey;
using NaughtyAttributes;
using System.Collections.Generic;

//using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class ModeType : MonoBehaviour
{
    //public float ModeBetMultiplyer;
    public static ModeType Instance;
    public ClassMatchPattern[] MatchPattern;

    public int BetIcrementAndDecrementValue;


    [SerializeField]
    public int maxLineBet = 100;
    public List<PayLine> PayLineList;
    public bool MoonSlot;

    private void Awake()
    {
        Instance = this;
    }

    //public List<PayLine> mainListOfPayLine;

    public int givenvalue, min, max;
    public int arrayLength = 3;

    [Button]
    public void spawnWild()
    {
        GenerateCombinations(givenvalue, min, max);
    }


    public void GenerateCombinations(int givenValue, int minValue, int maxValue)
    {
        List<int[]> combinations = new List<int[]>();

        //// Generate combinations with the given array length
        //// Pattern 1: {givenValue, 0, 0} to {givenValue, max, max}
        //for (int i = minValue; i <= maxValue; i++)
        //{
        //    combinations.Add(new int[] { givenValue, i, i, i, i });
        //}

        //for (int i = minValue; i <= maxValue; i++)
        //{
        //    combinations.Add(new int[] { givenValue, givenValue, i, i, i });
        //}
        //for (int i = minValue; i <= maxValue; i++)
        //{
        //    combinations.Add(new int[] { givenValue, givenValue, givenValue, i, i });
        //}
        //for (int i = minValue; i <= maxValue; i++)
        //{
        //    combinations.Add(new int[] { givenValue, givenValue, givenValue, givenValue, i });
        //}
        //combinations.Add(new int[] { givenValue, givenValue, givenValue, givenValue, givenValue });

        //foreach (var combination in combinations)
        //{
        //    PayLine newPayLine = new PayLine();
        //    newPayLine.line = combination;
        //    GetComponent<SlotController>().payTable.Add(newPayLine);
        //}

        for (int i = minValue; i < arrayLength - 1; i++)
        {
            for (int k = minValue; k <= maxValue; k++)
            {

                int[] sample = new int[arrayLength];
                for (int j = 0; j < arrayLength; j++)
                {
                    if (i < j)
                    {
                        sample[j] = k;
                    }
                    else
                    {
                        sample[j] = givenValue;
                    }
                }
                combinations.Add(sample);
            }

        }
        foreach (var combination in combinations)
        {
            PayLine newPayLine = new PayLine();
            newPayLine.line = combination;
            // GetComponent<SlotController>().payTable.Add(newPayLine);
            PayLineList.Add(newPayLine);
        }
    }


    [Button]
    public void spawnAllPossibleSamples()
    {
        GenerateCombinationsOfPossibleSameples(givenvalue, arrayLength);
    }

    public void GenerateCombinationsOfPossibleSameples(int givenValue, int arrayLength)
    {
        List<int[]> combinations = new List<int[]>();
        int[] templateArray = new int[arrayLength];
        for (int i = 0; i < arrayLength; i++)
        {
            templateArray[i] = -1;
        }

        // All values are given value
        combinations.Add((int[])templateArray.Clone());

        // Generate combinations with -1 replacing givenValue, ensuring at least 3 given values
        for (int first = 0; first < arrayLength; first++)
        {
            for (int second = first + 1; second < arrayLength; second++)
            {
                for (int third = second + 1; third < arrayLength; third++)
                {
                    int[] combination = (int[])templateArray.Clone();
                    combination[first] = givenValue;
                    combination[second] = givenValue;
                    combination[third] = givenValue;
                    combinations.Add(combination);
                }
            }
        }

        foreach (var combination in combinations)
        {
            PayLine newPayLine = new PayLine();
            newPayLine.line = combination;
            //GetComponent<SlotController>().payTable.Add(newPayLine);
            PayLineList.Add(newPayLine);

        }
    }



    [System.Serializable]
    public class ClassMatchPattern
    {
        public int[] Pattern;
    }
}

public enum Mode
{

    ThreeXThree, FourXFive, FiveXFive,

}
