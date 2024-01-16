using System.Collections.Generic;

namespace Utilities
{
    public static class DynamicObjectUtils
    {
        public static List<T> TypedList<T>(dynamic dynamicList)
        {
            List<T> resList = new List<T>();
            if (dynamicList != null)
                resList = dynamicList.ToObject<List<T>>();

            return resList;
        }

        public static T ToObject<T>(dynamic obj)
        {
            return obj.ToObject<T>();
        }
    }
}