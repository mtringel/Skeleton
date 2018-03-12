using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class AppDb : ConfigSection
    {
        internal AppDb()
            : base(null)
        {
        }

        public int DecimalPrecision = 18;

        public int DecimalScale = 3;
    }

}