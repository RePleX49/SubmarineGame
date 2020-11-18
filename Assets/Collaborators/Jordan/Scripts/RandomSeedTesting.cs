using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class RandomSeedTesting : MonoBehaviour
{
    private int[] noiseValues;

    public string seed;

    private int seedParsed;

    private List<int> usedRands = new List<int>();

    void Start()
    {
        Debug.Log("Seed as Give " + seed);

        seedParsed = IntParseASCII(seed);

        Debug.Log("Seed as Parsed " + seedParsed);

        UnityEngine.Random.InitState(seedParsed);

        noiseValues = new int[4];

        Debug.Log("Random Values: ");

        for (int i = 0; i < noiseValues.Length; i++)
        {
            bool isValid = false;

            noiseValues[i] = UnityEngine.Random.Range(1, 7);

            while (!isValid)
            {
                if (Find(noiseValues[i], usedRands))
                {
                    noiseValues[i]++;
                    noiseValues[i]  = noiseValues[i] % noiseValues.Length;
                }
                else
                {
                    isValid = true;
                    usedRands.Add(noiseValues[i]);

                }


            }
            //foreach (int val in usedRands)
            //{
            //    if (val == noiseValues[i])
            //    {

            //    }
            //}





            //for (int j = 0; j < i; j++) {

            //    while (noiseValues[i] == noiseValues[j])
            //    {
            //        noiseValues[i]++;
            //    }

            //}

            Debug.Log(noiseValues[i]);
        }
    }


    private int IntParseASCII (string value)
    {
        int result = 0;
        byte[] ba = Encoding.Default.GetBytes(value);
        var hexString = BitConverter.ToString(ba);
        hexString = hexString.Replace("-", "");

        Debug.Log("ASCIIConvert: " + hexString);

        //result = int.Parse(hexString);
        result = IntParseFast(hexString);

        return result;
    }


    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }

    
    private bool Find (int val, List<int> tar)
    {

        foreach (int item in tar)
        {
            if (item == val)
            {
                return true;
            }
        }
        return false;
    }
}
