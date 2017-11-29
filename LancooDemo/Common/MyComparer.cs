using LancooDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LancooDemo.Common
{
    class MyComparer : IEqualityComparer<Recommend_ResouceModel>
    {
        public bool Equals(Recommend_ResouceModel x, Recommend_ResouceModel y)
        {
            return x.SubjectID == y.SubjectID && x.UserID == y.UserID && x.ResourceID == y.ResourceID;
        }

        public int GetHashCode(Recommend_ResouceModel obj)
        {
            return obj.SubjectID.GetHashCode() ^ obj.UserID.GetHashCode() ^ obj.ResourceID.GetHashCode();
        }
    }
}