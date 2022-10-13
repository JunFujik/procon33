using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon33_gui.Forms
{
    internal partial class ConfigForm : Form
    {
        internal ConfigForm()
        {
            InitializeComponent();
        }

        internal void LoadConfig(Config config)
        {
            SystemURLTextBox.Text = config.ProconHost;
            TokenTextBox.Text = config.ProconToken;
            ScriptsPathTextBox.Text = config.ScriptsPath;
            PythonCommandTextBox.Text = config.PythonCommand;
            PythonArgumentTextBox.Text = config.PythonArgument;
            JkspeechPathTextBox.Text = config.JkspeechPath;
        }

        public void SaveConfig(ref Config config)
        {
            config.ProconHost = SystemURLTextBox.Text;
            config.ProconToken = TokenTextBox.Text;
            config.ScriptsPath = ScriptsPathTextBox.Text;
            config.PythonCommand = PythonCommandTextBox.Text;
            config.PythonArgument = PythonArgumentTextBox.Text;
            config.JkspeechPath = JkspeechPathTextBox.Text;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
