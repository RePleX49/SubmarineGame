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

    void Awake()
    {
        Systems.randomSeeding = this;
        GenerateNewSeed();
    }

    public void GenerateNewSeed()
    {
        char[] alphaArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        List<char> alphabet = new List<char>();
        List<int> digits = new List<int>();

        for (int i = 0; i < alphaArray.Length; i++)
        {
            alphabet.Add(alphaArray[i]);
        }
        for (int i = 0; i < 10; i++)
        {
            digits.Add(i);
            //Debug.Log(i);
        }

        string buildingSeed = "";

        while (buildingSeed.Length < 4)
        {
            int alphaOrNumeric = UnityEngine.Random.Range(0, 2);

            if (alphaOrNumeric == 0)
            {
                int randLetter = UnityEngine.Random.Range(0, alphabet.Count);
                buildingSeed += alphabet[randLetter];
                alphabet.Remove(alphabet[randLetter]);
            }
            else
            {
                int randNumber = UnityEngine.Random.Range(0, digits.Count);
                buildingSeed += digits[randNumber];
                digits.Remove(digits[randNumber]);
            }
        }

        Debug.Log(buildingSeed);
        seed = buildingSeed;
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

    public int[] SetUpArrayBySeed(int[] target, int min, int max, int puzzleTag)
    {


        List<int> usedRands = new List<int>();
        List<int> usedRands2 = new List<int>();
        List<int> usedRands3 = new List<int>();

        for (int i = 0; i < max; i++)
        {
            usedRands.Add(i);
            usedRands2.Add(i);
            usedRands3.Add(i);
        }
        int length = target.Length;
        //Debug.Log("Seed as Give " + seed);

        seedParsed = IntParseASCII(seed);

        //Debug.Log("Seed as Parsed " + seedParsed);

        UnityEngine.Random.InitState(seedParsed);

        target = new int[length];

        int counter = 0;

        //Debug.Log("Random Values: ");

        for (int i = 0; i < target.Length; i++)
        {
            //bool isValid = false;


            //int infStopper = 0;

            if (counter < 2)
            {
                target[i] = usedRands[UnityEngine.Random.Range(0, usedRands.Count)];
                usedRands.Remove(target[i]);
              
            }
            else if (counter < 4)
            {
                target[i] = usedRands2[UnityEngine.Random.Range(0, usedRands2.Count)];
                usedRands2.Remove(target[i]);
            }
            else
            {
                target[i] = usedRands3[UnityEngine.Random.Range(0, usedRands3.Count)];
                usedRands3.Remove(target[i]);
            }
            counter++;
        }

        return target;
    }

    public int[] SetUpArrayBySeed(int[] target, int min, int max, int nonRepeatVal, int nonRepeatOffset, int nonRepeatVal2, int nonRepeatOffset2)
    {

        List<int> usedRands = new List<int>();
        List<int> usedRands2 = new List<int>();
        for (int i = 0; i < max; i++)
        {
            usedRands.Add(i);
            usedRands2.Add(i);
        }
        int length = target.Length;


        seedParsed = IntParseASCII(seed);



        UnityEngine.Random.InitState(seedParsed);

        target = new int[length];


        for (int i = 0; i < target.Length; i++)
        {


            if ((i + nonRepeatOffset) % nonRepeatVal == 0)
            {
                target[i] = usedRands[UnityEngine.Random.Range(0, usedRands.Count)];
                usedRands.Remove(target[i]);
              
            } else if ((i + nonRepeatOffset2) % nonRepeatVal2 == 0)
            {
                target[i] = usedRands2[UnityEngine.Random.Range(0, usedRands2.Count)];
                usedRands2.Remove(target[i]);
            
            }
            else
            {
                target[i] = usedRands[UnityEngine.Random.Range(0, usedRands.Count)];
            }
        }

        return target;
    }


    private int IntParseASCII (string value)
    {
        int result = 0;
        byte[] ba = Encoding.Default.GetBytes(value);
        var hexString = BitConverter.ToString(ba);
        hexString = hexString.Replace("-", "");
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
