using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Service
{
    public static class FileManagementService
    {
        // Function to create a new folder in a specified directory
        public static void CreateNewFolder(string parentDirectory, string newFolderName)
        {
            // Combine the parent directory path with the new folder name to get the full path
            string newFolderPath = Path.Combine(parentDirectory, newFolderName);

            // Check if the directory already exists
            if (!Directory.Exists(newFolderPath))
            {
                // Create the directory if it does not exist
                Directory.CreateDirectory(newFolderPath);
            }
            else
            {
                // Optionally, handle the case where the folder already exists
                System.Diagnostics.Debug.WriteLine("The folder already exists.");
            }
        }

        // Function to copy a file to a specified folder with a new name
        public static string CopyFileToFolder(string filePath, string folderPath, string newFileNameWithoutExtension)
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Ensure the destination folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Get the file extension from the original file path
                string fileExtension = Path.GetExtension(filePath);

                // Combine the new file name without extension and the file extension
                string newFileName = newFileNameWithoutExtension + fileExtension;

                // Combine the folder path and new file name to create the destination file path
                string destinationFilePath = Path.Combine(folderPath, newFileName);

                // Copy the file to the destination folder with the new name
                File.Copy(filePath, destinationFilePath, overwrite: true);

                // Return the full path of the copied file
                return destinationFilePath;
            }
            else
            {
                // Handle the case where the file does not exist
                System.Diagnostics.Debug.WriteLine("The file does not exist.");
                return null; // Or you can throw an exception or return an empty string based on your preference
            }
        }
    }
}
