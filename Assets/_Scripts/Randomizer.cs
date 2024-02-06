using UnityEngine;


namespace Akadus
{
    public class Randomizer : MonoBehaviour
    {
        //public static int GenerateGold()
        //{
        //    float randomValue = Random.value; // Get a random value between 0 and 1

        //    if (randomValue <= 0.20f)
        //    {
        //        // 20% chance to get 1 gold
        //        return 1;
        //    }
        //    else if (randomValue <= 0.50f)
        //    {
        //        // 30% chance to get 2 gold
        //        return 2;
        //    }
        //    else if (randomValue <= 0.70f)
        //    {
        //        // 20% chance to get 3 gold
        //        return 3;
        //    }
        //    else if (randomValue <= 0.80f)
        //    {
        //        // 10% chance to get 4 gold
        //        return 4;
        //    }
        //    else
        //    {
        //        // 20% chance to get 5 gold
        //        return 5;
        //    }
        //}


        public static int GenerateGem()
        {
            float randomValue = Random.value; // Get a random value between 0 and 1

            if (randomValue <= 0.60f)
            {
                return 1;
            }
            else if (randomValue >= 0.60f)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

    }


}
