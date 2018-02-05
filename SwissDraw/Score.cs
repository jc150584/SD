using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissDraw
{
    public class Score
    {
        // くじの番号
        public int LotNumber { get; set; }
        public int winCount { get; set; }
        public int score { get; set; }

        //コンストラクタ
        public Score(int no, int count, int s)
        {
            LotNumber = no;
            winCount = count;
            score = s;
        }

        public static Dictionary<int, Score> CalcWinClont(Match[] result)
        {
            
            Dictionary<int, Score> s = new Dictionary<int, Score>();
            int wincount;
            int[] key = new int[100];
            foreach (Match m in result)
            {
                foreach(int k in key)
                {
                     wincount = Match.GetWinCount(m.Person1, result);
                     s[m.Person1] = new Score(m.Person1, wincount, 0);

                     wincount = Match.GetWinCount(m.Person2, result);
                     s[m.Person2] = new Score(m.Person2, wincount, 0);
                }
            }
            return s;
        }

        public static Dictionary<int, Score> CalcScore(Match[] result)
        {
            Dictionary<int, Score> c = CalcWinClont(result);

            foreach (Match m in result)
            {
                //person1が勝ったら1、person2が勝ったら2
                int i = m.Result;
                Score s1 = c[m.Person1];
                Score s2 = c[m.Person2];   
                
                //person1が勝ったとき、person2の勝ち数分スコアを増やす
                if (i == 1)
                {
                    int score = s1.score;
                    int plus = s2.winCount;
                    int newscore = score + plus;
                    c[m.Person1] = new Score(m.Person1, s1.winCount, newscore);
                }
                else if (i == 2)
                {
                    int score = s2.score;
                    int plus = s1.winCount;
                    int newscore = score + plus;
                    c[m.Person2] = new Score(m.Person2, s2.winCount, newscore);
                }
            }
            return c;
        }
    }
}