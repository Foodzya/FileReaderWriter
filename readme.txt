Hello! This is FileReaderWriter Application made by Alexander B.

This console application supports multiple file formats read/write operations and aggregate operations over the content of these file types.

Supported file formats are: .txt (plain text), .rtxt (reversed text), .etxt (encrypted text using caesar cipher) and .btxt (plain text as binary).

Almost all menu movements are executed by pressing a button once.

There are to ways to use the application: through the menu (launch FileReaderWriter.exe) and through the command line (only multiple .txt files write option to appropriate files with the specified extension).

How to use the application through the menu:
    1. By pressing 1 you'll move to the READ MENU which is responsible for reading files with specific formats (which I mentioned above).
    In read menu you'll have to specify absolute path to the file which you would like to read the text from. If the file exists by your path, you will move to the appropriate menu based on the file extension. If not, you will be returned to the READ MENU. Also you are able to return to the previous menu by pressing 2.
    2. By pressing 2 you'll move to the WRITE MENU which is responsible for reading text from console or text file (.txt) and formatting this text according to the extension of the file where you want to write the final text. If you will specify wrong path to the file which you'd like to write text in, then you will be able to type path once again or return back to the previous menu.
    3. By pressing 3 you'll quit the application.

How to use the application throught the menu:
    First you need to open the terminal in the folder where FileReaderWriter.exe is located. To launch multiple write workflow you need to specify following arguments (required!):
    --interactive (this argument must go first);
    --bulk (this argument is specifying that user is going to execute multiple files write flow);
    --source=<dir_name> (after '=' you must specify an absolute path to directory with .txt files);
    --target=<dir_name> (after '=' you must specify an absolute path to directory where files with specified format will be saved);
    --format=<format_type> (after '=' you must specify the extension of files in which .txt files will be converted (example: --format=.rtxt));
NOTE: IF THE SPECIFIED FORMAT IS .etxt YOU ALSO HAVE TO SPECIFY ADDITIONAL ARGUMENTS:
    --shift=<encryptor_shift> (this argument is used to specify shift for caesar cipher encryptor, only numeric values (example --shift=3));
    --direction<encryptor_direction> (this argument is used to specify direction for caesar cipher encryptor, only 'right' and 'left' values are applicable).

There are some examples of how to use this application:
1) 
Goal: choose file with .txt extension and convert its content into the new file with .rtxt extension.
Solution (steps):
    1. Press 2 in the main menu to move to the WRITE MENU;
    2. Press 1 to move to the menu where you can choose source of the text (it might be .txt file or console input);
    3. Press 1 to choose .txt file source;
    4. Specify absolute path to the txt file to read source text from (example: C:\ExampleFolder\example.txt). If you'd like to get back to the previous step you can enter 'q' or any incorrect path;
    5. Next specify absolute path to the file with desirable extension (.rtxt in this example). If you'd like to get back to the previous step you can enter 'q' or any incorrect path;
    6. Voila! If you didn't see any exception, convert is done. Go check for content of the target .rtxt file.
2) 
Goal: choose file with .btxt extension (binary file), read its content and convert it into the new file with .txt extension.
Solution (steps):
    1. Press 1 in the main menu to move to the READ MENU;
    2. Press 1 to specify path to the file to read from (.btxt in this example);
    3. Specify absolute path to the .btxt file to read source content from (example: C:\ExampleFolder\example.btxt). If you'd like to get back to the previous step you can enter 'q' or any incorrect path;
    4. After specifying path to the .btxt file select target where you would like to write text (it can be .txt file or console);
    5. Press 1 to write text to the .txt file;
    6. Voila! If you didn't see any exception, convert is done. Press any button to get back to the main menu. And go check for content of the target .txt file.
3)
Goal: to convert multiple txt files in .etxt files using command line (Settings for .etxt: shift = 1, direction = right).
Solution (must be in the folder with FileReaderWriter.exe): 
    FileReaderWriter.exe --interactive --bulk --source=C:\Txtfiles --target=C:\Txtfiles\Convertedtxtfiles --format=.etxt --shift=1 --direction=right
Voila! Go check C:\Txtfiles\Convertedfiles folder. There you'll find files with .etxt extension (names of the files are equal for their txt predecessors, so if it was test.txt resuls will be test.etxt).