﻿using Core.Entities;
using Core.Entities.Humans;
using Core.EventResolution;
using Core.ResourceManagers;
using System;

namespace Core.Events
{
    //Todo: This event is obsolete. Remove when there are at least a few actual events to use
    public class IncreaseScoreEvent : Event
    {
        private int ScoreAmount { get; set; }
        private Player currentPlayer { get; set; }

        #region Rules
        private const int MaxScore = 10;
        private const EventTargets ValidScoreTargets = EventTargets.Player;
        Predicate<Player> ScoreCanIncrease { get; set; }
        #endregion

        public IncreaseScoreEvent(string[] parts)
        {
            currentPlayer = new Player(parts[1]);
            ScoreAmount = int.Parse(parts[2]);
            ScoreCanIncrease = (i) => i.Score + ScoreAmount <= MaxScore;
        }

        protected override Event Dispatch()
        {
            if(ScoreCanIncrease(currentPlayer))
            {
                Result.Deltas.Add(new Delta { Actor = currentPlayer, Key = "Score", Value = ScoreAmount.ToString(), Targets = ResourceLocator.GetPlayers(Result) });
                Result.Actor = currentPlayer;
                Result.Targets = ValidScoreTargets;
                Result.Resolution = EventResolutionType.Commit;
            }

            return this;
        }

        protected override Event Resolve()
        {
            //TODO: Handle locking / writing to resource 
            var player = ResourceLocator.GetPlayer(currentPlayer.Id.Trunk);
            player.Score = player.Score + ScoreAmount;
            return this;
        }

        protected override Event Persist()
        {
            throw new NotImplementedException();   
        }
    }
}