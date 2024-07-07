using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    void Start()
    {
        MakeSmallest();
    }

    [SerializeField] int size = 1;
    private enum Stitch
    {
        DEC,
        SC,
        INC,
        MC,
        CLO
    }
    private List<List<Stitch>> stitches = new List<List<Stitch>>();

    private void MakeSmallest()
    {
        List<Stitch> firstRow = new List<Stitch>() { Stitch.MC, Stitch.MC, Stitch.MC, Stitch.MC, Stitch.MC, Stitch.MC };
        stitches.Add(firstRow);
        
        List<Stitch> secondRow = new List<Stitch>() { Stitch.INC, Stitch.INC, Stitch.INC, Stitch.INC, Stitch.INC, Stitch.INC };
        stitches.Add(secondRow);

        List<Stitch> middleRow = new List<Stitch>();
        for (int i = 0; i < 12; i++)
        {
            middleRow.Add(Stitch.SC);
        }

        stitches.Add(middleRow);
        stitches.Add(middleRow);
        stitches.Add(middleRow);

        List<Stitch> secondToLastRow = new List<Stitch>() { Stitch.DEC, Stitch.DEC, Stitch.DEC, Stitch.DEC, Stitch.DEC, Stitch.DEC };
        stitches.Add(secondToLastRow);

        List<Stitch> lastRow = new List<Stitch>() { Stitch.CLO, Stitch.CLO, Stitch.CLO, Stitch.CLO, Stitch.CLO, Stitch.CLO };
        stitches.Add(lastRow);

        PrintPattern();
    }

    public void PrintPattern()
    {
        Debug.Log("Total rows: " + stitches.Count);
        for (int i = 0; i < stitches.Count; i++)
        {
            Debug.Log("Row " + (i + 1) + ": " + string.Join(", ", stitches[i]) + "(" + stitches[i].Count + ")");
        }
    }

    public void Increment()
    {
        int lastIncRow = FindLastIncRowByIndex();
        //remove all of the middle rows since they will all be updated
        for (int i = 1; i < (3 + size); i++)
        {
            stitches.RemoveAt(lastIncRow + 1);
        }

        //add the new increase row
        List<Stitch> newIncRow = new List<Stitch>(stitches[lastIncRow]);
        for (int i = 0; i < stitches[lastIncRow].Count + 6; i++)
        {
            if(newIncRow[i] == Stitch.INC) newIncRow.Insert(i + 1, Stitch.SC);
        }
        stitches.Insert(lastIncRow + 1, newIncRow);

        //add the new middle rows
        int middleCount = FindStitchCount(newIncRow);
        for (int i = 0; i < 3 + size; i++)
        {
            List<Stitch> middleRow = new List<Stitch>();
            for (int j = 0; j < middleCount; j++)
            {
                middleRow.Add(Stitch.SC);
            }
            stitches.Insert(lastIncRow + 2 + i, middleRow);
        }

        //add the first new decrease row
        int firstDecRow = 5 + size * 2;
        List<Stitch> newDecRow = new List<Stitch>(stitches[firstDecRow]);
        for (int i = 0; i < stitches[firstDecRow].Count + 6; i++)
        {
            if (newDecRow[i] == Stitch.DEC) newDecRow.Insert(i + 1, Stitch.SC);
        }
        stitches.Insert(firstDecRow, newDecRow);

        size++;
        gameObject.transform.localScale = new Vector3(size, size, size);
    }

    public void Decrement()
    {
        size--;
        gameObject.transform.localScale = new Vector3(size, size, size);
    }

    private int FindStitchCount(List<Stitch> stitches)
    {
        int total = 0;
        for (int i = 0;i < stitches.Count; i++) {
            switch (stitches[i])
            {
                case Stitch.DEC:
                    total++;
                    break;
                case Stitch.SC:
                    total++;
                    break;
                case Stitch.INC:
                    total += 2;
                    break;
                case Stitch.MC:
                    total++;
                    break;
                case Stitch.CLO:
                    total++;
                    break;
            }
        }
        return total;
    }

    //private int FindLastIncRowByIndex()
    //{
    //    return 7 + 3 * (size - 1);
    //}

    private int FindLastIncRowByIndex()
    {
        return size;
    }
}
