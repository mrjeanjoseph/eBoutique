using System.Collections.Generic;

namespace GetMinMaxValue {
    public static class Extensions {
        public static int findMin(this IList<int> items) {
            int minVal = int.MaxValue;
            foreach (int i in items) {
                if (i < minVal) {
                    minVal = i;
                }
            }
            return minVal;
        }

        public static int findMax(this IList<int> items) {
            int maxVal = int.MinValue;
            foreach (int i in items) {
                if (i > maxVal) {
                    maxVal = i;
                }
            }
            return maxVal;
        }
    }
}
