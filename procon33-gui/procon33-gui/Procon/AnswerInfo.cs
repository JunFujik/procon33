using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon33_gui.Procon
{
    internal class AnswerInfo
    {

        internal AnswerInfo(string problemId, string [] answers, DateTime acceptedAt)
        {
            ProblemId = problemId;
            Answers = answers;
            AcceptedAt = acceptedAt;
        }

        internal string ProblemId
        {
            get;
            private set;
        }

        internal string[] Answers
        {
            get;
            private set;
        }

        internal DateTime AcceptedAt
        {
            get;
            private set;
        }
    }
}
