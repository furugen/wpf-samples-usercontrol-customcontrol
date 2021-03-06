﻿using Infragistics.Undo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpf_samples_undoredo
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public UndoManager UndoManager { get; set; }

        public MainWindowViewModel()
        {
            // 編集履歴を記録する対象に当ViewModelを指定します。
            this.UndoManager = new UndoManager();
            this.UndoManager.RegisterReference(this);
        }
     

        public void ShowHistory()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var historyItem in this.UndoManager.UndoHistory)
            {
                sb.AppendLine(historyItem.ShortDescription);
            }
            this.UndoHistory = sb.ToString();
            sb.Clear();

            foreach (var historyItem in this.UndoManager.RedoHistory)
            {
                sb.AppendLine(historyItem.ShortDescription);
            }
            this.RedoHistory = sb.ToString();
        }

        #region 各プロパティ
        private string textBoxValue = "";
        public string TextBoxValue
        {
            get
            {
                return this.textBoxValue;
            }
            set
            {
                this.SetProperty(ref textBoxValue, value);
            }
        }

        private DateTime? datePickerData;
        public DateTime? DatePickerData
        {
            get
            {
                return this.datePickerData;
            }
            set
            {
                this.SetProperty(ref datePickerData, value);
            }
        }

        private string redoHistory = "";
        public string RedoHistory
        {
            get
            {
                return this.redoHistory;
            }
            set
            {
                if(redoHistory != value)
                {
                    redoHistory = value;
                    NotifyPropertyChanged();
                }
                
            }
        }
        private string undoHistory = "";
        public string UndoHistory
        {
            get
            {
                return this.undoHistory;
            }
            set
            {
                if (undoHistory != value)
                {
                    undoHistory = value;
                    NotifyPropertyChanged();
                }

            }
        }

        private int selectedIndexOfListView;
        public int SelectedIndexOfListView
        {
            get
            {
                return this.selectedIndexOfListView;
            }
            set
            {
                this.SetProperty(ref selectedIndexOfListView, value);
            }
        }

        private int selectedIndexOfComboBox;
        public int SelectedIndexOfComboBox
        {
            get
            {
                return this.selectedIndexOfComboBox;
            }
            set
            {
                this.SetProperty(ref selectedIndexOfComboBox, value);
            }
        }

        private bool? isCheckedOfCheckBox = true;
        public bool? IsCheckedOfCheckBox
        {
            get
            {
                return this.isCheckedOfCheckBox;
            }
            set
            {
                this.SetProperty(ref isCheckedOfCheckBox, value);
            }
        }

        private bool? isCheckedOfRadioOnAttached = false;
        public bool? IsCheckedOfRadioOnAttached
        {
            get
            {
                return this.isCheckedOfRadioOnAttached;
            }
            set
            {
                this.SetProperty(ref isCheckedOfRadioOnAttached, value);
            }
        }

        private bool? isCheckedOfRadioNonAttached = true;
        public bool? IsCheckedOfRadioNonAttached
        {
            get
            {
                return this.isCheckedOfRadioNonAttached;
            }
            set
            {
                this.SetProperty(ref isCheckedOfRadioNonAttached, value);
            }
        }
        #endregion


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            // UndoManagerに変更値を登録
            UndoManager.FromReference(this).AddPropertyChange(this, propertyName, storage, value);
            // UndoRedoの履歴を表示
            this.ShowHistory();

            storage = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
