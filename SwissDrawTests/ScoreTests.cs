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
            Match[] result2 = new Match[20];
            result2[0] = new Match(1, 2);
            result2[0].Result = 1;
            result2[1] = new Match(3, 4);
            result2[1].Result = 1;
            result2[2] = new Match(5, 6);
            result2[2].Result = 1;
            result2[3] = new Match(7, 8);
            result2[3].Result = 1;
            result2[4] = new Match(9, 10);
            result2[4].Result = 1;
            result2[5] = new Match(11, 12);
            result2[5].Result = 1;
            result2[6] = new Match(13, 14);
            result2[6].Result = 1;
            result2[7] = new Match(15, 16);
            result2[7].Result = 1;
            result2[8] = new Match(17, 18);
            result2[8].Result = 1;
            result2[9] = new Match(19, 20);
            result2[9].Result = 1;

            result2[10] = new Match(1, 3);
            result2[10].Result = 1;
            result2[11] = new Match(2, 4);
            result2[11].Result = 1;
            result2[12] = new Match(5, 7);
            result2[12].Result = 1;
            result2[13] = new Match(6, 8);
            result2[13].Result = 1;
            result2[14] = new Match(9, 11);
            result2[14].Result = 1;
            result2[15] = new Match(10, 12);
            result2[15].Result = 1;
            result2[16] = new Match(13, 15);
            result2[16].Result = 1;
            result2[17] = new Match(14, 16);
            result2[17].Result = 1;
            result2[18] = new Match(17, 19);
            result2[18].Result = 1;
            result2[19] = new Match(18, 20);
            result2[19].Result = 1;

            var score2 = Score.CalcScore(result2);
            Assert.AreEqual(2, score2[1].winCount); //2, 3
            Assert.AreEqual(1, score2[2].winCount); //4
            Assert.AreEqual(1, score2[3].winCount); //4
            Assert.AreEqual(0, score2[4].winCount); 
            Assert.AreEqual(2, score2[5].winCount); //6, 7
            Assert.AreEqual(1, score2[6].winCount); //8
            Assert.AreEqual(1, score2[7].winCount); //8
            Assert.AreEqual(0, score2[8].winCount);
            Assert.AreEqual(2, score2[9].winCount); //10, 11
            Assert.AreEqual(1, score2[10].winCount); //12
            Assert.AreEqual(1, score2[11].winCount); //12
            Assert.AreEqual(0, score2[12].winCount); 
            Assert.AreEqual(2, score2[13].winCount); //14, 15
            Assert.AreEqual(1, score2[14].winCount); //16
            Assert.AreEqual(1, score2[15].winCount); //16
            Assert.AreEqual(0, score2[16].winCount); 
            Assert.AreEqual(2, score2[17].winCount); //18, 19
            Assert.AreEqual(1, score2[18].winCount); //20
            Assert.AreEqual(1, score2[19].winCount); //20
            Assert.AreEqual(0, score2[20].winCount); 


            Assert.AreEqual(1 + 1, score2[1].score); //2, 3
            Assert.AreEqual(0, score2[2].score);     //4   
            Assert.AreEqual(0, score2[3].score);     //4   
            Assert.AreEqual(0, score2[4].score);    
            Assert.AreEqual(1 + 1, score2[5].score); //6, 7
            Assert.AreEqual(0, score2[6].score);     //8
            Assert.AreEqual(0, score2[7].score);     //8
            Assert.AreEqual(0, score2[8].score);        
            Assert.AreEqual(1 + 1, score2[9].score); //10, 11
            Assert.AreEqual(0, score2[10].score);    //12
            Assert.AreEqual(0, score2[11].score);    //12
            Assert.AreEqual(0, score2[12].score);     
            Assert.AreEqual(1 + 1, score2[13].score); //14, 15
            Assert.AreEqual(0, score2[14].score);     //16   
            Assert.AreEqual(0, score2[15].score);     //16  
            Assert.AreEqual(0, score2[16].score);
            Assert.AreEqual(1 + 1, score2[17].score); //18, 19
            Assert.AreEqual(0, score2[18].score);     //20
            Assert.AreEqual(0, score2[19].score);     //20
            Assert.AreEqual(0, score2[20].score);     
        }

        public void CalcScoreTest02()
        {
            Match[] result2 = new Match[6];
        }
    }
}