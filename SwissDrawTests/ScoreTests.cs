using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwissDraw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissDraw.Tests
{
    [TestClass()]
    public class ScoreTests
    {
        [TestMethod()]
        public void CalcWinClontTest()
        {
            Match[] result = new Match[3];
            result[0] = new Match(1, 3);
            result[0].Result = 1;
            result[1] = new Match(2, 5);
            result[1].Result = 1;
            result[2] = new Match(4, 6);
            result[2].Result = 1;

            var score = Score.CalcScore(result);
            Assert.AreEqual(1, score[1].winCount);
            Assert.AreEqual(1, score[2].winCount);
            Assert.AreEqual(1, score[4].winCount);
            Assert.AreEqual(0, score[3].winCount);
            Assert.AreEqual(0, score[5].winCount);
            Assert.AreEqual(0, score[6].winCount);

            Match[] result2 = new Match[6];
            result2[0] = new Match(1, 3);
            result2[0].Result = 1;
            result2[1] = new Match(2, 5);
            result2[1].Result = 1;
            result2[2] = new Match(4, 6);
            result2[2].Result = 1;
            result2[3] = new Match(1, 2);
            result2[3].Result = 1;
            result2[4] = new Match(4, 5);
            result2[4].Result = 1;
            result2[5] = new Match(3, 6);
            result2[5].Result = 1;

            var score2 = Score.CalcScore(result2);
            Assert.AreEqual(2, score2[1].winCount);
            Assert.AreEqual(1, score2[2].winCount);
            Assert.AreEqual(1, score2[3].winCount);
            Assert.AreEqual(2, score2[4].winCount);
            Assert.AreEqual(0, score2[5].winCount);
            Assert.AreEqual(0, score2[6].winCount);


        }

        [TestMethod()]
        public void CalcScoreTest()
        {
            Match[] result2 = new Match[6];
            result2[0] = new Match(1, 3);
            result2[0].Result = 1;
            result2[1] = new Match(2, 5);
            result2[1].Result = 1;
            result2[2] = new Match(4, 6);
            result2[2].Result = 1;
            result2[3] = new Match(1, 2);
            result2[3].Result = 1;
            result2[4] = new Match(4, 5);
            result2[4].Result = 1;
            result2[5] = new Match(3, 6);
            result2[5].Result = 1;

            var score2 = Score.CalcScore(result2);
            Assert.AreEqual(2, score2[1].winCount);
            Assert.AreEqual(1, score2[2].winCount);
            Assert.AreEqual(1, score2[3].winCount);
            Assert.AreEqual(2, score2[4].winCount);
            Assert.AreEqual(0, score2[5].winCount);
            Assert.AreEqual(0, score2[6].winCount);

            Assert.AreEqual(1 + 1, score2[1].score);    //3 , 2
            Assert.AreEqual(0, score2[2].score);        //5
            Assert.AreEqual(0, score2[3].score);        //6
            Assert.AreEqual(0 + 0, score2[4].score);    //5 , 6
            Assert.AreEqual(0, score2[5].score);
            Assert.AreEqual(0, score2[6].score);
        }
    }
}