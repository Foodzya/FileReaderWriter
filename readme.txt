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

How to use the application through the console:
PRECONDITION:
You need to open the terminal in the folder where FileReaderWriter.exe is located;
    1) Bulk write operation:
    To launch multiple write workflow you need to specify following arguments (required!):
    --bulk (this argument is specifying that user is going to execute multiple files write flow);
    --source=<dir_name> (after '=' you must specify an absolute path to directory with .txt files);
    --target=<dir_name> (after '=' you must specify an absolute path to directory where files with specified format will be saved);
    --format=<format_type> (after '=' you must specify the extension of files in which .txt files will be converted (example: --format=.rtxt));
NOTE: IF THE SPECIFIED FORMAT IS .etxt YOU ALSO HAVE TO SPECIFY ADDITIONAL ARGUMENTS:
    --shift=<encryptor_shift> (this argument is used to specify shift for caesar cipher encryptor, only numeric values (example --shift=3));
    --direction<encryptor_direction> (this argument is used to specify direction for caesar cipher encryptor, only 'right' and 'left' values are applicable).
    2) Counting the number of repetitive words and writing them in descending order to .json or console (example: “Hey hello hi hey” => [ {hey: 2}, {hello: 1}, {hi:1} ]):
    Required arguments:
    --repetitions (this argument specifies that user is going to execute word counting to json/console operation);
    --source=<path_to_source_file> (after '=' you must specify an absolute path to the file to read from (.etxt, .rtxt, .txt, .btxt formats are allowed));
    --target=<path_to_json_file> (optional; if specified along with --json argument, result will be written into the .json file);
    --json (optional; have to be specified along with --target=<path_to_json_file> argument to execute correct write operation);
    --console (optional; if specified, result will be written into the console);
    --shift (if specified source file have .etxt format);
    --direction (if specified source file have .etxt format);

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
4)
Goal: to read text from the encrypted .etxt file, count number of words repetitions, then write into the json file all words with number of repetitions in descending order of the number of repetitions;
Solution:
    FileReaderWriter.exe --repetitions --source=C:\ApplicationTests\myFile.etxt --target=C:\ApplicationTests\targetFile.json --json --shift=1 --direction=1
NOTE: myFile.etxt contains encrypted text. So first of all it will be decrypted with the specified shift and direction, then will be executed counting operation, after that all words with number of repetitions will be written to targetFile.json.
    Consider myFile.etxt contains the following text:
    "Mpsfn Jqtvn jt tjnqmz evnnz ufyu pg uif qsjoujoh boe uzqftfuujoh joevtusz. Mpsfn Jqtvn ibt cffo uif joevtusz't tuboebse evnnz ufyu fwfs tjodf uif 1500t".
    So after executing command line operation above targeFile.json will look like:
    "[ {the:3}, {lorem:2}, {ipsum:2}, {dummy:2}, {text:2}, {is:1}, {simply:1}, {of:1}, {printing:1}, {and:1}, {typesetting:1}, {industry:1}, {has:1}, {been:1}, {industry's:1}, {standard:1}, {ever:1}, {since:1}, {1500s:1} ]"

In the following examples file Test.txt contains the following text: 
"But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?"
NOTE: If source file have .etxt format, you also have to specify two additional arguments: --shift= and --direction= for decryption purposes;  
5) 
Goal: to search "to" in the specified .txt file and write number of occurencies is the console;
Solution:
    FileReaderWriter.exe --search=to --source=C:\Txtfiles\Test.txt
Result: 
    The word "to" appears in the text 2 time(-s)
6) 
Goal: to  get a full list of words and the number of the repetitions and sort this list in descending order into json file;
Solution:
    FileReaderWriter.exe --repetitions --source=C:\Txtfiles\Text.txt --target=C:\Convertedfiles\Result.json --json
Result: [ {who:3}, {a:3}, {has:2}, {to:2}, {pleasure:2}, {that:2}, {no:2}, {but:1}, {any:1}, {right:1}, {find:1}, {fault:1}, {with:1}, {man:1}, {chooses:1}, {enjoy:1}, {annoying:1}, {consequences:1}, {or:1}, {one:1}, {avoids:1}, {pain:1}, {produces:1}, {resultant:1} ]
7) 
Goal: print to console number of vowels and consonants in the specified source file;
Solution:
    FileReaderWriter.exe --vowels --source=C:\Txtfiles\Test.txt
Result:
    Vowels: 60, consonants: 81