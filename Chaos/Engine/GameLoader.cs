﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Chaos.Engine;
using Chaos.Interfaces;
using Chaos.Model.DTOs;
using ExtendedXmlSerialization;

namespace Chaos.Model
{
    internal class GameLoader : IFile
    {
        private readonly ExtendedXmlSerializer xml = new ExtendedXmlSerializer();
        private readonly List<Monster> LoadedMonsters = new List<Monster>();

        private readonly List<Player> LoadedPlayers = new List<Player>();

        public string GetPath()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;

            return "";
        }

        public void LoadGame()
        {
            var state = LoadData();
            if (state != null)
            {
                LoadPlayers(state);
                LoadMonsters(state);

                var game = new GameForm();
                var gameboard = new Gameboard(game.GetGamePanel, game.GetNameField, game.GetMovesLeftLabel);
                var gameEngine = new GameEngine(LoadedPlayers.Count - 1, gameboard, game, false);

                gameEngine.GetPlayers = LoadedPlayers;
                gameEngine.CurrentPlayer = gameEngine.GetPlayers[0];

                var spellboard = new SpellBoard(game.GetSpellPanel, LoadedPlayers, gameEngine, 98, false);
                game.engine = gameEngine;
                game.gameboard = gameboard;
                game.spellboard = spellboard;
                gameEngine.spellboard = spellboard;
                gameEngine.InitializeEngineElements(true);

                var tiles = (Tile[]) gameboard.GetElementsCollection();
                foreach (var monster in LoadedMonsters)
                {
                    var mCoords = monster.coordinates;
                    foreach (var t in tiles)
                        if (t.GetCoordinates() == mCoords)
                            t.SetOccupant(monster);
                }
                //foreach (Tile tile in gameboard.GetElementsCollection())
                //{
                //    var coords = tile.GetCoordinates();
                //    var occupant = LoadedMonsters.Find(x => x.coordinates == coords);
                //    if (occupant != null)
                //    {
                //        tile.SetOccupant(occupant);
                //    }
                //}
                game.Show();
            }
        }

        private string DeserializedData()
        {
            var data = File.ReadAllText(GetPath());
            if (!string.IsNullOrEmpty(data))
                return data;

            throw new IOException();
        }

        private GameState LoadData()
        {
            string data = null;
            try
            {
                data = DeserializedData();
            }
            catch (Exception ex)
            {
                return null;
            }

            return xml.Deserialize<GameState>(data);
        }

        private void LoadPlayers(GameState state)
        {
            var generator = new SpellsGenerator();
            foreach (var dto in state.players)
            {
                var playerFromDTO = new Player(dto.Name);
                var playerIndex = state.players.IndexOf(dto);
                foreach (var spellName in state.players[playerIndex].Spells)
                    playerFromDTO.AvailableSpells.Add(generator.GetSpellByName(spellName));

                LoadedPlayers.Add(playerFromDTO);
            }
        }

        private void LoadMonsters(GameState state)
        {
            var temp = new List<Monster>();
            var generator = new MonsterGenerator();
            foreach (var dto in state.monsters)
            {
                var owner = LoadedPlayers.Find(x => x.Name == dto.Owner);
                var monsterFromDTO = new Monster();
                if (dto.Name == "Wizard")
                    dto.Name = "Wizard" + owner.Name.Substring(owner.Name.Length - 1);
                monsterFromDTO = generator.GetMonsterByName(dto.Name, owner);
                monsterFromDTO.Moves = dto.Moves;
                monsterFromDTO.MovesRemaining = dto.MovesRemaining;
                monsterFromDTO.Attack = dto.Attack;
                monsterFromDTO.Health = dto.Health;
                monsterFromDTO.MaxHealth = dto.MaxHealth;
                monsterFromDTO.coordinates = dto.Coordinates;

                LoadedMonsters.Add(monsterFromDTO);
            }
        }
    }
}