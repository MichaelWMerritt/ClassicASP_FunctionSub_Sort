using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SubroutineFunctionSort_VB
{
    class NewFile
    {
        private string file, filePath, fileName, fileExtension, newFile, userFilePath;
        private StreamReader input = null;

        public NewFile(string[] args)
        {
            file = args[0];
            if (args.Length > 1)
            {
                userFilePath = args[1];
            }
            else
            {
                userFilePath = "";
            }
            extractFileInformation(userFilePath);
        }

        /*
         * Sets information about file such as extension and directory name
         * Accepts string array of arguments
         * Will either set filePath as current file directory or specified user arg # 2
         */
        private void extractFileInformation(string userFilePath)
        {
            if (File.Exists(file))
            {
                input = File.OpenText(file);
            }

            filePath = Path.GetDirectoryName(file);
            fileName = Path.GetFileNameWithoutExtension(file);
            fileExtension = Path.GetExtension(file);

            if (userFilePath.Length > 0)
            {
                filePath = userFilePath;
            }

            newFile = filePath + @"\" + fileName + "Sorted" + fileExtension;
        }

        public String getNewFile()
        {
            return newFile;
        }

        public StreamReader getInput()
        {
            return input;
        }
    }
}
