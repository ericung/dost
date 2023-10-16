using System;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Threading;

namespace SharedRecognizer
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Program
    {
        public static void Main(string[] args)
        {
            using (SpeechRecognizer recognizer = new SpeechRecognizer())
            {
                Grammar azGrammar = CreateAzGrammar();
                azGrammar.Name = "AZ Grammar";
                recognizer.LoadGrammar(azGrammar);

                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizedHandler);

                Console.ReadLine();

            }
        }

        public static Grammar CreateAzGrammar()
        {
            // az devops
            Choices azureGroupChoices = new Choices(new string[] { "devops", "pipelines", "boards", "repos", "artificacts" });
            Choices azureSubgroupsChoices = new Choices(new string[] { "admin", "extension", "project", "security", "service-endpoint", "team", "user", "wiki"});
            Choices azureCommandsChoices = new Choices(new string[] { "configure", "feedback", "invoke", "login", "logout" });

            // az costmanagement
            Choices azureCostManagementChoices = new Choices(new string[] { "costmanagement" });
            Choices azureCostManagementExportChoices = new Choices(new string[] { "export" });

            // windows powershell commands
            Choices powershellCommands = new Choices(new string[] { "c d" });

            // ubuntu linux bash commands
            Choices ubuntuCommands = new Choices(new string[] { "c d" });

            GrammarBuilder azureGroupGrammar = new GrammarBuilder(azureGroupChoices);
            azureGroupGrammar.Append(azureSubgroupsChoices);
            GrammarBuilder azureCostManagementGrammarBuilder = new GrammarBuilder(azureCostManagementChoices);
            azureCostManagementGrammarBuilder.Append(azureCostManagementExportChoices);
            GrammarBuilder azureCommand = new GrammarBuilder(new Choices(new string[] { "azure", "a z"}));
            azureCommand.Append(azureGroupGrammar);
            azureCommand.Append(azureCommandsChoices);
            azureCommand.Append(azureCostManagementGrammarBuilder);
            GrammarBuilder quitPhrase = new GrammarBuilder("Quit");

            Choices bothChoices = new Choices(new GrammarBuilder[] { azureCommand, quitPhrase });
            Grammar grammar = new Grammar((GrammarBuilder)bothChoices);
            grammar.Name = "Commands";
            return grammar;
        }

        public static void SpeechRecognizedHandler(object? sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)
            {
                var command = e.Result.Text;

                switch(command)
                {
                    case "a z":
                        command = command.Replace("a z", "az");
                        break;
                    case "c d":
                        command = command.Replace("c d", "cd");
                        command = command.Replace("C D", "CD");
                        break;
                    default:
                        break;
                }

                Console.WriteLine("{0}", command ?? "<no text>");

                if (e.Result.Text == "Quit")
                {
                }
            }
            else
            {
                Console.WriteLine("No recognition result");
            }
        }
    }
}