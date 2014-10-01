using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace SubroutineFunctionSort_VB
{
    class SortAlgorithm
    {
        private string patternFindSubandFuncNames, patternFindSubandFuncs, textFromFile;
        SortedDictionary<String, String> subsAndFunctions = new SortedDictionary<string, string>();
        ArrayList subsAndFunctionNameList = new ArrayList(), subsAndFunctionBlockList = new ArrayList();


        /*
         * Creates Regexes with specified patterns
         * Accepts boolean to choose either sort by name only or sort by name of block (function/sub) and name
         * Returns ArrayList of regexes which must be cast upon use
         * 
         */
        private ArrayList setRegex(bool sortByName)
        {
            ArrayList regexes = new ArrayList();
            if (sortByName)
            {
                //Pattern to capture Name of Function/Sub including the word function/sub
                patternFindSubandFuncNames = @"((sub\s)[a-zA-Z0-9]*(?=\()|(sub\s)[a-zA-Z0-9]*(?=\s\())|((function\s)[a-zA-Z0-9]*(?=\()|(function\s)[a-zA-Z0-9]*(?=\s\())";
            }
            else
            {
                //Pattern to capture only Name of Function/Sub 
                patternFindSubandFuncNames = @"((?<=sub\s)[a-zA-Z0-9]*(?=\()|(?<=sub\s)[a-zA-Z0-9]*(?=\s\())|((?<=function\s)[a-zA-Z0-9]*(?=\()|(?<=function\s)[a-zA-Z0-9]*(?=\s\())";
            }
            //Pattern to capture whole subroutine/function block
            patternFindSubandFuncs = @"((?=sub)(?s:.*?)(?<=sub\n))|((?=function)(?s:.*?)(?<=function\n))";

            Regex rgx1 = new Regex(patternFindSubandFuncs, RegexOptions.IgnoreCase);
            Regex rgx2 = new Regex(patternFindSubandFuncNames, RegexOptions.IgnoreCase);
            regexes.Add(rgx1);
            regexes.Add(rgx2);
            return regexes;
        }

        /*
         * Sort Algorithm function
         * Accepts string array of arguments
         * Reads inputted file if possible and writes new sorted file
         * 
         */
        public void runSort(string[] args)
        {
            NewFile nf = new NewFile(args);
            StreamReader input = nf.getInput();
            // Creates new ArrayList and sets it from setRegex function, true or false toggles how algorithm sorts
            ArrayList regexes = setRegex(false);

            try
            {
                using (input)
                {
                    while (!input.EndOfStream)
                    {
                        /**Reads text from file and concats with newline character. 
                         * This way it doesn't matter how the file was saved as this just 
                         * reads each line and recreates a formatted file.
                         */
                        textFromFile += input.ReadLine() + "\n";
                    }
                    // First matches the whole subroutine/function block
                    foreach (Match match in ((Regex)regexes[0]).Matches(textFromFile))
                    {
                        // then matches the name of that block and adds the name as key and block as value to SortedDictionary
                        subsAndFunctions.Add(((Regex)regexes[1]).Match(match.Value).Value, match.Value);
                    }
                }

                var subsAndFunctionNames = subsAndFunctions.ToList();

                foreach (var x in subsAndFunctionNames)
                {
                    subsAndFunctionNameList.Add(x.Key);
                    subsAndFunctionBlockList.Add(x.Value);
                }

                var matchCount = -1;

                using (System.IO.StreamWriter newFileToWrite = new System.IO.StreamWriter(nf.getNewFile()))
                {
                    //Each time a subroutine/function block is found it will be replaced by block stored in SortedDictionary
                    var newStr = ((Regex)regexes[0]).Replace(textFromFile,
                                    m =>
                                    {
                                        matchCount++;
                                        return subsAndFunctionNames[matchCount].Value;
                                    });

                    newFileToWrite.WriteLine(newStr);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        public ArrayList getSubsAndFunctionNameList()
        {
            return subsAndFunctionNameList;
        }

        public ArrayList getSubsAndFunctionBlockList()
        {
            return subsAndFunctionBlockList;
        }
    }
}
