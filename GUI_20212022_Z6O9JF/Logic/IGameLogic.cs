﻿using GUI_20212022_Z6O9JF.Models;
using System.Collections.ObjectModel;

namespace GUI_20212022_Z6O9JF.Logic
{
    public interface IGameLogic
    {
        GameLogic.FieldType[,] GameMap { get; set; }
        string Map { get; set; }
        ObservableCollection<Player> Players { get; set; }

        void ClientSetup();
        GameLogic.FieldType[,] GameMapSetup(string path);
        ObservableCollection<Player> Setup();
    }
}