using SORTER_MVVM.Model.Interfaces;
using System.IO;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;

namespace SORTER_MVVM.Model
{
    internal static class Source
    {
        public static void SourseTOP(string OutPut_DirectoryList_Items, string InPut_Directory, string type)
        {
            //sourse.justsort
            string[] Input_list = Directory.GetFiles($@"{OutPut_DirectoryList_Items}");
            foreach (var item in Input_list)
            {
                if (item.Contains(type))
                {
                    FileInfo Input_list_FileInfo = new FileInfo($@"{item}");
                    string FileName = Input_list_FileInfo.Name;
                    //Catch&Move
                    if (File.Exists($@"{InPut_Directory}\{FileName}") == true)
                    {
                        DialogResult Сhoice = WinForms.MessageBox.Show($"{FileName} already exists. \nDo you want to replace it?", "Move error", MessageBoxButtons.YesNo);
                        if (Сhoice == DialogResult.Yes)
                        {
                            File.Delete($@"{InPut_Directory}\{FileName}");
                            File.Move(item, $@"{InPut_Directory}\{FileName}");
                        }
                        else if (Сhoice == DialogResult.No) { }
                    }
                    else
                    {
                        File.Move(item, $@"{InPut_Directory}\{FileName}");
                    }
                    //Catch&Move
                }
            }
            //sourse.justsort
        }
    }
}
