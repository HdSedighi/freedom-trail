using System;
using System.Collections.Generic;

class Solution
{
    public int FindRotateSteps(string ring, string key)
    {
        int ringLen = ring.Length;
        int keyLen = key.Length;

        // Dictionary to map characters to their positions in the ring
        Dictionary<char, List<int>> charToPos = new Dictionary<char, List<int>>();
        for (int i = 0; i < ringLen; i++)
        {
            char c = ring[i];
            if (!charToPos.ContainsKey(c))
            {
                charToPos[c] = new List<int>();
            }
            charToPos[c].Add(i);
        }

        // DP array
        int[,] dp = new int[keyLen + 1, ringLen];
        for (int i = 0; i <= keyLen; i++)
        {
            for (int j = 0; j < ringLen; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }
        dp[0, 0] = 0;

        // Iterate through each character in key
        for (int i = 0; i < keyLen; i++)
        {
            char keyChar = key[i];
            if (!charToPos.ContainsKey(keyChar)) continue;

            // Iterate through positions of the current key character in the ring
            foreach (int pos in charToPos[keyChar])
            {
                // Iterate through possible starting positions in the ring
                for (int j = 0; j < ringLen; j++)
                {
                    if (dp[i, j] == int.MaxValue) continue; // Skip if previous state is unreachable

                    // Calculate rotation distance from current position j to target position pos
                    int clockwise = Math.Abs(pos - j);
                    int counterclockwise = ringLen - clockwise;
                    int minDist = Math.Min(clockwise, counterclockwise);

                    // Update dp[i + 1][pos]
                    dp[i + 1, pos] = Math.Min(dp[i + 1, pos], dp[i, j] + minDist + 1);
                }
            }
        }

        // Find the minimum steps from the last character in the key
        int minSteps = int.MaxValue;
        for (int j = 0; j < ringLen; j++)
        {
            minSteps = Math.Min(minSteps, dp[keyLen, j]);
        }

        return minSteps;
    }

    public static void Main(string[] args)
    {
        string ring = "godding";
        string key = "gd";
        Solution solution = new Solution();
        int result = solution.FindRotateSteps(ring, key);
        Console.WriteLine(result); // Output: 4
    }
}
