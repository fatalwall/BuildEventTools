# BuildEventTools
Tools to assist in building and packaging of applicaitons

## GetAssemblyValue

        GetAssemblyValue
        Outputs a specified detail about an assembly

        <COMMANDS>

        Path            Assembly Path (dll or exe)
        Value Name      (Optional) Defaults to Version if no value is provided
                                Expected Values:
						Version
						Company
						Copyright
						Description
						Product
						Title
						Trademark
									
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

## NSISEmbeddedListBuilder

        NSIS Embedded List Builder
        Appends values to NSIS Options file for EmbeddedList plugin

        <COMMANDS>

        Path            INI File Path
        Text            Primary field of List
        SubItem1 value  (Optional) Additional field of List
        SubItem2 value  (Optional) Additional field of List
        SubItemN value  (Optional) Additional field of List
        IconIndex value (Optional) Integer value matching a Icon
        Checked value   (Optional) bool value indicating check state
                                0 = Unchecked
                                1 = Checked

        ?               Displays this menu


        <USAGE>

        Returns the Assembly Version Number
                NSISEmbeddedListBuilder.EXE Plugins.ini PluginName SubItem1 "Unknown Author" IconIndex 2 Checked 1


        <Exit Codes>

           0    Application completed successfully
          -1    Unknown exception has occured
          -2    Last argument ignored because an odd number of arguments passedast
          -3    An io exception occured while trying to read or write ini file
          -4    A file, directory, drive not found exception occured
	  -5	Unauthorized Access


        *************************************************************************
        * Copyright (C) 2019 Peter Varney - All Rights Reserved
        * You may use, distribute and modify this code under the
        * terms of the MIT license,
        *
        * You should have received a copy of the MIT license with
        * this file. If not, visit : https://github.com/fatalwall/INI_File_Tools
        *************************************************************************
