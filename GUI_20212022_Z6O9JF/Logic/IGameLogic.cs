﻿using GUI_20212022_Z6O9JF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI_20212022_Z6O9JF.Logic
{
    public interface IGameLogic
    {
        List<Faction> AvailableFactions { get; set; }
        int ClientID { get; set; }
        HexagonTile[,] GameMap { get; set; }
        string Map { get; set; }
        ObservableCollection<Player> Players { get; set; }
        HexagonTile SelectedHexagonTile { get; set; }

        void AddUnit();
        void AddVillage();
        void DecreaseMoves();
        HexagonTile[,] GameMapSetup(string path);
        void GetResources();
        void MoveUnit(HexagonTile hexagonTile);
        List<Quest> RandomQuestSelector(int n);
        void ReadQuests();
        void ReloadHexagonObjects();
        void ResetMoves();
        void SelectableFactions();
        void UpgradeVillage();
        void ChooseOffer();
    }
}