using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using WebApplication.Factory;
using WebApplication.Models;

namespace WebApplication.Pages
{
    public partial class DetectFolder : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void DetectChanges(object sender, EventArgs eventArgs)
        {
            var newPhysicalPathFolder = inputFilePath.Text;                           // Load string from input box
            if (!Path.IsPathRooted(newPhysicalPathFolder))                                  // Check if path is relative
                newPhysicalPathFolder = Server.MapPath(inputFilePath.Text);

            var newFiles = FilesFactory.LoadFiles(newPhysicalPathFolder);      // Load files models from directory. If it does not exist, it return null.
            if (newFiles == null)                                                           // If directory not exist, it will set variables to default values and it display message in label.
            {
                FilesFactory._oldPath = null;
                FilesFactory._files = new List<FileModel>();
                ResultsLabel.Text = "Hledaná složka neexistuje!";                           // If filepath is not exist, it write warning and finished
                return;
            }
            
            if (FilesFactory.IsNewDirectory(newPhysicalPathFolder))                         // It check if path is load first time
            {                                                                               // If it is true, it restores values
                FilesFactory._oldPath = newPhysicalPathFolder;
                FilesFactory._files = newFiles;
                ResultsLabel.Text = "Nový adresař, žádne změny.";
                return;
            }

            var result = FilesFactory.ChangesInDirectory(newFiles);                         // It find out different files

            ResultsLabel.Text = FilesFactory.ViewResults(result);                           // It composes string of results to view  
        }
    }
}