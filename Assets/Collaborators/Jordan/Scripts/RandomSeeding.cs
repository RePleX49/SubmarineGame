using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class RandomSeeding : MonoBehaviour
{
    //private int[] noiseValues;

    public string seed;

    private int seedParsed;

    void Start()
    {
        Systems.randomSeeding = this;
    }

    public int[] SetUpArrayBySeed (int[] target, int min, int max)
    {
        List<int> usedRands = new List<int>();
        int length = target.Length;
        //Debug.Log("Seed as Give " + seed);

        seedParsed = IntParseASCII(seed);

        //Debug.Log("Seed as Parsed " + seedParsed);

        UnityEngine.Random.InitState(seedParsed);

        target = new int[length];

        //Debug.Log("Random Values: ");

        for (int i = 0; i < target.Length; i++)
        {
            bool isValid = false;

            target[i] = UnityEngine.Random.Range(min, max + 1);

            int infStopper = 0;

            while (!isValid && infStopper < 1000)
            {
                if (Find(target[i], usedRands))
                {
                    target[i]++;
                    target[i] = target[i] % target.Length;
                    if (target[i] == 0)
                    {
                        target[i] = 1;
                    }
                }
                else
                {
                    isValid = true;
                    usedRands.Add(target[i]);

                }

                infStopper++;
            }
         
            //Debug.Log(noiseValues[i]);
        }

        return target;
    }


    private int IntParseASCII (string value)
    {
        int result = 0;
        byte[] ba = Encoding.Default.GetBytes(value);
        var hexString = BitConverter.ToString(ba);
        hexString = hexString.Replace("-", "");

        //Debug.Log("ASCIIConvert: " + hexString);

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
