using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon33_gui.Procon
{
    internal class ProblemInfo
    {
        internal ProblemInfo(string problemId, int numChunks, DateTime startTime, int timeLimit, int numData)
        {
            ProblemId = problemId;
            NumChunks = numChunks;
            StartTime = startTime;
            EndTime = startTime.AddSeconds(timeLimit);
            NumData = numData;
        }

        internal string ProblemId
        {
            get;
            private set;
        }

        internal int NumChunks
        {
            get;
            private set;
        }

        internal DateTime StartTime
        {
            get;
            private set;
        }

        internal DateTime EndTime
        {
            get;
            private set;
        }

        internal int NumData
        {
            get;
            private set;
        }
    }
}
