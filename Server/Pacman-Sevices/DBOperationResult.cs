using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    public class DBOperationResult
    {
        public enum AddResult
        {
            Success,
            NullObject,
            UnknowFail,
            ExistingRecord
        }
    }
}
