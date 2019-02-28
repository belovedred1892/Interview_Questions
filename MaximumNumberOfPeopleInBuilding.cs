// Google phone interview 02/25/2019 11:00AM - 11:45AM
using System;
using System.Collections.Generic;
using System.Linq;

public class MaximumNumberOfPeopleInBuilding
{
    public Result FindMaximum(DateTime[,] entries)
    {
        if (entries == null || entries.GetLength(0) == 0)
        {
            return null;
        }

        DateTime[] enterArr = new DateTime[entries.GetLength(0)];
        DateTime[] leavingArr = new DateTime[entries.GetLength(0)];

        for (int k = 0 ; k < entries.GetLength(0); k++)
        {
            enterArr[k] = entries[k, 0];
            leavingArr[k] = entries[k, 1];
        }

        Array.Sort(enterArr);
        Array.Sort(leavingArr);

        int i = 0, j = 0;
        int maxNumber = 0;
        var maxTimePeriods = new List<(DateTime start, DateTime end)>();
        while (i < enterArr.Length)
        {
            if (enterArr[i] < leavingArr[j])
            {
                int curNumber = i - j + 1;
                if (curNumber > maxNumber)
                {
                    maxTimePeriods = new List<(DateTime start, DateTime end)>
                    {
                        (enterArr[i], leavingArr[j])
                    };
                }
                else if (curNumber == maxNumber)
                {
                    if (maxTimePeriods.Last().end == enterArr[i])
                    {
                        maxTimePeriods[maxTimePeriods.Count - 1] = (maxTimePeriods[maxTimePeriods.Count - 1].start, leavingArr[j]);
                    }
                    else
                    {
                        maxTimePeriods.Add((enterArr[i], leavingArr[j]));
                    }
                }

                i++;
            }
            else
            {
                j++;
            }
        }

        return new Result
        {
            maxNumber = maxNumber,
            timePeriods = maxTimePeriods
        };
    }

    public class Result
    {
        public int maxNumber;
        public List<(DateTime start, DateTime end)> timePeriods;
    }
}