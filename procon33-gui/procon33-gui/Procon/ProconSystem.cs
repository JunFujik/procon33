using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IO;


namespace procon33_gui.Procon
{
    internal class ProconSystem
    {
        static readonly string ProconScriptFileName = "procon33_system.py";

        string m_host;
        string m_token;
        string m_scriptPath;
        string m_pythonCommand;
        string m_pythonArg;
        bool m_useHttps;

        internal ProconSystem(string host, string token, string script)
        {
            m_host = host;
            m_token = token;
            m_scriptPath = script;
        }

        internal ProconSystem(Config config)
        {
            m_host = config.ProconHost;
            m_token = config.ProconToken;
            m_scriptPath = Path.Combine(config.ScriptsPath, ProconScriptFileName);
            m_pythonCommand = config.PythonCommand;
            m_pythonArg = config.PythonArgument;
            m_useHttps = config.UseHttps;
        }

        internal ProconError TryGetMatch(out MatchInfo outMatchInfo)
        {
            Process p = new Process()
            {
                StartInfo =
                {
                    FileName = m_pythonCommand,
                    Arguments = $"{m_pythonArg} {m_scriptPath} --token {m_token} --host {RootURL} match",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            var match = Regex.Matches(output, "(?<key>.+?)=(?<value>.+?)[;$\r\n]");
            Dictionary<string, string> response = match.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);

            string result = response["result"];
            if (!result.Equals("success"))
            {
                outMatchInfo = null;
                return ResultToError(result);
            }

            if (response.ContainsKey("correct_point"))
            {
                int problems = int.Parse(response["problems"]);
                decimal[] bonus_factor = ParseArray<decimal>(response["bonus_factor"]);
                decimal changePenalty = decimal.Parse(response["change_penalty"]);
                decimal wrongPenalty = decimal.Parse(response["wrong_penalty"]);
                decimal correctPenalty = decimal.Parse(response["correct_point"]);
                outMatchInfo = new MatchInfo(problems, bonus_factor, changePenalty, wrongPenalty, correctPenalty);
            }
            else
            {
                int problems = int.Parse(response["problems"]);
                decimal[] bonus_factor = ParseArray<decimal>(response["bonus_factor"]);
                decimal penalty = decimal.Parse(response["penalty"]);
                outMatchInfo = new MatchInfo(problems, bonus_factor, penalty, penalty, penalty);
            }
            
            return ProconError.Success;
        }

        internal ProconError TryGetProblem(out ProblemInfo outProblemInfo)
        {
            Process p = new Process()
            {
                StartInfo =
                {
                    FileName = m_pythonCommand,
                    Arguments = $"{m_pythonArg} {m_scriptPath} --token {m_token} --host {RootURL} problem",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            var match = Regex.Matches(output, "(?<key>.+?)=(?<value>.+?)[;$\r\n]");
            Dictionary<string, string> response = match.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);

            string result = response["result"];
            if (!result.Equals("success"))
            {
                outProblemInfo = null;
                return ResultToError(result);
            }

            if (response.ContainsKey("start_at"))
            {
                outProblemInfo = new ProblemInfo(
                    response["id"],
                    int.Parse(response["chunks"]),
                    DateTimeOffset.FromUnixTimeSeconds(long.Parse(response["start_at"])).LocalDateTime,
                    int.Parse(response["time_limit"]),
                    int.Parse(response["data"]));
            }
            else
            {
                outProblemInfo = new ProblemInfo(
                    response["id"],
                    int.Parse(response["chunks"]),
                    DateTimeOffset.FromUnixTimeSeconds(long.Parse(response["starts_at"])).LocalDateTime,
                    int.Parse(response["time_limit"]),
                    int.Parse(response["data"]));
            }
            return ProconError.Success;
        }


        internal ProconError AnswerEhuda(out AnswerInfo answerInfo, string[] ehuda, ProblemInfo problem)
        {
            string answerData = string.Join(" ", ehuda);

            Process p = new Process()
            {
                StartInfo =
                {
                    FileName = m_pythonCommand,
                    Arguments = $"{m_pythonArg} {m_scriptPath} --token {m_token} --host {RootURL} --data {answerData} --problem {problem.ProblemId} answer",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            var match = Regex.Matches(output, "(?<key>.+?)=(?<value>.+?)[;$\r\n]");
            Dictionary<string, string> response = match.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);

            string result = response["result"];
            if (!result.Equals("success"))
            {
                answerInfo = null;
                return ResultToError(result);
            }


            answerInfo = new AnswerInfo(
                response["problem_id"],
                ParseArray<string>(response["answers"]),
                DateTimeOffset.FromUnixTimeSeconds(long.Parse(response["accepted_at"])).LocalDateTime);
            return ProconError.Success;
        }

        internal ProconError DownloadChunks(out List<string> outChunks, int numChunks)
        {
            Process p = new Process()
            {
                StartInfo =
                {
                    FileName = m_pythonCommand,
                    Arguments = $"{m_pythonArg} {m_scriptPath} --token {m_token} --host {RootURL} --num {numChunks} chunk",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            var match = Regex.Matches(output, "(?<key>.+?)=(?<value>.+?)[;$\r\n]");
            Dictionary<string, string> response = match.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);

            string result = response["result"];
            if (!result.Equals("success"))
            {
                outChunks = null;
                return ResultToError(result);
            }

            List<string> chunkPaths;
            var getChunksError = GetChunks(out chunkPaths, ParseArray<string>(response["chunks"]));

            outChunks = chunkPaths;
            return getChunksError;
        }

        ProconError GetChunks(out List<string> outChunkPaths, IEnumerable<string> chunkNames)
        {
            List<string> chunkPaths = new List<string>();
            foreach (var chunkname in chunkNames)
            {
                Process getProcess = new Process()
                {
                    StartInfo =
                    {
                        FileName = m_pythonCommand,
                        Arguments = $"{m_pythonArg} {m_scriptPath} --token {m_token} --host {RootURL} --filename {chunkname} get",
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    }
                };
                getProcess.Start();

                string output = getProcess.StandardOutput.ReadToEnd();
                var match = Regex.Matches(output, "(?<key>.+?)=(?<value>.+?)[;$\r\n]");
                Dictionary<string, string> response = match.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);

                string result = response["result"];
                if (!result.Equals("success"))
                {
                    outChunkPaths = null;
                    return ResultToError(result);
                }

                chunkPaths.Add(response["path"]);
            }

            outChunkPaths = chunkPaths;
            return ProconError.Success;
        }

        T[] ParseArray<T>(string arrayStr)
        {
            string[] elementsStr = arrayStr.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');

            if (typeof(T) == typeof(int))
            {
                return elementsStr.Select(x => int.Parse(x)).ToArray() as T[];
            }
            else if(typeof(T) == typeof(double))
            {
                return elementsStr.Select(x => double.Parse(x)).ToArray() as T[];
            }
            else if (typeof(T) == typeof(decimal))
            {
                return elementsStr.Select(x => decimal.Parse(x)).ToArray() as T[];
            }
            else if(typeof(T) == typeof(string))
            {
                return elementsStr.ToArray() as T[];
            }

            throw new ArgumentException();
        }

        ProconError ResultToError(string result)
        {
            foreach(var name in typeof(ProconError).GetEnumNames())
            {
                if(result.Equals(name))
                {
                    return (ProconError)Enum.Parse(typeof(ProconError), name, true);
                }
            }

            return ProconError.Error;
        }

        internal ProconError Test()
        {
            Process p = new Process()
            {
                StartInfo =
                {
                    FileName = m_pythonCommand,
                    Arguments = $"{m_pythonArg} {m_scriptPath} --token {m_token} --host {RootURL} test",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            var match = Regex.Matches(output, "(?<key>.+?)=(?<value>.+?)[;$\r\n]");
            Dictionary<string, string> response = match.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);

            string result = response["result"];
            if (!result.Equals("success"))
            {
                return ResultToError(result);
            }

            return ProconError.Success;
            
        }

        private string RootURL
        {
            get
            {
                return m_useHttps ? $"https://{m_host}/" : $"http://{m_host}/";
            }
        }
    }
}
