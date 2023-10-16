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
            // Initialize an instance of the shared recognizer.  
            using (SpeechRecognizer recognizer = new SpeechRecognizer())
            {
                // Create and load a sample grammar.  
                Grammar testGrammar = CreateColorGrammar();
                testGrammar.Name = "Test Grammar";
                recognizer.LoadGrammar(testGrammar);

                // Attach event handlers for recognition events.  
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizedHandler);

                Console.ReadLine();

            }
        }

        public static Grammar CreateColorGrammar()
        {
            // Create a set of color choices.  
            Choices azureGroupChoices = new Choices(new string[] { "devops", "pipelines", "boards", "repos", "artificacts" });
            GrammarBuilder colorElement = new GrammarBuilder(azureGroupChoices);

            // Create grammar builders for the two versions of the phrase.  
            GrammarBuilder azureCommand = new GrammarBuilder(new Choices(new string[] { "azure", "a z"}));
            azureCommand.Append(azureGroupChoices);
            GrammarBuilder quitPhrase = new GrammarBuilder("Quit");

            // Create a Choices for the two alternative phrases, convert the Choices  
            // to a GrammarBuilder, and construct the grammar from the result.  
            Choices bothChoices = new Choices(new GrammarBuilder[] { azureCommand, quitPhrase });
            Grammar grammar = new Grammar((GrammarBuilder)bothChoices);
            grammar.Name = "Commands";
            return grammar;
        }

        // Handle the SpeechRecognized event.  
        public static void SpeechRecognizedHandler(object? sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)
            {
                var str = e.Result.Text;

                if (e.Result.Text.Contains("a z"))
                {
                    str = e.Result.Text.Replace("a z", "az");
                }

                Console.WriteLine("Recognition result = {0}", str ?? "<no text>");

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