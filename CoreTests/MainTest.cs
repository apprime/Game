using Core;
using Core.Entities.Humans;
using Core.ResourceManagers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MainTest
    {
        private Player _player;

        [TestMethod]
        public void StartAndPushEvent()
        {
            _player = new Player("TROLOLO/AdmiralAardwark/0");
            var _engine = Engine.Instance;

            _engine.Subscribe(_player);

            var p = ResourceLocator.GetPlayer(_player.Id.Trunk);
            var preScore = p.Score;

            _engine.Push(_player.Id + "/" + _player.Name + "/" + _player.Score + ";IncreaseScore");
            _engine.synchCallForPeasants();
            var postScore = p.Score;

            Assert.AreNotEqual(preScore, postScore);
            Assert.IsTrue(postScore == 1);
        }
    }
}
