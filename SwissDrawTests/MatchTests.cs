﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void MakeMatchTest()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });

            Match[] matches = Match.MakeMatch(persons, new Match[0]);
            Assert.AreEqual(3, matches.Length);
            Assert.AreEqual(1, matches[0].Person1);
            Assert.AreEqual(4, matches[0].Person2);
            matches[0].Result = 1;
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


        /////////  独自に追加したメソッド
        [TestMethod()]
        public void GetKeyArrayTest()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });

            int[] result = Match.GetKeyArray(persons);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
        }

        [TestMethod()]
        public void GetMinimumKeyTest()
        {
            int[][] keys = new int[1][];
            int[] key0 = { 1, 2, 3, 4, 5, 6 };
            keys[0] = key0;
            Match[] matches1 = new Match[0];
            Match[] matches2 = new Match[1];
            matches2[0] = new Match(1, 2);
            Match[] matches3 = new Match[2];
            matches3[0] = new Match(1, 2);
            matches3[1] = new Match(3, 6);

            var result1 = Match.GetMinimumKey(keys, matches1);
            Assert.AreEqual(1, result1);
            var result1_2 = Match.GetMinimumKey(keys, matches2);
            Assert.AreEqual(3, result1_2);
            var result1_3 = Match.GetMinimumKey(keys, matches3);
            Assert.AreEqual(4, result1_3);

            int[][] keys2 = new int[2][];
            int[] key1 = { 1, 2, 4 };
            int[] key2 = { 3, 5, 6 };
            keys2[0] = key1;
            keys2[1] = key2;
            var result3 = Match.GetMinimumKey(keys2, matches1);
            Assert.AreEqual(1, result3);
            var result4 = Match.GetMinimumKey(keys2, matches2);
            Assert.AreEqual(4, result4);
        }
        [TestMethod()]

        public void IsSameGroupTest()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });
            Assert.AreEqual(true, Match.IsSameGroup(persons, 1, 2));
            Assert.AreEqual(false, Match.IsSameGroup(persons, 1, 4));
        }
        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]

        public void IsSameGroupTest2()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });
            var result = Match.IsSameGroup(persons, 1, 8);
        }

        [TestMethod()]
        public void MakePersonArrayTest()
        {
            Dictionary<int, int> winCounts = new Dictionary<int, int>();
            winCounts.Add(1, 0);
            winCounts.Add(2, 0);
            winCounts.Add(3, 0);
            winCounts.Add(4, 0);

            var result = Match.MakePersonArray(0, winCounts);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);

            Dictionary<int, int> winCounts2 = new Dictionary<int, int>();
            winCounts2.Add(1, 1);
            winCounts2.Add(2, 1);
            winCounts2.Add(3, 0);
            winCounts2.Add(4, 0);

            var result2 = Match.MakePersonArray(0, winCounts2);
            Assert.AreEqual(2, result2.Length);
            Assert.AreEqual(3, result2[0]);
            Assert.AreEqual(4, result2[1]);
            var result3 = Match.MakePersonArray(1, winCounts2);
            Assert.AreEqual(2, result3.Length);
            Assert.AreEqual(1, result3[0]);
            Assert.AreEqual(2, result3[1]);


        }

        [TestMethod()]
        public void SplitPersonsTest()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });
            int[][] result = Match.SplitPersons(persons, new Match[0]);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(6, result[0].Length);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(2, result[0][1]);
            Assert.AreEqual(3, result[0][2]);
            Assert.AreEqual(4, result[0][3]);
            Assert.AreEqual(5, result[0][4]);
            Assert.AreEqual(6, result[0][5]);

            Match[] matches = new Match[3];
            matches[0] = new Match(1, 4);
            matches[0].Result = 1;
            matches[1] = new Match(2, 5);
            matches[1].Result = 1;
            matches[2] = new Match(3, 6);
            matches[2].Result = 1;
            int[][] result2 = Match.SplitPersons(persons, matches);
            Assert.AreEqual(2, result2.Length);
            Assert.AreEqual(3, result2[0].Length);
            Assert.AreEqual(3, result2[1].Length);
            Assert.AreEqual(1, result2[0][0]);
            Assert.AreEqual(2, result2[0][1]);
            Assert.AreEqual(3, result2[0][2]);
            Assert.AreEqual(4, result2[1][0]);
            Assert.AreEqual(5, result2[1][1]);
            Assert.AreEqual(6, result2[1][2]);

        }


        [TestMethod()]
        public void GetVersusKey1Test()
        {
            Dictionary<int, Person> persons = new Dictionary<int, Person>();
            persons.Add(1, new Person { LotNumber = 1, PersonGroup = "A", PersonName = "安藤" });
            persons.Add(2, new Person { LotNumber = 2, PersonGroup = "A", PersonName = "伊藤" });
            persons.Add(3, new Person { LotNumber = 3, PersonGroup = "A", PersonName = "有働" });
            persons.Add(4, new Person { LotNumber = 4, PersonGroup = "B", PersonName = "遠藤" });
            persons.Add(5, new Person { LotNumber = 5, PersonGroup = "B", PersonName = "尾堂" });
            persons.Add(6, new Person { LotNumber = 6, PersonGroup = "C", PersonName = "加藤" });
            int[] keys = Match.GetKeyArray(persons);
            int[][] SplittedKeys = Match.SplitPersons(persons, new Match[0]);
            var result = Match.GetVersusKey1(1, SplittedKeys, new Match[0], persons, new Match[0]);
            Assert.AreEqual(4, result);


        }
    }
}