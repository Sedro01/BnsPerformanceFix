using BnsDatTool;
using BnsDatTool.lib;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnsPerformanceFix
{
    public class Program
    {
        public class Options
        {
            [Option('f', "filter", Required = true, HelpText = "Path to filter")]
            public string FilterPath { get; set; }

            [Option('o', "outfile", Required = false, HelpText = "Path to output")]
            public string OutputPath { get; set; }

            [Value(0, Required = true, HelpText = "Path to local[64].dat or localfile[64].bin")]
            public string InputPath { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                var filter = default(Filter);
                using (var stream = new FileStream(o.FilterPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    filter = Filter.Load(stream);
                }

                var output = o.OutputPath;
                if (output == null)
                {
                    var datFilename = Path.GetFileNameWithoutExtension(o.InputPath);
                    var filterFilename = Path.GetFileNameWithoutExtension(o.FilterPath);
                    var extension = Path.GetExtension(o.InputPath);
                    output = Path.Combine(Path.GetDirectoryName(o.InputPath), $"{datFilename}-{filterFilename}{extension}");
                }

                RunInTemp(o.InputPath, output, filter);
            });
        }

        public static void RunInTemp(string input, string output, Filter filter)
        {
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            try
            {
                var temp = Path.Combine(tempDir, Path.GetFileName(input));
                File.Copy(input, temp);
                if (Path.GetExtension(temp) == ".bin")
                {
                    BnsPerformanceFix.FilterLocalBinInPlace(temp, filter);
                }
                else
                {
                    BnsPerformanceFix.FilterLocalDatInPlace(temp, filter);
                }
                File.Copy(temp, output, overwrite: true);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}
