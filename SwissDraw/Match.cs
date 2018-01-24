using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissDraw
{
    public class Match
    {
        private int person1;
        private int person2;
        // result 1:Person1の勝ち　2:Person2の勝ち　0:決着がついていない　他:エラー
        private int result;

        public int Person1 { get => person1; set => person1 = value; }
        public int Person2 { get => person2; set => person2 = value; }
        public int Result { get => result; set => result = value; }

        //コンストラクタ
        public Match(int i, int j)
        {
            person1 = i;
            person2 = j;
            result = 0;
        }

        public static Match[] MakeMatch(Dictionary<int, Person> persons, Match[] results)
        {
            //parsonsが奇数のとき999、Z、不戦勝を追加する
            if (persons.Count % 2 != 0)
            {
                persons.Add(999, new Person { LotNumber = 999, PersonGroup = "Z", PersonName = "不戦勝" });
            }

            // personsのkeyのみ取り出す
            int[] keys = GetKeyArray(persons);

            // 配列を初期化する
            int matchCount = keys.Length / 2;

            // 勝数でkeyを分割する
            int[][] SplittedKeys = SplitPersons(persons, results);

            //「対戦していない」「同じチームじゃない」「勝ち数が同じ」
            Match[] matches = MakeMatch1(matchCount, SplittedKeys, persons, results);       /**********NullReferenceException**************/
            if (matches == null)
            {
                //「対戦していない」「同じチームじゃない」
                matches = MakeMatch2(matchCount, SplittedKeys, persons, results);
                if (matches == null)
                {
                    //「対戦していない」
                    matches = MakeMatch3(matchCount, SplittedKeys, persons, results);
                }
            }

            return matches;
        }

        // 「対戦していない」「同じチームじゃない」「勝ち数が同じ」で対戦
        public static Match[] MakeMatch1(int matchCount, int[][] SplittedKeys, Dictionary<int, Person> persons, Match[] results)
        {
            Match[] matches = new Match[matchCount];

            for (int i = 0; i < matchCount; i++)
            {
                // 最小のくじ番号を取得する(使われていないこと)
                int minKey = GetMinimumKey(SplittedKeys, matches);      /**********NullReferenceException**************/

                // 対応する対戦相手のくじ番号を取得する（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
                int versusKey = GetVersusKey1(minKey, SplittedKeys, matches, persons, results);

                // versusKey<0なら、対戦相手は見つからなかったため、nullを返す
                if (versusKey < 0) //すべて当てはまるものがない
                { 
                    return null;
                }
                //対戦を保存する
                matches[i] = new Match(minKey, versusKey);
            }
            return matches;
        }

        // 「対戦していない」「同じチームじゃない」で対戦
        public static Match[] MakeMatch2(int matchCount, int[][] SplittedKeys, Dictionary<int, Person> persons, Match[] results)
        {
            Match[] matches = new Match[matchCount];

            for (int i = 0; i < matchCount; i++)
            {
                // 最小のくじ番号を取得する(使われていないこと)
                int minKey = GetMinimumKey(SplittedKeys, matches);

                // 対応する対戦相手のくじ番号を取得する（使われていない、対戦していない、同じチームじゃない）
                int versusKey = GetVersusKey2(minKey, SplittedKeys, matches, persons, results);

                // versusKey<0なら、対戦相手は見つからなかったため、nullを返す
                if (versusKey < 0)
                {
                    return null;
                }
                //対戦を保存する
                matches[i] = new Match(minKey, versusKey);
            }
            return matches;
        }

        // 「対戦していない」で対戦
        public static Match[] MakeMatch3(int matchCount, int[][] SplittedKeys, Dictionary<int, Person> persons, Match[] results)
        {
            Match[] matches = new Match[matchCount];

            for (int i = 0; i < matchCount; i++)
            {
                // 最小のくじ番号を取得する(使われていない)
                int minKey = GetMinimumKey(SplittedKeys, matches);

                // 対応する対戦相手のくじ番号を取得する（使われていない、対戦していない）
                int versusKey = GetVersusKey3(minKey, SplittedKeys, matches, persons, results);

                // versusKey<0なら、対戦相手は見つからなかったため、nullを返す
                if (versusKey < 0)
                {
                    return null;
                }
                //対戦を保存する
                matches[i] = new Match(minKey, versusKey);
            }
            return matches;
        }

        // 勝ち数の多いほうから順番に、勝ち数が同じ人のIDを配列にまとめる
        public static int[][] SplitPersons(Dictionary<int, Person> persons, Match[] results)
        {
            Dictionary<int, int> winCountDic = new Dictionary<int, int>();
            int[] keyArray = GetKeyArray(persons);
            int maxWinCount = -1;//最大の勝ち数
            foreach (int i in keyArray)
            {
                int wCount = GetWinCount(i, results);
                winCountDic.Add(i, wCount);//くじ番号、勝ち数
                if (maxWinCount < wCount)
                {
                    maxWinCount = wCount;
                }
            }
            int[][] result = new int[maxWinCount + 1][];
            for (int i = 0; i <= maxWinCount; i++)
            {
                result[i] = MakePersonArray(maxWinCount - i, winCountDic);//result[0]に最大勝ち数・・・result[maxWinCount]に最小勝ち数
            }
            return result;
        }

        // winCountDicの中で、勝数がvの要素の配列を生成する
        public static int[] MakePersonArray(int v, Dictionary<int, int> winCountDic)//最大勝ち数、（くじ番号、勝ち数）
        {
            List<int> l
                = new List<int>();
            foreach (int i in winCountDic.Keys)
            {
                if (winCountDic[i] == v)
                {
                    l.Add(i);
                }
            }
            return l.ToArray();
        }

        // 対応する対戦相手のくじ番号を取得する
        //（使われていない、対戦していない、同じチームじゃない、勝ち数が同じ）
        private static int GetVersusKey1(int minKey, int[][] splittedKeys, Match[] matches,
            Dictionary<int, Person> persons, Match[] results)
        {
            int i = 0;
            bool flag = true;
            while (flag == true)
            {
                //i勝グループ配列にくじ番号最小値が存在するか
                if (splittedKeys[i].Contains(minKey) == true)
                {
                    flag = false;
                }
                else
                {
                    i++;
                }
            }
            foreach (int key in splittedKeys[i])//勝ち数が同じグループの中で
            {
                if (key != minKey)//自分でないか
                {
                    if (ContainsKey(matches, key) == false)//使われていないか
                    {
                        if (IsMatched(results, key, minKey) == false)//すでに対戦しているか
                        {
                            if (IsSameGroup(persons, key, minKey) == false)//同じグループでないか
                            {
                                //すべて当てはまれば対戦相手のkeyを返す
                                return key;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        // 対応する対戦相手のくじ番号を取得する
        //（使われていない、対戦していない、同じチームじゃない、勝ち数無視）
        public static int GetVersusKey2(int minKey, int[][] splittedKeys, Match[] matches,
            Dictionary<int, Person> persons, Match[] results)
        {
            int i = 0;
            bool flag = true;
            while (flag == true)
            {
                //i勝グループ配列にくじ番号最小値が存在するか
                if (splittedKeys[i].Contains(minKey) == true)
                {
                    flag = false;
                }
                else
                {
                    i++;
                }
            }

            while (i <= splittedKeys.Length -1)//勝ち数が同じグループの中で
            {
                foreach (int key in splittedKeys[i])
                {
                    if (key != minKey)//自分でないか
                    {
                        if (ContainsKey(matches, key) == false)//使われていないか
                        {
                            if (IsMatched(results, key, minKey) == false)//すでに対戦しているか
                            {
                                if (IsSameGroup(persons, key, minKey) == false)//同じグループでないか
                                {
                                    return key;
                                }
                            }
                        }
                    }
                }
                i++;
            }
            return -1;
        }


        // 対応する対戦相手のくじ番号を取得する
        //（使われていない、対戦していない、チーム無視、勝ち数無視）
        public static int GetVersusKey3(int minKey, int[][] splittedKeys, Match[] matches,
            Dictionary<int, Person> persons, Match[] results)
        {
            int i = 0;
            bool flag = true;
            while (flag == true)
            {
                if (splittedKeys[i].Contains(minKey) == true)
                {
                    flag = false;
                }
                else
                {
                    i++;
                }
            }
            while (i <= splittedKeys.Length)
            {
                foreach (int key in splittedKeys[i])
                {
                    if (key != minKey)//自分でないか
                    {
                        if (ContainsKey(matches, key) == false)//使われていないか
                        {
                            if (IsMatched(results, key, minKey) == false)//すでに対戦しているか
                            {
                                return key;
                            }
                        }
                    }
                }
                i++;
            }
            return -1;
        }

        //同じグループか調べる
        public static bool IsSameGroup(Dictionary<int, Person> persons, int i, int j)
        {
            String iTeam = persons[i].PersonGroup;
            String jTeam = persons[j].PersonGroup;
            return iTeam.Equals(jTeam);
        }

        // resultsの中に、iとjの対戦があればtrueを返す(すでに対戦していたらtrue)
        private static bool IsMatched(Match[] results, int i, int j)
        {
            foreach (Match m in results)
            {
                if (IsMatched(m, i, j) == true)
                {
                    return true;
                }
            }
            return false;
        }

        // mがiとjの対戦ならtrueを返す
        private static bool IsMatched(Match m, int i, int j)
        {
            if (m.person1 == i)
            {
                if (m.person2 == j)
                {
                    return true;
                }
            }
            if (m.person2 == i)
            {
                if (m.person1 == j)
                {
                    return true;
                }
            }
            return false;
        }

        // 指定されたkeyの勝ち数を調べる
        private static int GetWinCount(int key, Match[] results)
        {
            int winCount = 0;
            foreach (Match result in results)
            {
                if (CheckWin(key, result) == true)
                {
                    winCount++;
                }
            }
            return winCount;
        }

        //勝ったらtrueを返す
        private static Boolean CheckWin(int key, Match result)
        {
            if (ContainsKey(result, key) == true)
            {
                if (result.result == 1)
                {
                    if (result.person1 == key)
                    {
                        return (true);
                    }
                }
                else if (result.result == 2)
                {
                    if (result.person2 == key)
                    {
                        return (true);
                    }
                }
            }
            return false;
        }

        // 最小のくじ番号を取得する(使われていないこと)
        private static int GetMinimumKey(int[][] splittedKeys, Match[] matches)
        {
            int minKey = 100000;
            foreach (int[] a in splittedKeys)
            {
                foreach (int b in a)
                {
                    if (minKey > b)
                    {
                        if (ContainsKey(matches, b) == false)       /**********NullReferenceException**************/
                        {
                            minKey = b;
                        }
                    }
                }
            }
            return minKey;
        }

        //くじ番号iが今回の対戦に使われていなければfalse
        private static bool ContainsKey(Match[] matches, int i)
        {
            foreach (Match m in matches)
            {
                if (ContainsKey(m, i) == true)       /**********NullReferenceException**************/
                {
                    return true;
                }
            }
            return false;
        }

        //くじ番号iがすでに対戦に使われていたらtrue
        private static bool ContainsKey(Match m, int i)
        {
            if (m.person1 == i)      /**********NullReferenceException**************/
            {
                return true;
            }
            if (m.person2 == i)
            {
                return true;
            }
            return false;
        }

        // 2つの配列をマージして1つにする
        public static Match[] MergeMatch(Match[] results1, Match[] results2)
        {
            Match[] result = new Match[results1.Length + results2.Length];
            int c = 0;
            for (int i = 0; i < results1.Length; i++)
            {
                result[c++] = results1[i];
            }
            for (int i = 0; i < results2.Length; i++)
            {
                result[c++] = results2[i];
            }
            return result;
        }

        // personsのkeyを取り出して配列にする
        public static int[] GetKeyArray(Dictionary<int, Person> persons)
        {
            return persons.Keys.ToArray();
        }
    }
}