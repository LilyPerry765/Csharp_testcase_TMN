using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public interface IService
    {
        void Stop();

        void Start(string[] args);
    }
}
