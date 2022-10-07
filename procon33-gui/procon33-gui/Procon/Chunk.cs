using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace procon33_gui.Procon
{
    internal class DownloadedChunks
    {
        Dictionary<int, string> m_chunks = new Dictionary<int, string>();

        internal DownloadedChunks(string[] chunks)
        {
            foreach(var chunkPath in chunks)
            {
                string chunkFileName = Path.GetFileName(chunkPath);
                var match = Regex.Match(chunkFileName, @"problem(?<pos>%d+)_(<?<digest>[a-z0-9]+\.wav)");
                int chunkPos = int.Parse(match.Groups["pos"].Value);
                string chunkDigest = match.Groups["digest"].Value;
                m_chunks.Add(chunkPos, chunkPath);
            }
        }

        internal string[] GetRawChunkPaths()
        {
            return m_chunks.Values.ToArray();
        }
    }
}
