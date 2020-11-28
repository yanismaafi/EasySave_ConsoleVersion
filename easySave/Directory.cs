using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace easySave
{
    class Directory
    {

        public static int nbrDir;



        public long getDirectorySize(string directory)
        {
            string[] files = System.IO.Directory.GetFiles(directory);

            long size = 0;

            foreach (string file in files)    // Loop on the files into Directory 
            {

                FileInfo info = new FileInfo(file);     // Use FileInfo to get length of each file.
                size += info.Length;
            }

            return size;      // return the result directory size
        }



        public DataLog extractInformationDirectory(string directory)
        {
            nbrDir++;
            DirectoryInfo dirInfo = new DirectoryInfo(directory);
            string dirName = dirInfo.Name;
            string dirExtention = dirInfo.Extension;
            DateTime dirLastAcess = dirInfo.LastAccessTime;
            long size = getDirectorySize(directory);

            DataLog InformationDirectory = new DataLog(nbrDir, dirName, dirLastAcess, size);   // Convert the Directory's information to Json

            return InformationDirectory;
        }



    }
}
