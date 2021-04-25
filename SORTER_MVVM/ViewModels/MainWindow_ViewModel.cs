using SORTER_MVVM.Infrastructure.Commands;
using SORTER_MVVM.Model;
using SORTER_MVVM.Model.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SORTER_MVVM.ViewModels
{
    internal class MainWindow_ViewModel : ViewModel 
    {
        private readonly IDialogService dialogService = new DefaultDialogService();
        private readonly System_tray_Application system_Tray_Application = new System_tray_Application();

        #region Поля
        #region  IsEnabled кнопки ввода директорий сбора файлов
        /// <summary> IsEnabled кнопки ввода конечной директории</summary>
        private bool _Browse_OutPut_IsEnabled = true;

        public bool Browse_OutPut_IsEnabled
        {
            get => _Browse_OutPut_IsEnabled;
            set => Set(ref _Browse_OutPut_IsEnabled, value);
        }
        #endregion

        #region  IsEnabled кнопки разовой сортировки
        /// <summary> IsEnabled кнопки разовой сортировки</summary>
        private bool _Sort_IsEnabled = false;

        public bool Sort_IsEnabled
        {
            get => _Sort_IsEnabled;
            set => Set(ref _Sort_IsEnabled, value);
        }
        #endregion

        #region  IsEnabled кнопки Non-Stop сортировки
        /// <summary> IsEnabled кнопки Non-Stop сортировки</summary>
        private bool _Non_Stop_IsEnabled = false;

        public bool Non_Stop_IsEnabled
        {
            get => _Non_Stop_IsEnabled;
            set => Set(ref _Non_Stop_IsEnabled, value);
        }
        #endregion

        #region  IsChecked кнопки Non-Stop сортировки
        /// <summary> IsChecked кнопки Non-Stop сортировки</summary>
        private bool _Non_Stop_IsChecked = false;

        public bool Non_Stop_IsChecked
        {
            get => _Non_Stop_IsChecked;
            set => Set(ref _Non_Stop_IsChecked, value);
        }
        #endregion

        #region  IsEnabled вторичной кнопки ввода директорий сбора файлов
        /// <summary> IsEnabled вторичной кнопки ввода конечной директории</summary>
        private bool _Creator_Browse_OutPut_IsEnabled = false;

        public bool Creator_Browse_OutPut_IsEnabled
        {
            get => _Creator_Browse_OutPut_IsEnabled;
            set => Set(ref _Creator_Browse_OutPut_IsEnabled, value);
        }
        #endregion

        #region  IsEnabled кнопки удаления вибраной директории
        /// <summary> IsEnabled кнопки удаления вибраной директории</summary>
        private bool _Deletor_IsEnabled = false;

        public bool Deletor_IsEnabled
        {
            get => _Deletor_IsEnabled;
            set => Set(ref _Deletor_IsEnabled, value);
        }
        #endregion

        #region  IsEnabled кнопки Cleaner
        /// <summary> IsEnabled кнопки Cleaner (очистка коллекции директорий)</summary>
        private bool _Cleaner_IsEnabled = false;

        public bool Cleaner_IsEnabled
        {
            get => _Cleaner_IsEnabled;
            set => Set(ref _Cleaner_IsEnabled, value);
        }
        #endregion

        #region  IsEnabled кнопки Replace
        /// <summary> IsEnabled кнопки Replace (замена выбраной директории сбора, и конечной директории)</summary>
        private bool _Replace_IsEnabled = false;

        public bool Replace_IsEnabled
        {
            get => _Replace_IsEnabled;
            set => Set(ref _Replace_IsEnabled, value);
        }
        #endregion

        #region  Visibility окна приложения
        /// <summary> Visibility окна приложения</summary>
        private Visibility _Main_Window_Visibility = Visibility.Visible;

        public Visibility Main_Window_Visibility
        {
            get => _Main_Window_Visibility;
            set => Set(ref _Main_Window_Visibility, value);
        }
        #endregion

        #region Список директорий сбора файлов
        /// <summary>Список директорий сбора файлов</summary>
        private ObservableCollection<string> _Directory_List = new ObservableCollection<string> { };

        public ObservableCollection<string> Directory_List
        {
            get => _Directory_List;
            set => Set(ref _Directory_List, value);
        }
        #endregion

        #region Выбраний елемент в списке директории сбора файлов
        /// <summary>Выбраний елемент в списке директории сбора файлов</summary>
        private string _Directory_List_Selected_Item;

        public string Directory_List_Selected_Item
        {
            get => _Directory_List_Selected_Item;
            set 
            {
                Set(ref _Directory_List_Selected_Item, value);
                if (value != null)
                {
                    Deletor_IsEnabled = true;
                    if (InPut_Directory != null)
                    {
                        Replace_IsEnabled = true;
                    }
                    else
                    {
                        Replace_IsEnabled = false;
                    }

                }
                else
                {
                    Deletor_IsEnabled = false;
                    Replace_IsEnabled = false;
                }
            } 

        }
        #endregion

        #region Значение в фильтре сортировки
        /// <summary>Значение в фильтре сортировки</summary>
        private string _Type_Value;

        public string Type_Value
        {
            get => _Type_Value;
            set
            {
                Set(ref _Type_Value, value);
                Is_Enabled_Checker();
            }
        }
        #endregion

        #region Конечная директория
        /// <summary>Конечная директория</summary>
        private string _InPut_Directory;

        public string InPut_Directory
        {
            get => _InPut_Directory;
            set => Set(ref _InPut_Directory, value);
        }
        #endregion 
        #endregion

        #region Команды

        #region Закрыть
        public ICommand Close_App_Command { get; }

        private void On_Close_App_Command_Executed(object p)
        {
            Application.Current.Shutdown();
        }

        private bool Can_Close_App_Command_Executed(object p) => true;
        #endregion

        #region Свернуть
        public ICommand Collapse_App_Command { get; }

        private void On_Collapse_App_Command_Executed(object p)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private bool Can_Collapse_App_Command_Executed(object p) => true;
        #endregion

        #region Перетягивание
        public ICommand Drag_App_Command { get; }

        private void On_Drag_App_Command_Executed(object p)
        {
            Application.Current.MainWindow.DragMove();
        }

        private bool Can_Drag_App_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия OutPut_Directory
        public ICommand Browse_click_Command { get; }

        private void On_Browse_click_command_Executed(object p)
        {
            if (dialogService.OpenFileDialog_OutPut() == true)
            {
                if (Directory_List.Contains(dialogService.Directory) || dialogService.Directory == InPut_Directory)
                {
                    MessageBox.Show(dialogService.Exist_Message);
                }
                else
                {
                    Directory_List.Add(dialogService.Directory);
                }
            }
            Is_Enabled_Checker();
        }

        private bool Can_Browse_click_command_Executed(object p) => true;
        #endregion

        #region Событие нажатия InPut_Directory
        public ICommand InPut_Browse_click_Command { get; }

        private void On_InPut_Browse_click_Command_Executed(object p)
        {
            if (dialogService.OpenFileDialog_OutPut() == true)
            {
                if (Directory_List.Contains(dialogService.Directory))
                {
                    MessageBox.Show(dialogService.Exist_Message);
                }
                else
                {
                    InPut_Directory = dialogService.Directory;
                    if (Directory_List_Selected_Item != null)
                    {
                        Replace_IsEnabled = true;
                    }
                }
            }
            Is_Enabled_Checker();
        }

        private bool Can_InPut_Browse_click_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия SORT
        public ICommand Sort_click_Command { get; }

        private void On_Sort_click_Command_Executed(object p)
        {
            foreach (var item in Directory_List)
            {
                Source.SourseTOP(item, InPut_Directory, Type_Value);
            }
        }

        private bool Can_Sort_click_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия Non-Stop_SORT
        public ICommand Non_Stop_Sort_click_Command { get; }

        private async void On_Non_Stop_Sort_click_Command_Executed(object p)
        {
            while (Non_Stop_IsChecked == true)
            {
                foreach (var item in Directory_List)
                {
                    Source.SourseTOP(item, InPut_Directory, Type_Value);
                }
                await Task.Delay(1000);
            }
        }

        private bool Can_Non_Stop_Sort_click_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия Deletor (удаления вибраной директории)
        public ICommand Deletor_click_Command { get; }

        private void On_Deletor_click_Command_Executed(object p)
        {
            Directory_List.Remove(Directory_List_Selected_Item);
            if (Directory_List.Count == 0)
            {
                Browse_OutPut_IsEnabled = true;
            }
            Is_Enabled_Checker();
        }

        private bool Can_Deletor_click_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия Cleaner (очистка коллекции директорий)
        public ICommand Cleaner_click_Command { get; }

        private void On_Cleaner_click_Command_Executed(object p)
        {
            Directory_List.Clear();
        }

        private bool Can_Cleaner_click_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия Replace (замена выбраной директории сбора, и конечной директории)
        public ICommand Replace_click_Command { get; }

        private void On_Replace_click_Command_Executed(object p)
        {
            string Temp_InPut_Directory = InPut_Directory;
            string Temp2_Directory_List_Selected_Item = Directory_List_Selected_Item;
            int index = Directory_List.IndexOf(Directory_List_Selected_Item);
            Directory_List.RemoveAt(index);
            Directory_List.Insert(index, Temp_InPut_Directory);
            InPut_Directory = Temp2_Directory_List_Selected_Item;
        }

        private bool Can_Replace_click_Command_Executed(object p) => true;
        #endregion

        #region Событие нажатия SHADOW MODE (скрытие приложения в системный трей)
        public ICommand SHADOW_click_Command { get; }

        private void On_SHADOW_click_Command_Executed(object p)
        {
            Main_Window_Visibility = Visibility.Hidden;
            system_Tray_Application.NotifyIcon_On();
            if (Non_Stop_IsChecked != true)
            {
                system_Tray_Application.NotifyIconContextMenu.MenuItems[1].Enabled = false;
                system_Tray_Application.NotifyIconContextMenu.MenuItems[1].Checked = true;
            }
            else
            {
                system_Tray_Application.NotifyIconContextMenu.MenuItems[1].Enabled = true;
                system_Tray_Application.NotifyIconContextMenu.MenuItems[1].Checked = false;
            }
        }

        private bool Can_SHADOW_click_Command_Executed(object p) => true;
        #endregion

        #endregion

        #region Методы
        private void Open_Event(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            system_Tray_Application.NotifyIcon_Off();
        }

        private void Stop_Sort_Event(object sender, EventArgs e)
        {
            Non_Stop_IsChecked = false;
            system_Tray_Application.NotifyIconContextMenu.MenuItems[1].Enabled = false;
            system_Tray_Application.NotifyIconContextMenu.MenuItems[1].Checked = true;
        }

        private void Close_Event(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Is_Enabled_Checker()
        {
            if (Directory_List.Count != 0 && Type_Value != "" && InPut_Directory != "")
            {
                Non_Stop_IsEnabled = true;
                Sort_IsEnabled = true;
            }
            else
            {
                Non_Stop_IsEnabled = false;
                Sort_IsEnabled = false;
            }
        }

        private void Directory_List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Directory_List.Count == 0)
            {
                Browse_OutPut_IsEnabled = true;
                Creator_Browse_OutPut_IsEnabled = false;
                Cleaner_IsEnabled = false;
            }
            else
            {
                Browse_OutPut_IsEnabled = false;
                Creator_Browse_OutPut_IsEnabled = true;
                Cleaner_IsEnabled = true;
            }
        }
        #endregion

        public MainWindow_ViewModel()
        {
            #region Команды

            Close_App_Command = new LambdaCommand(On_Close_App_Command_Executed, Can_Close_App_Command_Executed);

            Collapse_App_Command = new LambdaCommand(On_Collapse_App_Command_Executed, Can_Collapse_App_Command_Executed);

            Drag_App_Command = new LambdaCommand(On_Drag_App_Command_Executed, Can_Drag_App_Command_Executed);

            Browse_click_Command = new LambdaCommand(On_Browse_click_command_Executed, Can_Browse_click_command_Executed);

            InPut_Browse_click_Command = new LambdaCommand(On_InPut_Browse_click_Command_Executed, Can_InPut_Browse_click_Command_Executed);

            Sort_click_Command = new LambdaCommand(On_Sort_click_Command_Executed, Can_Sort_click_Command_Executed);

            Non_Stop_Sort_click_Command = new LambdaCommand(On_Non_Stop_Sort_click_Command_Executed, Can_Non_Stop_Sort_click_Command_Executed);

            Deletor_click_Command = new LambdaCommand(On_Deletor_click_Command_Executed, Can_Deletor_click_Command_Executed);

            Cleaner_click_Command = new LambdaCommand(On_Cleaner_click_Command_Executed, Can_Cleaner_click_Command_Executed);

            Replace_click_Command = new LambdaCommand(On_Replace_click_Command_Executed, Can_Replace_click_Command_Executed);

            SHADOW_click_Command = new LambdaCommand(On_SHADOW_click_Command_Executed, Can_SHADOW_click_Command_Executed);
            #endregion

            #region NotifyIcon
            system_Tray_Application.NotifyIconContextMenu_Add_new_Item("Open", Open_Event);
            system_Tray_Application.NotifyIconContextMenu_Add_new_Item("Stop sort", Stop_Sort_Event);
            system_Tray_Application.NotifyIconContextMenu_Add_new_Item("Close", Close_Event);
            #endregion

            Directory_List.CollectionChanged += Directory_List_CollectionChanged;
            
        }
    }
}
