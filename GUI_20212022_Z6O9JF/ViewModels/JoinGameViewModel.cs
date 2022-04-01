﻿using GUI_20212022_Z6O9JF.Logic;
using GUI_20212022_Z6O9JF.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212022_Z6O9JF.ViewModels
{
    public class JoinGameViewModel : ObservableRecipient
    {
        public IGameLogic gameLogic;
        public ICommand BackCommand { get; set; }
        public ICommand GameCommand { get; set; }
        public ICommand NextFaction { get; set; }
        public ICommand PreviousFaction { get; set; }
        int index = 0;
        public Faction SelectedFaction { get { return gameLogic.AvailableFactions[index]; } }
        public static bool IsInDesignMode
        {
            get
            {
                return
                    (bool)DependencyPropertyDescriptor
                    .FromProperty(DesignerProperties.
                    IsInDesignModeProperty,
                    typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }
        public JoinGameViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IGameLogic>()) { }
        public JoinGameViewModel(IGameLogic gameLogic)
        {
            this.gameLogic = gameLogic;

            //gameLogic.ClientConnect();
            gameLogic.GameMap = gameLogic.GameMapSetup($"Maps/map{gameLogic.Map}.txt");

            string name = "bercike";

            GameCommand = new RelayCommand(() => gameLogic.ChampSelect(SelectedFaction, name));
            NextFaction = new RelayCommand(() =>
            {
                if (gameLogic.AvailableFactions.Count - 1 > index)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                OnPropertyChanged("SelectedFaction");
            });
            PreviousFaction = new RelayCommand(() =>
            {
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = gameLogic.AvailableFactions.Count - 1;
                }
                OnPropertyChanged("SelectedFaction");
            });

            Messenger.Register<JoinGameViewModel, string, string>(this, "Base", (recipient, msg) =>
            {
                OnPropertyChanged("SelectedFaction");
            });
        }
    }
}
