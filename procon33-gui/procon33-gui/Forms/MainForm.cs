using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using procon33_gui.Procon;
using System.IO;
using System.Diagnostics;
using System.Media;

namespace procon33_gui.Forms
{
    public partial class MainForm : Form
    {
        static readonly string ConfigFile = "config.xml";

        ProconSystem procon;
        MatchInfo m_match;
        ProblemInfo m_problem;
        Timer m_timer;
        Config m_config;
        List<Task<IEnumerable<string>>> m_isolationQueue = new List<Task<IEnumerable<string>>>();

        public MainForm()
        {
            InitializeComponent();

            DataTable ehuda =   new DataTable();
            for(int i = 0; i < 10; i++)
            {
                ehuda.Columns.Add("");

            }

            for (int i = 0; i <= 4; i++)
            {
                var ehudaIds = new List<string>();
                for(int j = 1; j <= 10; j++)
                {
                    int id = i * 10 + j;
                    ehudaIds.Add(id.ToString("d02"));

                    if (id == 44)
                        break;
                }
                ehuda.Rows.Add(ehudaIds.ToArray());
            }
            PreAnswerEhuda.DataSource = ehuda;
            PreAnswerEhuda.RowHeadersVisible = false;
            PreAnswerEhuda.ColumnHeadersVisible = false;
            foreach(DataGridViewColumn column in PreAnswerEhuda.Columns)
            {
                column.Width = 40;
            }

            m_timer = new Timer()
            {
                Interval = 10
            };
            m_timer.Tick += (o, e) =>
            {
                if (m_problem != null)
                {
                    var remainingTime = (int)(m_problem.EndTime - DateTime.Now).TotalSeconds;
                    RemainingTimeLabel.Text = $"残り{remainingTime}秒";
                }
                foreach(var isolQueue in m_isolationQueue)
                {
                    if (!isolQueue.IsCompleted)
                        return;

                    var filepaths = isolQueue.Result;
                    foreach(var filepath in filepaths)
                    {
                        AddChunkData(filepath);
                    }
                }
            };
            m_timer.Start();
        }

        private void ConfigChanged()
        {
            procon = new ProconSystem(m_config);
        }

        private void MatchInfoButton_Click(object sender, EventArgs e)
        {
            MatchInfo matchInfo;
            var result = procon.TryGetMatch(out matchInfo);

            if(result != ProconError.Success)
            {
                MessageBox.Show($"試合情報を取得できませんでした。" + 
                    $"エラー:{result}");
                return;
            }

            SetMatchInfo(matchInfo);

            ProblemInfoButton.PerformClick();
        }

        private void SetMatchInfo(MatchInfo matchInfo)
        {
            StringBuilder labelText = new StringBuilder();
            labelText.AppendLine($"問題数: {matchInfo.NumProblems}");
            labelText.AppendFormat("ボーナス係数: {0} {1}", string.Join(", ", matchInfo.BonusFactor.Select(x => x.ToString()).ToArray()), Environment.NewLine);
            labelText.AppendLine($"正解札　得点: {matchInfo.CorrectPoint}");
            labelText.AppendLine($"お手付き　減点: {matchInfo.WrongPenalty}");
            labelText.AppendLine($"変更札　減点: {matchInfo.ChangePenalty}");
            MatchInfoLabel.Text = labelText.ToString();

            m_match = matchInfo;
        }

