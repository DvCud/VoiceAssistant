using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace VoiceAssistant_v1
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();
        //System.Media.SoundPlayer music = new.System.Media.SoundPlayer();
        public Form1()
        {
            InitializeComponent();

            Choices choices = new Choices();
            string[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngne_SpeechRecognized);

            speech.SelectVoiceByHints(VoiceGender.Female);
        }

        private void recEngne_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;

            if(result == "Hello") 
            {
                result = "Hello, my name is Quinch. How can I help you?";            
            }

            if(result == "What time is it") 
            {
                result = "it is currently" + DateTime.Now.ToLongTimeString();
            }

            if (result == "Open Google") 
            {
                System.Diagnostics.Process.Start("https://www.google.com/");
                result = "Opening Google";
            }

            if (result == "Twitter") 
            {
                System.Diagnostics.Process.Start("https://twitter.com/login"); 
            }

            if (result == "Close Brave") 
            {
                System.Diagnostics.Process[] close = System.Diagnostics.Process.GetProcessesByName("Brave");
                foreach (System.Diagnostics.Process p in close)
                    p.Kill();
                result = "Closing Brave";
            }

            if(result == "Shut Down") 
            {
                Application.Exit();
            }

            /* if(result == "Song") 
             * {
             *      music.SoundLocation = "Song";
             *      music.Play();
             *      result = "";
             * }
             */

            /*
             * if(result == "Stop")
             * {
             *      speech.SpeakAsyncCancelAll();
             *      music.Stop();
             *      result = "";
             * }
             */

            if (result == "Open YouTube")
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/");
                result = "Opening YouTube";
            }

            /*if (result == "any application")
            {
                System.Diagnostics.Process.Start("application path");
                result = "Name of the application";
            }*/

            if (result == "Open Visual Studio")
            {
                System.Diagnostics.Process.Start("C:\\Users\\dev13\\Desktop");
                result = "Opening Visual Studio";
            }

            speech.SpeakAsync(result);
            label2.Text = result;
        }
    }
}
