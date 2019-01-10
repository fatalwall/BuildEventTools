using System;
using System.Linq;
using System.IO.INI;

namespace NSISEmbeddedListBuilder
{
    class Program
    {
        private static void printHelp()
        {
            string output = "";

            output += Environment.NewLine + string.Format("\tNSIS Embedded List Builder");
            output += Environment.NewLine + string.Format("\tAppends values to NSIS Options file for EmbeddedList plugin");

            //Commands
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t<COMMANDS>");
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t{0}\t\t{1}", "Path", "INI File Path");
            output += Environment.NewLine + string.Format("\t{0}\t\t{1}", "Text", "Primary field of List");
            output += Environment.NewLine + string.Format("\t{0}\t{1}", "SubItem1 value", "(Optional) Additional field of List");
            output += Environment.NewLine + string.Format("\t{0}\t{1}", "SubItem2 value", "(Optional) Additional field of List");
            output += Environment.NewLine + string.Format("\t{0}\t{1}", "SubItemN value", "(Optional) Additional field of List");
            output += Environment.NewLine + string.Format("\t{0}\t{1}", "IconIndex value", "(Optional) Integer value matching a Icon");
            output += Environment.NewLine + string.Format("\t{0}\t{1}", "Checked value", "(Optional) bool value indicating check state");
            output += Environment.NewLine + string.Format("\t\t\t\t{0}", "0 = Unchecked");
            output += Environment.NewLine + string.Format("\t\t\t\t{0}", "1 = Checked");


            //Information Commands
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t{0}\t\t{1}", "?", "Displays this menu");

            //Usage
            output += Environment.NewLine;
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t<USAGE>");
            output += Environment.NewLine;

            output += Environment.NewLine + string.Format("\tReturns the Assembly Version Number");
            output += Environment.NewLine + string.Format("\t\tNSISEmbeddedListBuilder.EXE Plugins.ini PluginName SubItem1 \"Unknown Author\" IconIndex 2 Checked 1");
            output += Environment.NewLine;

            //Exit Codes
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t<Exit Codes>");
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "0", "Application completed successfully");
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "-1", "Unknown exception has occured");
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "-2", "Last argument ignored because an odd number of arguments passedast");
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "-3", "An io exception occured while trying to read or write ini file");
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "-4", "A file not found exception occured while trying to read ini file");


            //Copywrite details
            output += Environment.NewLine;
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t*************************************************************************");
            output += Environment.NewLine + string.Format("\t* Copyright (C) 2019 Peter Varney - All Rights Reserved");
            output += Environment.NewLine + string.Format("\t* You may use, distribute and modify this code under the");
            output += Environment.NewLine + string.Format("\t* terms of the MIT license,");
            output += Environment.NewLine + string.Format("\t*");
            output += Environment.NewLine + string.Format("\t* You should have received a copy of the MIT license with");
            output += Environment.NewLine + string.Format("\t* this file. If not, visit : https://github.com/fatalwall/BuildEventTools");
            output += Environment.NewLine + string.Format("\t*************************************************************************");

            Console.WriteLine(output);
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Count() < 2)
                {
                    //Output Help Details
                    printHelp();
                    Environment.Exit(0); //Exit program after outputing message
                }
                if (args.Count() % 2 != 0)
                {
                    Environment.ExitCode = -2;
                }

                string Key, Value;
                File file = new File(args[0]);
                Section section = file.Sections.Where(s => s["Text"]?.Value == args[1]).FirstOrDefault();
                if (section is null)
                {   //New Section
                    int maxSectionNumber = file.Sections.Where(n => n.Name.StartsWith("Item ")).Select(n => int.Parse(n.Name.Replace("Item ", ""))).Max();
                    section = new Section(string.Format("Item {0}", maxSectionNumber + 1));
                    section.Add(new KeyValuePair("Text", args[1]));
                    for (int i = 2; i <= args.Count() - 2; i += 2)
                    {
                        Key = args[i];
                        Value = args[i + 1];
                        section.Add(new KeyValuePair(Key, Value));
                    }
                    file.Add(section);
                }
                else
                {   //Existing Section with specified Text Value
                    for (int i = 2; i <= args.Count() - 2; i += 2)
                    {
                        Key = args[i];
                        Value = args[i + 1];
                        try { section[Key].Value = Value; } //Existing Key
                        catch { section.Add(new KeyValuePair(Key, Value)); } //New Key
                    }
                }
                file.Write();
            }
            catch (System.IO.FileNotFoundException) { Environment.ExitCode = -4; Console.WriteLine("File not found."); }
            catch (System.IO.IOException) { Environment.ExitCode = -3; Console.WriteLine("An IO excection has occured."); }
            catch { Environment.ExitCode = -1; Console.WriteLine("An unknown exception has occured."); }
        }
    }
}
