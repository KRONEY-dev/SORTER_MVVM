namespace SORTER_MVVM.Model.Interfaces
{
    internal interface IDialogService
    {
        string Directory { get; set;}
        string Exist_Message { get; }
        bool OpenFileDialog_OutPut();
        bool OpenFileDialog_InPut();
    }
}
