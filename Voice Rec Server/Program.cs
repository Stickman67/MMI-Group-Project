using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Speech.Recognition;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace RecoServeur
{
	class Program
	{
		public static SpeechRecognitionEngine speechRecognitionEngine;
		public static string reception = "#";
		public static string endWord = "stop"; 
		public static string ipServer = "127.0.0.1";
		public static byte[] data = new byte[512];
		public static int port = 26000;
		public static double validity = 0.70f;

		public static void Main(string[] args)
		{

			speechRecognitionEngine = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
			try
			{
				// create the engine
				// hook to events
			//	speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
				speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

				// load dictionary
				try
				{
					string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\grammar.txt");
					foreach (string line in lines)
					{
						//reco UDP Port
						if (line.StartsWith("#P"))
						{
							var parts = line.Split(new char[] { ' ' });
							port = Convert.ToInt32(parts[1]);
							Console.WriteLine("Port : " + parts[1]);
							continue;
						}
						// Reco endWord
						if (line.StartsWith("#E"))
						{
							var parts = line.Split(new char[] { ' ' });
							endWord = parts[1];
							Console.WriteLine("End Word : " + parts[1]);
							continue;
						}
						// Reco IP server
						if (line.StartsWith("#I"))
						{
							var parts = line.Split(new char[] { ' ' });
							ipServer = parts[1];
							Console.WriteLine("ipServer : " + parts[1]);
							continue;
						}
						// Reco validity
						if (line.StartsWith("#V"))
						{
							var parts = line.Split(new char[] { ' ' });
							validity = Convert.ToInt32(parts[1])/100.0f;
							Console.WriteLine("Validity : " + parts[1]);
							continue;
						}

						// skip commentblocks and empty lines..
						if (line.StartsWith("#") || line == String.Empty) continue;
					}
                    Choices place = new Choices("place", "create", "add");
                    Choices delete = new Choices("delete", "remove");
                    Choices all = new Choices("all", "everything");
                    Choices number = new Choices("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
                    Choices colour = new Choices("red", "green", "blue", "yellow", "orange", "purple", "pink", "black", "white");

                    GrammarBuilder place_no_colour = new GrammarBuilder();
                    place_no_colour.Append(place);
                    place_no_colour.Append("marker");

                    GrammarBuilder place_colour = new GrammarBuilder();
                    place_colour.Append(place);
                    place_colour.Append(colour);
                    place_colour.Append("marker");

                    Choices place_cmd = new Choices(place_colour, place_no_colour);

                    GrammarBuilder delete_all_no_colour = new GrammarBuilder();
                    delete_all_no_colour.Append(delete);
                    delete_all_no_colour.Append(all);

                    GrammarBuilder delete_all_colour = new GrammarBuilder();
                    delete_all_colour.Append(delete);
                    delete_all_colour.Append(all);
                    delete_all_colour.Append(colour);

                    Choices delete_all_cmd = new Choices(delete_all_no_colour, delete_all_colour);

                    GrammarBuilder delete_no_number = new GrammarBuilder();
                    delete_no_number.Append(delete);
                    delete_no_number.Append(colour);

                    GrammarBuilder delete_no_colour = new GrammarBuilder();
                    delete_no_colour.Append(delete);
                    delete_no_colour.Append(number);

                    GrammarBuilder delete_both = new GrammarBuilder();
                    delete_both.Append(delete);
                    delete_both.Append(colour);
                    delete_both.Append(number);

                    Choices delete_cmd = new Choices(delete_both, delete_no_colour, delete_no_number);

                    GrammarBuilder goto_no_number = new GrammarBuilder();
                    goto_no_number.Append("go to");
                    goto_no_number.Append(colour);

                    GrammarBuilder goto_no_colour = new GrammarBuilder();
                    goto_no_colour.Append("go to");
                    goto_no_colour.Append(number);

                    GrammarBuilder goto_both = new GrammarBuilder();
                    goto_both.Append("go to");
                    goto_both.Append(colour);
                    goto_both.Append(number);

                    Choices goto_cmd = new Choices(goto_both, goto_no_colour, goto_no_number);

                    Choices commands = new Choices(place_cmd, delete_cmd, delete_all_cmd, goto_cmd);

					Grammar commandList = new Grammar(new GrammarBuilder(commands));
                    speechRecognitionEngine.LoadGrammar(commandList);
				}
				catch (Exception ex)
				{
					throw ex;
					System.Environment.Exit(0);
				}

				// use the system's default microphone
				speechRecognitionEngine.SetInputToDefaultAudioDevice();
				// start listening
				speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message, "MicroPhone?");
				speechRecognitionEngine.RecognizeAsyncStop();
				speechRecognitionEngine.Dispose();
				System.Environment.Exit(0);
			}
			// UDP init
			Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ipServer), port);
			// Ready
			Console.WriteLine("Ready.....");

			while (true)
			{
				if (reception != "#")
				{
					data = Encoding.ASCII.GetBytes(reception);
					server.SendTo(data, iep);
					//Console.WriteLine("Sending : "+reception);
					reception = "#";
				}
				Thread.Sleep(2);
			}

		} // main

		public static  void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
		{
			Console.WriteLine(e.AudioLevel);
		}


		public static void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			if (e.Result.Confidence >= validity)
			{
				reception = e.Result.Text;
				//Console.WriteLine(reception);
				// Arret du serveur si mot magique
				if (e.Result.Text == endWord)
				{
					speechRecognitionEngine.RecognizeAsyncStop();
					speechRecognitionEngine.Dispose();
					System.Environment.Exit(0);
				}
			}
			else
			{
				reception = "#";
			}
		}

	} // Program
} // RecoServeur
// End source

