namespace Strings
{
    class SpecialString
    {

        // SA-IS Algorithm For Suffix Array Construction
        public static int[] ConstructSuffixArray(string input)
        {
            int n = input.Length;
            int[] sa = new int[n];
            bool[] isLMS = new bool[n];
            for (int i = 0; i < n; i++)
            {
                isLMS[i] = IsLMS(input, i);
            }

            // stage 1: reduce the problem by identifying the LMS substrings
            int[] buckets = GetBuckets(input);
            for (int i = 0; i < n; i++)
            {
                if (isLMS[i])
                {
                    sa[--buckets[input[i]]] = i;
                }
            }
            InduceSort(input, sa, isLMS, buckets);
            int num = 0;
            int[] lmsMap = new int[n + 1];
            for (int i = 0; i < n; i++)
            {
                if (isLMS[sa[i]])
                {
                    lmsMap[sa[i]] = num++;
                }
            }
            int[] lms = new int[num];
            for (int i = 0, j = 0; i < n; i++)
            {
                if (isLMS[i])
                {
                    lms[j++] = i;
                }
            }

            // stage 2: solve the reduced problem recursively
            int[] sortedLMS = new int[num];
            int prev = -1;
            int name = 0;
            for (int i = 0; i < num; i++)
            {
                int curr = lms[i];
                bool diff = false;
                for (int j = 0; j < n; j++)
                {
                    if (prev == -1 || input[curr + j] != input[prev + j] || isLMS[curr + j] != isLMS[prev + j])
                    {
                        diff = true;
                        break;
                    }
                    else if (j > 0 && (isLMS[curr + j] || isLMS[prev + j]))
                    {
                        break;
                    }
                }
                if (diff)
                {
                    name++;
                    prev = curr;
                }
                sortedLMS[lmsMap[curr]] = name;
            }
            int[] sa1;
            if (name == num)
            {
                sa1 = new int[num];
                for (int i = 0; i < num; i++)
                {
                    sa1[sortedLMS[i]] = i;
                }
            }
            else
            {
                string s1 = new string(sortedLMS.Select(x => input[x]).ToArray());
                sa1 = ConstructSuffixArray(s1);
                for (int i = 0; i < num; i++)
                {
                    sortedLMS[sa1[i]] = i + 1;
                }
            }

            // stage 3: induce the order of the L-type suffixes
            buckets = GetBuckets(input);
            for (int i = n - 1; i >= 0; i--)
            {
                if (!isLMS[i])
                {
                    sa[--buckets[input[i]]] = i;
                }
            }
            InduceSort(input, sa, isLMS, buckets);

            return sa;
        }

        private static void InduceSort(string input, int[] sa, bool[] isLMS, int[] buckets)
        {
            int n = input.Length;
            int[] bucketEnds = new int[256];
            for (int i = 0; i < 256; i++)
            {
                bucketEnds[i] = buckets[i + 1] - 1;
            }
            for (int i = 0; i < n; i++)
            {
                int j = sa[i] - 1;
                if (j >= 0 && !isLMS[j])
                {
                    sa[bucketEnds[input[j]]--] = j;
                }
            }
            for (int i = n - 1; i >= 0; i--)
            {
                int j = sa[i] - 1;
                if (j >= 0 && isLMS[j])
                {
                    sa[--buckets[input[j]]] = j;
                }
            }
        }

        private static int[] GetBuckets(string input)
        {
            int[] buckets = new int[257];
            foreach (char c in input)
            {
                buckets[c + 1]++;
            }
            for (int i = 1; i < 257; i++)
            {
                buckets[i] += buckets[i - 1];
            }
            return buckets;
        }

        private static bool IsLMS(string input, int i)
        {
            if (i > 0 && input[i] > input[i - 1])
            {
                return true;
            }
            if (i == 0)
            {
                return true;
            }
            int j = i;
            while (j > 0 && input[j] == input[j - 1])
            {
                j--;
            }
            return j > 0 && input[j] > input[j - 1];
        }

        public static void Mainn()
        {
            string a = "bac";
            string b = "bac";
            string c = "banana";
            var res = ConstructSuffixArray(c);
            for(int i = 0; i<c.Length; i++){
                System.Console.WriteLine(res[i]);
            }
        }
    }
}