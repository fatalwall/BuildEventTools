﻿/* 
 *Copyright (C) 2018 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/BuildEventTools
 */

using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace GetAssemblyValue
{
    class Program
    {
        private static string output { get; set; }

        private static string filePath { get; set; }
        private static ValueNames valueName { get; set; }

        private static void printHelp()
        {
            string output = "";

            output += Environment.NewLine + string.Format("\tGetAssemblyValue");
            output += Environment.NewLine + string.Format("\tOutputs a specified detail about an assembly");

            //Commands
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t<COMMANDS>");
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t{0}\t\t{1}", "Path", "Assembly Path (dll or exe)");
            output += Environment.NewLine + string.Format("\t{0}\t{1}", "Value Name", "(Optional) Defaults to Version if no value is provided");
            output += Environment.NewLine + string.Format("\t\t\t\t{0}", "Expected Values:");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Company");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Copyright");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Description");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Product");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Title");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Trademark");
            output += Environment.NewLine + string.Format("\t\t\t\t\t{0}", "Version");

            //Information Commands
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t{0}\t\t{1}", "?", "Displays this menu");

            //Usage
            output += Environment.NewLine;
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t<USAGE>");
            output += Environment.NewLine;

            output += Environment.NewLine + string.Format("\tReturns the Assembly Version Number");
            output += Environment.NewLine + string.Format("\t\tGetAssemblyValue.EXE C:\\MyProgram.exe");
            output += Environment.NewLine;

            output += Environment.NewLine + string.Format("\tReturns the Assembly Company Attribute");
            output += Environment.NewLine + string.Format("\t\tGetAssemblyValue.EXE C:\\MyProgram.exe CompanyName");
            output += Environment.NewLine;

            //Exit Codes
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t<Exit Codes>");
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "0", "Application completed successfully");
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "-1", "Assembly Path could not be found");
            output += Environment.NewLine + string.Format("\t{0,4}\t{1}", "-2", "Value passed for ValueName is invalid");



            //Copywrite details
            output += Environment.NewLine;
            output += Environment.NewLine;
            output += Environment.NewLine + string.Format("\t*************************************************************************");
            output += Environment.NewLine + string.Format("\t* Copyright (C) 2018 Peter Varney - All Rights Reserved");
            output += Environment.NewLine + string.Format("\t* You may use, distribute and modify this code under the");
            output += Environment.NewLine + string.Format("\t* terms of the MIT license,");
            output += Environment.NewLine + string.Format("\t*");
            output += Environment.NewLine + string.Format("\t* You should have received a copy of the MIT license with");
            output += Environment.NewLine + string.Format("\t* this file. If not, visit : https://github.com/fatalwall/INI_File_Tools");
            output += Environment.NewLine + string.Format("\t*************************************************************************");

            Console.WriteLine(output);
        }

        static void Main(string[] args)
        {
            if (args.Contains("?")
                || args.Contains("-h", StringComparer.OrdinalIgnoreCase)
                || args.Contains("h", StringComparer.OrdinalIgnoreCase)
                || args.Contains("-help", StringComparer.OrdinalIgnoreCase)
                || args.Contains("help", StringComparer.OrdinalIgnoreCase))
            {
                //Output Help Details
                printHelp();
                Environment.Exit(0); //Exit program after outputing message
            }

            /*
             * INPUT
             *   Path           Assembly Path (dll or exe)
             *   ValueName      (Optional) Defaults to Version if no value is provided
             *                  Expected Values:
             *                      Version
             *                      Company
             *                      Copyright
             *                      Description
             *                      Product
             *                      Title
             *                      Trademark
             *                     
             * Exit Codes
             *    0             Success
             *   -1             Assembly Path could not be found
             *   -2             Value passed for ValueName is invalid
            */
            switch (args.Count())
            {
                case 0:
                    printHelp();
                    Environment.Exit(0); //Exit program after outputing message
                    break;
                case 1:
                    filePath = File.Exists(args[0]) ? args[0] : null;
                    //valueName = ValueNames.Version;
                    break;
                case 2:
                    filePath = File.Exists(args[0]) ? args[0] : null;
                    try { valueName = (ValueNames)Enum.Parse(typeof(ValueNames),args[1], true); }
                    catch { Environment.Exit(-2); } //args[1] was not a valid type
                    break;
                default:
                    filePath = null;
                    break;
            }
            //If file path is invalid exit with error
            if (filePath == null) Environment.Exit(-1); //args[0] could not be found

            try
            {
                Assembly assembly = Assembly.LoadFile(filePath);
                switch (valueName)
                {
                    case ValueNames.Version:
                        output = assembly.GetName().Version.ToString();
                        break;
                    case ValueNames.Company:
                        output = ((AssemblyCompanyAttribute)(assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true)[0]))?.Company ?? "";
                        break;
                    case ValueNames.Copyright:
                        output = ((AssemblyCopyrightAttribute)(assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true)[0]))?.Copyright ?? "";
                        break;
                    case ValueNames.Description:
                        output = ((AssemblyDescriptionAttribute)(assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true)[0]))?.Description ?? "";
                        break;
                    case ValueNames.Product:
                        output = ((AssemblyProductAttribute)(assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true)[0]))?.Product ?? "";
                        break;
                    case ValueNames.Title:
                        output = ((AssemblyTitleAttribute)(assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true)[0]))?.Title ?? "";
                        break;
                    case ValueNames.Trademark:
                        output = ((AssemblyTrademarkAttribute)(assembly.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), true)[0]))?.Trademark ?? "";
                        break;
                }
                assembly = null;
            } catch (BadImageFormatException)
            {
                FileVersionInfo exe = FileVersionInfo.GetVersionInfo(filePath);
                switch (valueName)
                {
                    case ValueNames.Version:
                        output = exe?.ProductVersion ?? "";
                        break;
                    case ValueNames.Company:
                        output = exe?.CompanyName ?? "";
                        break;
                    case ValueNames.Copyright:
                        output = exe?.LegalCopyright ?? "";
                        break;
                    case ValueNames.Description:
                        output = exe?.FileDescription ?? "";
                        break;
                    case ValueNames.Product:
                        output = exe?.ProductName ?? "";
                        break;
                    case ValueNames.Title:
                        output = exe?.ProductName ?? "";
                        break;
                    case ValueNames.Trademark:
                        output = exe?.LegalTrademarks ?? "";
                        break;
                }
                exe = null;
            }
            Console.Write(output);
        }

        enum ValueNames
        {
            Version,
            Product,
            Title,
            Copyright,
            Trademark,
            Description,
            Company
        }
    }
}
