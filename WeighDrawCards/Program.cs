using System;
using System.Collections.Generic;
/*思考题：权重随机抽卡问题。卡牌游戏中，卡片等级为S、A、B、C、D等等，其概率可以修改。这时使用“权重”的方式特别方便填写，例如：
卡牌列表：[“S”, “A”, “B”, “C”]

权重列表：[  1,  2,   2,    4   ]

这样配置，则S概率为1 / 9，其它依次为：2 / 9, 2 / 9, 4 / 9。在列表中随意添加卡牌和权重，都可以自动识别并随机抽卡。试着实现这种灵活的抽卡程序。*/
namespace WeighDrawCards
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            // {1, 10, 100, 9889}

            List<int> weight = new List<int>() { 1, 2, 2, 4 };
            List<string> cards = new List<string>() { "S", "A", "B", "C" };

            // 一共抽到多少张卡，统计
            int[] counter = new int[4];

            int count = 0;
            int N = 10000;

            // 整理权重数组，合并到前N项和
            List<int> temp = new List<int>();
            int temp_sum=0;
            for (int i = 0; i < weight.Count; i++)
            {
                temp_sum += weight[i];
                temp.Add(temp_sum);
            }
          
            // temp = [1, 1+2, 1+2+2, 1+2+2+4] = [1, 3, 5, 9]
            int sum = temp[temp.Count - 1];  

            // 开始N次抽卡
            while (count < N)    // 计数
            {
                int r = random.Next(0, sum);
                int card_index = -1;  
                for (int i = 0; i < weight.Count; i++)
                {
                    if (r < temp[i])
                    {
                        card_index = i;
                        break;
                    }
                }
                counter[card_index] +=1;

                count += 1;
            }

            Console.WriteLine($"共抽卡{N}次");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine($"{cards[i]} : {counter[i]}次");
            }
            Console.ReadKey();
        }
    }
}
