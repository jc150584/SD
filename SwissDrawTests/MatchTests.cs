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
    public class MatchTests
    {
        [TestMethod()]
        public void MatchTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void MakeMatchTest01()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });

            //1回戦
            Match[] matches = Match.MakeMatch(persons, new Match[0]);
            Assert.AreEqual(3, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(4, matches[0].Person2);//1 vs 4
            Assert.AreEqual(2, matches[1].Person1);
            Assert.AreEqual(5, matches[1].Person2);//2 vs 5
            Assert.AreEqual(3, matches[2].Person1);
            Assert.AreEqual(6, matches[2].Person2);//3 vs 6

            matches[0].Result = 1;// 1
            matches[1].Result = 2;// 5
            matches[2].Result = 2;// 6

            //2回戦(1勝同士、1勝と0勝、0勝同士)
            Match[] matches2 = Match.MakeMatch(persons, matches);
            Assert.AreEqual(3, matches2.Length);
            Assert.AreEqual(1, matches2[0].Person1);
            Assert.AreEqual(5, matches2[0].Person2);//1 vs 5
            Assert.AreEqual(6, matches2[1].Person1);
            Assert.AreEqual(2, matches2[1].Person2);//6 vs 2
            Assert.AreEqual(3, matches2[2].Person1);
            Assert.AreEqual(4, matches2[2].Person2);//3 vs 4

            matches2[0].Result = 1;//1
            matches2[1].Result = 1;//6
            matches2[2].Result = 1;//3

            var result = Match.MergeMatch(matches, matches2);

            //3回戦(2勝同士、1勝同士、0勝同士)
            Match[] matches3 = Match.MakeMatch(persons, result);
            Assert.AreEqual(3, matches3.Length);
            Assert.AreEqual(1, matches3[0].Person1);
            Assert.AreEqual(6, matches3[0].Person2);//1 vs 6
            Assert.AreEqual(3, matches3[1].Person1);
            Assert.AreEqual(5, matches3[1].Person2);//3 vs 5
            Assert.AreEqual(2, matches3[2].Person1);
            Assert.AreEqual(4, matches3[2].Person2);//2 vs 4
        }

        [TestMethod()]
        public void MakeMatchTest02()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });

            //1回戦
            Match[] matches = Match.MakeMatch(persons, new Match[0]);
            Assert.AreEqual(3, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(4, matches[0].Person2);//1 vs 4
            Assert.AreEqual(2, matches[1].Person1);
            Assert.AreEqual(5, matches[1].Person2);//2 vs 5
            Assert.AreEqual(3, matches[2].Person1);
            Assert.AreEqual(6, matches[2].Person2);//3 vs 6

            matches[0].Result = 1;//1
            matches[1].Result = 2;//5
            matches[2].Result = 2;//6

            //2回戦
            Match[] matches2 = Match.MakeMatch(persons, matches);
            Assert.AreEqual(3, matches2.Length);
            Assert.AreEqual(1, matches2[0].Person1);
            Assert.AreEqual(5, matches2[0].Person2);//1 vs 5
            Assert.AreEqual(6, matches2[1].Person1);
            Assert.AreEqual(2, matches2[1].Person2);//6 vs 2
            Assert.AreEqual(3, matches2[2].Person1);
            Assert.AreEqual(4, matches2[2].Person2);//3 vs 4

            matches2[0].Result = 2;//5
            matches2[1].Result = 2;//2
            matches2[2].Result = 1;//3

            var result = Match.MergeMatch(matches, matches2);

            //3回戦(2勝と1勝、1勝同士、1勝と0勝)
            Match[] matches3 = Match.MakeMatch(persons, result);
            Assert.AreEqual(3, matches3.Length);
            Assert.AreEqual(5, matches3[0].Person1);
            Assert.AreEqual(3, matches3[0].Person2);//5 vs 3
            Assert.AreEqual(1, matches3[1].Person1);
            Assert.AreEqual(6, matches3[1].Person2);//1 vs 6
            Assert.AreEqual(2, matches3[2].Person1);
            Assert.AreEqual(4, matches3[2].Person2);//2 vs 4
        }

        [TestMethod()]
        public void MergeMatchTest()
        {
            Match[] matches1 = new Match[1];
            matches1[0] = new Match(1, 2);

            Match[] matches2 = new Match[2];
            matches2[0] = new Match(2, 3);
            matches2[1] = new Match(1, 4);

            var result1 = Match.MergeMatch(matches1, new Match[0]);
            Assert.AreEqual(1, result1.Length);

            var result2 = Match.MergeMatch(matches1, matches2);
            Assert.AreEqual(3, result2.Length);
        }

        public void MakeMatchTest03()
        {

            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "A", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "A", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "B", PersonName = "加藤" });
            persons.Add(7, new Person { LotNumber = 7, PersonGroup = "B", PersonName = "佐藤" });
            persons.Add(8, new Person { LotNumber = 8, PersonGroup = "B", PersonName = "佐藤" });
            persons.Add(9, new Person { LotNumber = 9, PersonGroup = "B", PersonName = "鈴木" });
            persons.Add(10, new Person { LotNumber = 10, PersonGroup = "B", PersonName = "高橋" });
            persons.Add(11, new Person { LotNumber = 11, PersonGroup = "C", PersonName = "石橋" });
            persons.Add(12, new Person { LotNumber = 12, PersonGroup = "C", PersonName = "仁一" });
            persons.Add(13, new Person { LotNumber = 13, PersonGroup = "C", PersonName = "和也" });
            persons.Add(14, new Person { LotNumber = 14, PersonGroup = "C", PersonName = "古山" });
            persons.Add(15, new Person { LotNumber = 15, PersonGroup = "C", PersonName = "澤田" });
            persons.Add(16, new Person { LotNumber = 16, PersonGroup = "D", PersonName = "山口" });
            persons.Add(17, new Person { LotNumber = 17, PersonGroup = "D", PersonName = "井上" });
            persons.Add(18, new Person { LotNumber = 18, PersonGroup = "D", PersonName = "木村" });
            persons.Add(19, new Person { LotNumber = 19, PersonGroup = "D", PersonName = "林" });
            persons.Add(20, new Person { LotNumber = 20, PersonGroup = "D", PersonName = "清水" });


            //1回戦
            Match[] matches = Match.MakeMatch(persons, new Match[0]);
            Assert.AreEqual(10, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(6, matches[0].Person2);//1-6
            Assert.AreEqual(2, matches[1].Person1);
            Assert.AreEqual(7, matches[1].Person2);//2-7
            Assert.AreEqual(3, matches[2].Person1);
            Assert.AreEqual(8, matches[2].Person2);//3-8
            Assert.AreEqual(4, matches[3].Person1);
            Assert.AreEqual(9, matches[3].Person2);//4-9
            Assert.AreEqual(5, matches[4].Person1);
            Assert.AreEqual(10, matches[4].Person2);//5-10
            Assert.AreEqual(11, matches[5].Person1);
            Assert.AreEqual(16, matches[5].Person2);//11-16
            Assert.AreEqual(12, matches[6].Person1);
            Assert.AreEqual(17, matches[6].Person2);//12-17
            Assert.AreEqual(13, matches[7].Person1);
            Assert.AreEqual(18, matches[7].Person2);//13-18
            Assert.AreEqual(14, matches[8].Person1);
            Assert.AreEqual(19, matches[8].Person2);//14-19
            Assert.AreEqual(15, matches[9].Person1);
            Assert.AreEqual(20, matches[9].Person2);//15-20


            matches[0].Result = 1;// 1win
            matches[1].Result = 2;// 7win 
            matches[2].Result = 1;// 3win
            matches[3].Result = 2;// 9win
            matches[4].Result = 1;// 5win
            matches[5].Result = 1;// 11win
            matches[6].Result = 2;// 17win
            matches[7].Result = 1;// 13win
            matches[8].Result = 2;// 19win
            matches[9].Result = 1;// 15win

            //2回戦
            Match[] matches2 = Match.MakeMatch(persons, matches);

            Assert.AreEqual(10, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(6, matches[0].Person2);//1-7
            Assert.AreEqual(2, matches[1].Person1);
            Assert.AreEqual(7, matches[1].Person2);//3-9
            Assert.AreEqual(3, matches[2].Person1);
            Assert.AreEqual(8, matches[2].Person2);//5-11
            Assert.AreEqual(4, matches[3].Person1);
            Assert.AreEqual(9, matches[3].Person2);//13-17
            Assert.AreEqual(5, matches[4].Person1);
            Assert.AreEqual(10, matches[4].Person2);//5-19
            Assert.AreEqual(11, matches[5].Person1);
            Assert.AreEqual(16, matches[5].Person2);//2-6
            Assert.AreEqual(12, matches[6].Person1);
            Assert.AreEqual(17, matches[6].Person2);//4-8
            Assert.AreEqual(13, matches[7].Person1);
            Assert.AreEqual(18, matches[7].Person2);//10-12
            Assert.AreEqual(14, matches[8].Person1);
            Assert.AreEqual(19, matches[8].Person2);//14-16
            Assert.AreEqual(15, matches[9].Person1);
            Assert.AreEqual(20, matches[9].Person2);//18-20

            var result = Match.MergeMatch(matches, matches2);

            matches[0].Result = 1;// 1win
            matches[1].Result = 2;// 7win 
            matches[2].Result = 1;// 3win
            matches[3].Result = 1;// 9win
            matches[4].Result = 1;// 5win
            matches[5].Result = 1;// 11win
            matches[6].Result = 1;// 17win
            matches[7].Result = 2;// 13win
            matches[8].Result = 2;// 19win
            matches[9].Result = 2;// 15win

            //3回戦
            Match[] matches3 = Match.MakeMatch(persons, matches2);

            Assert.AreEqual(10, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(6, matches[0].Person2);//1-9
            Assert.AreEqual(2, matches[1].Person1);
            Assert.AreEqual(7, matches[1].Person2);//5-13
            Assert.AreEqual(3, matches[2].Person1);
            Assert.AreEqual(8, matches[2].Person2);//15-20
            Assert.AreEqual(4, matches[3].Person1);
            Assert.AreEqual(9, matches[3].Person2);//2-7
            Assert.AreEqual(5, matches[4].Person1);
            Assert.AreEqual(10, matches[4].Person2);//3-11
            Assert.AreEqual(11, matches[5].Person1);
            Assert.AreEqual(16, matches[5].Person2);//4-12
            Assert.AreEqual(12, matches[6].Person1);
            Assert.AreEqual(17, matches[6].Person2);//16-17
            Assert.AreEqual(13, matches[7].Person1);
            Assert.AreEqual(18, matches[7].Person2);//6-14
            Assert.AreEqual(14, matches[8].Person1);
            Assert.AreEqual(19, matches[8].Person2);//8-18
            Assert.AreEqual(15, matches[9].Person1);
            Assert.AreEqual(20, matches[9].Person2);//10-19



        }

        [TestMethod()]
        public void MakeMatchTest04()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "A", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "A", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "B", PersonName = "加藤" });
            persons.Add(7, new Person { LotNumber = 7, PersonGroup = "B", PersonName = "佐藤" });
            persons.Add(8, new Person { LotNumber = 8, PersonGroup = "B", PersonName = "佐藤" });
            persons.Add(9, new Person { LotNumber = 9, PersonGroup = "B", PersonName = "鈴木" });
            persons.Add(10, new Person { LotNumber = 10, PersonGroup = "B", PersonName = "高橋" });
            persons.Add(11, new Person { LotNumber = 11, PersonGroup = "C", PersonName = "石橋" });
            persons.Add(12, new Person { LotNumber = 12, PersonGroup = "C", PersonName = "仁一" });
            persons.Add(13, new Person { LotNumber = 13, PersonGroup = "C", PersonName = "和也" });
            persons.Add(14, new Person { LotNumber = 14, PersonGroup = "C", PersonName = "古山" });
            persons.Add(15, new Person { LotNumber = 15, PersonGroup = "C", PersonName = "澤田" });
            persons.Add(16, new Person { LotNumber = 16, PersonGroup = "D", PersonName = "山口" });
            persons.Add(17, new Person { LotNumber = 17, PersonGroup = "D", PersonName = "井上" });
            persons.Add(18, new Person { LotNumber = 18, PersonGroup = "D", PersonName = "木村" });
            persons.Add(19, new Person { LotNumber = 19, PersonGroup = "D", PersonName = "林" });
            persons.Add(20, new Person { LotNumber = 20, PersonGroup = "D", PersonName = "清水" });


            //1回戦
            Match[] matches = Match.MakeMatch(persons, new Match[0]);
            Assert.AreEqual(10, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(6, matches[0].Person2);//1-6
            Assert.AreEqual(2, matches[1].Person1);
            Assert.AreEqual(7, matches[1].Person2);//2-7
            Assert.AreEqual(3, matches[2].Person1);
            Assert.AreEqual(8, matches[2].Person2);//3-8
            Assert.AreEqual(4, matches[3].Person1);
            Assert.AreEqual(9, matches[3].Person2);//4-9
            Assert.AreEqual(5, matches[4].Person1);
            Assert.AreEqual(10, matches[4].Person2);//5-10
            Assert.AreEqual(11, matches[5].Person1);
            Assert.AreEqual(16, matches[5].Person2);//11-16
            Assert.AreEqual(12, matches[6].Person1);
            Assert.AreEqual(17, matches[6].Person2);//12-17
            Assert.AreEqual(13, matches[7].Person1);
            Assert.AreEqual(18, matches[7].Person2);//13-18
            Assert.AreEqual(14, matches[8].Person1);
            Assert.AreEqual(19, matches[8].Person2);//14-19
            Assert.AreEqual(15, matches[9].Person1);
            Assert.AreEqual(20, matches[9].Person2);//15-20


            matches[0].Result = 1;// 1win
            matches[1].Result = 1;// 2win 
            matches[2].Result = 1;// 3win
            matches[3].Result = 1;// 4win
            matches[4].Result = 1;// 5win
            matches[5].Result = 1;// 11win
            matches[6].Result = 1;// 12win
            matches[7].Result = 1;// 13win
            matches[8].Result = 1;// 14win
            matches[9].Result = 1;// 15win

            //2回戦
            Match[] matches2 = Match.MakeMatch(persons, matches);

            Assert.AreEqual(10, matches2.Length);
            Assert.AreEqual(1, matches2[0].Person1);
            Assert.AreEqual(11, matches2[0].Person2);//1-11
            Assert.AreEqual(2, matches2[1].Person1);
            Assert.AreEqual(12, matches2[1].Person2);//2-12
            Assert.AreEqual(3, matches2[2].Person1);
            Assert.AreEqual(13, matches2[2].Person2);//3-13
            Assert.AreEqual(4, matches2[3].Person1);
            Assert.AreEqual(14, matches2[3].Person2);//4-14
            Assert.AreEqual(5, matches2[4].Person1);
            Assert.AreEqual(15, matches2[4].Person2);//5-15
            Assert.AreEqual(6, matches2[5].Person1);
            Assert.AreEqual(16, matches2[5].Person2);//6-16
            Assert.AreEqual(7, matches2[6].Person1);
            Assert.AreEqual(17, matches2[6].Person2);//7-17
            Assert.AreEqual(8, matches2[7].Person1);
            Assert.AreEqual(18, matches2[7].Person2);//8-18
            Assert.AreEqual(9, matches2[8].Person1);
            Assert.AreEqual(19, matches2[8].Person2);//9-19
            Assert.AreEqual(10, matches2[9].Person1);
            Assert.AreEqual(20, matches2[9].Person2);//10-20

            var result = Match.MergeMatch(matches, matches2);

            matches[0].Result = 1;// 1win
            matches[1].Result = 2;// 12win 
            matches[2].Result = 1;// 3win
            matches[3].Result = 2;// 14win
            matches[4].Result = 2;// 15win
            matches[5].Result = 2;// 16win
            matches[6].Result = 1;// 7win
            matches[7].Result = 2;// 18win
            matches[8].Result = 1;// 9win
            matches[9].Result = 2;// 20win

            //3回戦
            Match[] matches3 = Match.MakeMatch(persons, result);

            Assert.AreEqual(10, matches3.Length);
            Assert.AreEqual(1, matches3[0].Person1);
            Assert.AreEqual(7, matches3[0].Person2);//1-7
            Assert.AreEqual(3, matches3[1].Person1);
            Assert.AreEqual(9, matches3[1].Person2);//3-9
            Assert.AreEqual(10, matches3[2].Person1);
            Assert.AreEqual(12, matches3[2].Person2);//10-12
            Assert.AreEqual(14, matches3[3].Person1);
            Assert.AreEqual(16, matches3[3].Person2);//14-16
            Assert.AreEqual(18, matches3[4].Person1);
            Assert.AreEqual(2, matches3[4].Person2);//18-2
            Assert.AreEqual(20, matches3[5].Person1);
            Assert.AreEqual(4, matches3[5].Person2);//20-4
            Assert.AreEqual(5, matches3[6].Person1);
            Assert.AreEqual(6, matches3[6].Person2);//5-6
            Assert.AreEqual(8, matches3[7].Person1);
            Assert.AreEqual(11, matches3[7].Person2);//8-11
            Assert.AreEqual(13, matches3[8].Person1);
            Assert.AreEqual(17, matches3[8].Person2);//13-17
            Assert.AreEqual(15, matches3[9].Person1);
            Assert.AreEqual(19, matches3[9].Person2);//15-19



        }
    }
}