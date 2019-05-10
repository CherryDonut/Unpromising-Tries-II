using System;
using ConsoleApp6;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BallTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicJump()
        {
        //    var area = new GameForm();
        //    var lvl = new Level();
        //    var ballTop = new WonderBall(new Point(200, 200), area.sizeOfBlock - 10, 0.9);
        //    var ballBot = new WonderBall(new Point(200, 400), area.sizeOfBlock - 10, 0.9);

        //    for (var i = 0; i < (area.width / area.sizeOfBlock); i++)
        //        lvl.FirstFloor.Add(new Cell(true, false, Brushes.Brown));
        //    for (var j = 0; j < (area.width / area.sizeOfBlock); j++)
        //        lvl.SecondFloor.Add(new Cell(true, false, Brushes.Brown));

        //    //Assert.AreEqual(Math.Abs(area.height - 350 - ballTop.Y - area.sizeOfBlock / 2) < 10 && lvl.SecondFloor[ballTop.X / area.sizeOfBlock].IsBlock, true);           
        }

        [TestMethod]
        public void BasicGoThrough()
        {

        }

        [TestMethod]
        public void BasicDying()
        {

        }

        [TestMethod]
        public void DieFromAbove()
        {

        }

        [TestMethod]
        public void PounceFromCeiling()
        {

        }

        [TestMethod]
        public void GetThroughCeiling()
        {

        }

    }
}
