using Core;
using Core.Entities;
using Core.Entities.Humans;
using Core.ResourceManagers;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Player _player;
        private Engine _engine = Engine.Instance;

        public Form1()
        {
            InitializeComponent();

            _player = new Player("P1234567890123456789/PlayerMcPlayerface/0");
        }

        private void StartEngine_Click(object sender, EventArgs e)
        {
            //_engine.StartEngine();
            //_engine.Subscribe(_player);
        }

        private void StopEngine_Click(object sender, EventArgs e)
        {
            //_engine.StopEngine();
            _engine.Unsubscribe(_player);
        }

        private void Action_Click(object sender, EventArgs e)
        {
            //Ok, we have pushed a button and want to get 1 point
            _engine.Subscribe(_player);

            //Obsolete(?) increase score event.
            //_engine.Push("IncreaseScore;" + _player.Id + "/" + _player.Name + "/" + _player.Score +";1" );

            //We know because of fulhax that this id is a monsterid that we are attacking //M1234567891234567890
            _engine.Push("Attack;" + "P1234567890123456789;M1234567891234567890");
            
            _engine.synchCallForPeasants();
            //_engine.Unsubscribe(_player);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var p = ResourceLocator.GetPlayer(_player.Id.Trunk);
            var m = ResourceLocator.GetMonster("1234567891234567890");

            CurrentScore.Text = p.Score.ToString();
        }
    }
}