        private void SetProblemInfo(ProblemInfo problemInfo)
        {
            m_problem = problemInfo;

            StringBuilder labelText = new StringBuilder();
            labelText.AppendLine($"問題ID: {problemInfo.ProblemId}");
            labelText.AppendLine($"重ね合わせ数: {problemInfo.NumData}");
            labelText.AppendLine($"分割データ数: {problemInfo.NumChunks}");
            labelText.AppendLine($"開始時刻: {problemInfo.StartTime}");
            labelText.AppendLine($"制限時間: {(problemInfo.EndTime - problemInfo.StartTime).TotalSeconds}");
            labelText.AppendLine($"終了時刻: {problemInfo.EndTime}");
            ProblemInfoLabel.Text = labelText.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ProblemInfoButton_Click(object sender, EventArgs e)
        {
            ProblemInfo problemInfo;
            var result = procon.TryGetProblem(out problemInfo);

            if (result != ProconError.Success)
            {
                MessageBox.Show($"問題情報を取得できませんでした。" +
                    $"エラー:{result}");
                return;
            }

            SetProblemInfo(problemInfo);
        }

        private void AnswerButton_Click(object sender, EventArgs e)
        {
            if(m_problem == null)
            {
                MessageBox.Show("問題情報が取得されていないので解答できません");
                return;
            }

            List<string> selectedEhuda = PreAnswerEhuda.SelectedCells
                .OfType<DataGridViewTextBoxCell>()
                .Select(x => (string)x.Value)
                .Select(x =>
                {
                    if (x.Length == 4)
                    {
                        x = x.Substring(1, 2);
                    }
                    return x;
                })
                .OrderBy(x => int.Parse(x))
                .ToList();
            selectedEhuda.Sort();

            StringBuilder message = new StringBuilder();
            message.AppendLine($"以下の{selectedEhuda.Count}枚の絵札で解答しますか？");
            message.AppendLine(string.Join(", ", selectedEhuda));
            var dialogResult = MessageBox.Show(message.ToString(), "", MessageBoxButtons.OKCancel);

            if(dialogResult == DialogResult.Cancel)
            {
                return;
            }

            AnswerInfo answerInfo;
            var answerResult = procon.AnswerEhuda(out answerInfo, selectedEhuda.ToArray(), m_problem);

            if (answerResult != ProconError.Success)
            {
                MessageBox.Show($"解答できませんでした。" +
                    $"エラー:{answerResult}");
                return;
            }

            StringBuilder acceptedMessage = new StringBuilder();
            acceptedMessage.AppendLine("解答は受理されました");
            acceptedMessage.AppendLine($"時刻: {answerInfo.AcceptedAt}");
            acceptedMessage.AppendLine($"問題ID: {m_problem.ProblemId}");
            acceptedMessage.AppendLine($"絵札: {string.Join(" ", answerInfo.Answers)}");
            MessageBox.Show(acceptedMessage.ToString());

            var selectedCells = PreAnswerEhuda.SelectedCells
               .OfType<DataGridViewTextBoxCell>()
               .ToArray();
            foreach (DataGridViewTextBoxCell cell in selectedCells)
            {
                if (((string)cell.Value).Length == 2)
                {
                    cell.Value = "[" + cell.Value + "]";
                }
            }
        }

        private void GetChunkButton_Click(object sender, EventArgs e)
        {
            if(m_problem == null)
            {
                MessageBox.Show("問題データを取得してください");
                return;
            }

            int numChunks = Convert.ToInt32(NumChunkNumericBox.Value);
            if(numChunks <= 0 || numChunks > m_problem.NumChunks)
            {
                MessageBox.Show("取得する分割データ数は1以上から問題で指定された個数までで指定してください");
                return;
            }

            var messageBoxResult = MessageBox.Show($"分割データを{numChunks}個取得しますか？", "", MessageBoxButtons.OKCancel);
            if(messageBoxResult == DialogResult.Cancel)
            {
                return;
            }

            List<string> chunkPaths;
            var result = procon.DownloadChunks(out chunkPaths, numChunks);

            if(result != ProconError.Success)
            {
                MessageBox.Show($"分割データを取得できませんでした　エラー: {result}");
                return;
            }

            foreach(var chunkPath in chunkPaths)
            {
                AddChunkData(chunkPath);
            }

            string scriptPath = Path.Combine(m_config.ScriptsPath, "chain_wave.py");
            Process correlateProcess = new Process()
            {
                StartInfo =
                    {
                        FileName = "py",
                        Arguments = scriptPath,
                        UseShellExecute = false,
                        WorkingDirectory = ".\\data",
                        RedirectStandardOutput = true,
                    }
            };
            correlateProcess.Start();

            var files = correlateProcess.StandardOutput.ReadToEnd().Split(',');
            foreach(var file in files)
            {
                AddChunkData(file);
            }
        }

        void AddChunkData(string wavFilepath)
        {
            var datasource = ChunkDataGridView.DataSource as DataTable;
            if(datasource == null)
            {
                datasource = new DataTable();
                datasource.Columns.Add(new DataColumn("ファイル名")
                {
                    ReadOnly = true
                });
                datasource.Columns.Add(new DataColumn("ファイルパス")
                {
                    ReadOnly = true
                });
            }

            foreach(DataGridViewRow row in ChunkDataGridView.Rows)
            {
                if(wavFilepath == (string)row.Cells["ファイルパス"].Value)
                {
                    return;
                }
            }

            string filename = Path.GetFileName(wavFilepath);
            datasource.Rows.Add(filename, wavFilepath);

            ChunkDataGridView.DataSource = datasource;
        }

        private void CorrelateButton_Click(object sender, EventArgs e)
        {
            List<string> selectedChunks = ChunkDataGridView.SelectedCells
                .OfType<DataGridViewCell>()
                .Select(x => x.OwningRow)
                .Distinct()
                .Select(x => (string)x.Cells["ファイルパス"].Value)
                .ToList();

            foreach (var chunkPath in selectedChunks)
            {
                RunCorrelateScript(chunkPath, false);
            }
        }

        private void FlattenCorrelateButton_Click(object sender, EventArgs e)
        {
            List<string> selectedChunks = ChunkDataGridView.SelectedCells
                .OfType<DataGridViewCell>()
                .Select(x => x.OwningRow)
                .Distinct()
                .Select(x => (string)x.Cells["ファイルパス"].Value)
                .ToList();

            foreach (var chunkPath in selectedChunks)
            {
                RunCorrelateScript(chunkPath, true);
            }
        }

        private void RunCorrelateScript(string filepath, bool flatten)
        {
            string scriptPath = Path.Combine(m_config.ScriptsPath, "search_part.py");
            string args = $"/c py \"{scriptPath}\" \"{filepath}\" --jkspeech \"{m_config.JkspeechPath}\"";

            if (flatten)
            {
                args += " --flatten";
            }

            args += $" & echo target: {filepath} & pause";

            Process correlateProcess = new Process()
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = args
                }
            };
            correlateProcess.Start();
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            var configForm = new ConfigForm();
            configForm.LoadConfig(m_config);
            configForm.ShowDialog();
            configForm.SaveConfig(ref m_config);
            ConfigChanged();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Config config =  new Config();
            if (File.Exists(ConfigFile))
            {
                var loader = File.OpenText(ConfigFile);
                config.Load(loader);
                loader.Dispose();
            }

