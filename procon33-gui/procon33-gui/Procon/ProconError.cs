using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon33_gui.Procon
{
    internal enum ProconError
    {
        Success,

        Error,
        BadGateway,
        AccessTimeError,
        FormatError,
        InvalidToken,
        TooLargeRequestError, 
        NotFound
    }

    static class ProconErrorExtension
    {
        static internal string ToString(this ProconError error)
        {
            return typeof(ProconError).GetEnumName(error);
        }
    }
}
