using UnityEngine;

namespace Akadus
{
    public class Randomizer : MonoBehaviour
    {
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
