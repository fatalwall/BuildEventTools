using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INI_LIB;

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
                INI File = new INI(args[0]);
                INI_Section Section = File.Sections.Where(s => s["Text"]?.Value == args[1]).FirstOrDefault();
                if (Section is null)
                {   //New Section
                    int maxSectionNumber = File.Sections.Where(n => n.Name.StartsWith("Item ")).Select(n => int.Parse(n.Name.Replace("Item ", ""))).Max();
                    Section = new INI_Section(string.Format("Item {0}", maxSectionNumber + 1));
                    Section.Add(new INI_KeyValue("Text", args[1]));
                    for (int i = 2; i <= args.Count() - 2; i += 2)
                    {
                        Key = args[i];
                        Value = args[i + 1];
                        Section.Add(new INI_KeyValue(Key, Value));
                    }
                    File.Add(Section);
                }
                else
                {   //Existing Section with specified Text Value
                    for (int i = 2; i <= args.Count() - 2; i += 2)
                    {
                        Key = args[i];
                        Value = args[i + 1];
                        try { Section[Key].Value = Value; } //Existing Key
                        catch { Section.Add(new INI_KeyValue(Key, Value)); } //New Key
                    }
                }
                File.Write();
            }
            catch (System.IO.FileNotFoundException) { Environment.ExitCode = -4; }
            catch (System.IO.IOException) { Environment.ExitCode = -3; }
            catch { Environment.ExitCode = -1; }
        }
    }
}
