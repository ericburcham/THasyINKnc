using System.Collections.Generic;
using System.Threading;

namespace ParallelLoops.Models
{
    internal class Parent
    {
        public Parent()
        {
            Children = new List<Child>();
        }

        public string Name { get; set; }

        public List<Child> Children { get; set; }

        public int Id { get; set; }

        public int SumOfChildIds { get; private set; }

        public void AddChildId(int childId)
        {
            var temp = SumOfChildIds;
            Thread.Sleep(1);
            temp += childId;
            Thread.Sleep(1);
            SumOfChildIds = temp;
        }
    }
}