using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Core.ResourceManagers;
using System;
using Data.Repositories;
using Data.DataProviders.Players;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Processes.Events
{
    //Todo: This event is obsolete. Remove when there are at least a few actual events to use
    public class IncreaseScoreEvent : Event
    {
        private int ScoreAmount { get; set; }
        private Player currentPlayer { get; set; }

        private PlayerRepository repo;

        #region Rules
        private const int MaxScore = 10;
        private const EventTargets ValidScoreTargets = EventTargets.Player;
        Predicate<Player> ScoreCanIncrease { get; set; }
        #endregion

        public IncreaseScoreEvent(string[] parts, IServiceProvider sp)
        {
            currentPlayer = new Player(parts[0]);
            ScoreAmount = int.Parse(parts[1]);
            ScoreCanIncrease = (i) => i.Score + ScoreAmount <= MaxScore;
            repo = sp.GetService<PlayerRepository>();
        }

        protected override ReadonlyEvent GatherData()
        {
            if(ScoreCanIncrease(currentPlayer))
            {
                Result.Deltas.Add(new Delta { Actor = currentPlayer, Key = "Score", Value = ScoreAmount.ToString(), Targets = repo.Get(Result) });
                Result.Actor = currentPlayer;
                Result.Targets = ValidScoreTargets;
                Result.Resolution = EventResolutionType.Commit;
            }

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            //TODO: Handle locking / writing to resource 

            var player = repo.Get(currentPlayer.Id);
            player.Score = player.Score + ScoreAmount;
            return this;
        }

        protected override Event Persist()
        {
            throw new NotImplementedException();   
        }
    }
}