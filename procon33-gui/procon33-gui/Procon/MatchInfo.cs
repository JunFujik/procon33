using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon33_gui.Procon
{
    internal class MatchInfo
    {
        internal MatchInfo(int numProblems, decimal[] bonusFactor, decimal penalty)
        {
            NumProblems = numProblems;
            BonusFactor = bonusFactor;
            Penalty = penalty;
        }

        internal MatchInfo(int numProblems, decimal[] bonusFactor, decimal changePenalty, decimal wrongPenalty, decimal correctPoint)
        {
            NumProblems = numProblems;
            BonusFactor = bonusFactor;
            Penalty = changePenalty;
            ChangePenalty = changePenalty;
            WrongPenalty = wrongPenalty;
            CorrectPoint = correctPoint;
        }

        internal int NumProblems
        {
            get;
            private set;
        }

        internal decimal[] BonusFactor
        {
            get;
            private set;
        }

        internal decimal Penalty
        {
            get;
            private set;
        }

        internal decimal ChangePenalty
        {
            get;
            private set;
        }

        internal decimal WrongPenalty
        {
            get;
            private set;
        }

        internal decimal CorrectPoint
        {
            get;
            private set;
        }
    }
}
