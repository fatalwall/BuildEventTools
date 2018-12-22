# BuildEventTools
Tools to assist in building and packaging of applicaitons

#GetAssemblyValue

        GetAssemblyValue
        Outputs a specified detail about an assembly

        <COMMANDS>

        Path            Assembly Path (dll or exe)
        Value Name      (Optional) Defaults to Version if no value is provided
                                Expected Values:
                                        CompanyName
                                        ProductName
                                        Version

        ?               Displays this menu


        <USAGE>

        Returns the Assembly Version Number
                GetAssemblyValue.EXE C:\MyProgram.exe

        Returns the Assembly Company Attribute
                GetAssemblyValue.EXE C:\MyProgram.exe CompanyName


        <Exit Codes>

           0    Application completed successfully
          -1    Assembly Path could not be found
          -2    Value passed for ValueName is invalid


        *************************************************************************
        * Copyright (C) 2018 Peter Varney - All Rights Reserved
        * You may use, distribute and modify this code under the
        * terms of the MIT license,
        *
        * You should have received a copy of the MIT license with
        * this file. If not, visit : https://github.com/fatalwall/INI_File_Tools
        *************************************************************************