            m_config = config;
            ConfigChanged();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var writer = File.CreateText(ConfigFile);
            m_config.Save(writer);
            writer.Close();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void ChunkDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PlayDataButton_Click(object sender, EventArgs e)
        {
            List<string> selectedChunks = ChunkDataGridView.SelectedCells
                .OfType<DataGridViewCell>()
                .Select(x => x.OwningRow)
                .Distinct()
                .Select(x => (string)x.Cells["ファイルパス"].Value)
                .ToList();

            if(selectedChunks.Count > 1)
            {
                MessageBox.Show("同時に再生できるデータは一つまでです");
                return;
            }

            if(selectedChunks.Count == 0)
            {
                return;
            }

            SoundPlayer player = new SoundPlayer(selectedChunks.First());
            player.Play();
        }

        private void SoundSeparateButton_Click(object sender, EventArgs e)
        {
            List<string> selectedChunks = ChunkDataGridView.SelectedCells
                .OfType<DataGridViewCell>()
                .Select(x => x.OwningRow)
                .Distinct()
                .Select(x => (string)x.Cells["ファイルパス"].Value)
                .ToList();

            foreach(var chunkDataPath in selectedChunks) {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    RestoreDirectory = true,
                    Filter = "wavファイル|*.wav",
                    CheckPathExists = true,
                    Multiselect = false
                };

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string addWavePath = openFileDialog.FileName;
                AddIsolationQueue(chunkDataPath, addWavePath);
            }
        }

        void AddIsolationQueue(string chunkDataPath, string addWavePath)
        {
            var isolationTask = Task.Run<IEnumerable<string>>(() =>
            {
                string workingDir = "";
                Random random = new Random();
                do
                {
                    workingDir = Path.Combine(Path.GetTempPath(), $"isolation_{random.Next(100000, 999999)}");
                }
                while (Directory.Exists(workingDir));
                Directory.CreateDirectory(workingDir);

                string outpathL = @".\data\" + Path.GetFileNameWithoutExtension(chunkDataPath) + "_" + Path.GetFileNameWithoutExtension(addWavePath) + "1.wav";
                string outpathR = @".\data\" + Path.GetFileNameWithoutExtension(chunkDataPath) + "_" + Path.GetFileNameWithoutExtension(addWavePath) + "2.wav";

                string scriptPath = Path.Combine(m_config.ScriptsPath, "isolation.py");
                string args = $"/c echo chunk: {chunkDataPath} & echo add: {addWavePath} & py \"{scriptPath}\"";

                Process correlateProcess = new Process()
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        Arguments = args,
                        UseShellExecute = false,
                        WorkingDirectory = workingDir,
                        RedirectStandardInput = true,
                    }
                };
                correlateProcess.Start();

                var stdin = correlateProcess.StandardInput;
                stdin.WriteLine(chunkDataPath);
                stdin.WriteLine(addWavePath);

                correlateProcess.WaitForExit();

                File.Move(workingDir + @"\lgm_em_1.wav", outpathL);
                File.Move(workingDir + @"\lgm_em_2.wav", outpathR);

                return new string[] {
                    outpathL, outpathR
                };
            });
                
            m_isolationQueue.Add(isolationTask);
        }

        private void ChunkDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filepaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filepath in filepaths)
                {
                    AddChunkData(filepath);
                }
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            var result = procon.Test();

            if(result == ProconError.Success)
            {
                MessageBox.Show("成功");
            }
            else
            {
                MessageBox.Show("失敗");
            }
        }
    }
}
