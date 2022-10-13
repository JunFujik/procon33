using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace procon33_gui
{
    public class Config
    {
        public Config()
        {
            PythonCommand = "py";
        }

        public void Save(TextWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            serializer.Serialize(writer, this);
        }

        public void Load(TextReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            var loadedConfig = (Config)serializer.Deserialize(reader);

            ProconHost = loadedConfig.ProconHost;
            ScriptsPath = loadedConfig.ScriptsPath;
            ProconToken = loadedConfig.ProconToken;
            PythonCommand = loadedConfig.PythonCommand;
            PythonArgument = loadedConfig.PythonArgument;
            JkspeechPath = loadedConfig.JkspeechPath;
        }

        public string ProconHost
        {
            get;
            set;
        }

        public string ScriptsPath
        {
            get;
            set;
        }

        public string ProconToken
        {
            get;
            set;
        }

        public string PythonCommand
        {
            get;
            set;
        }

        public string PythonArgument
        {
            get;
            set;
        }

        public string JkspeechPath
        {
            get;
            set;
        }
    }
}
