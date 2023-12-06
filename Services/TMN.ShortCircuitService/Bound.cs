using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public struct Bound
    {
        public Bound(double min, double max)
            : this()
        {
            this.Minimum = min;
            this.Maximum = max;
        }

        public double Minimum
        {
            get;
            set;
        }

        public double Maximum
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0:000}{1:000}", Minimum * 10, Maximum * 10);
        }

        public static Bound Ignore = new Bound();
    }
}
