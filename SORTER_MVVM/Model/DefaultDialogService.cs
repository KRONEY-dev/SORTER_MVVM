using Microsoft.WindowsAPICodePack.Dialogs;

namespace SORTER_MVVM.Model.Interfaces
{
    internal class DefaultDialogService : IDialogService
    {
        public string Directory { get; set;}
        public string Exist_Message { get => "This directory is already exist"; }
        private readonly string Title_for_InputDialog = "Enter input directory files";
        private readonly string Title_for_OutputDialog = "Enter output directory files";

        public bool OpenFileDialog_OutPut() => OpenFileDialog(Title_for_OutputDialog);

        public bool OpenFileDialog_InPut() => OpenFileDialog(Title_for_InputDialog);

        private bool OpenFileDialog(string Title)
        {
            using (CommonOpenFileDialog pass = new CommonOpenFileDialog())
            {
                pass.IsFolderPicker = true;
                pass.Title = Title;

                if (pass.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Directory = pass.FileName;
                    return true;
                }
                return false;
            }
        }
    }
}
