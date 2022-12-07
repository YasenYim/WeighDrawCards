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
            List<string> cards = new List<string>() { "S", "A", "B", "C"};

            // 一共抽到多少张卡，统计
            int[] counter = new int[4];

            int count = 0;
            int N = 1000;

            // 整理权重数组，合并到前N项和
            List<int> temp = new List<int>();
            int temp_sum=0;
            for (int i = 0; i < weight.Count; i++)
            {
                temp_sum += weight[i];
                temp.Add(temp_sum);
            }

            Console.WriteLine($"打印temp中的数字:");
            for (int i = 0;i<temp.Count;i++)
            {
                Console.WriteLine(temp[i]);
            }

            // temp = [1, 1+2, 1+2+2, 1+2+2+4] = [1, 3, 5, 9]
            int sum = temp[temp.Count - 1];
            Console.WriteLine($"权重数字的总和是{sum}");

            // 开始N次抽卡
            while (count < N)    // 计数
            {
                // int r是（0,8）闭区间的随机数
                int r = random.Next(0, sum);

                // 卡片的序号初始化为-1
                int card_index = -1;

                /* 遍历权重的数组
                 * i = 0,temp[0] = 1 
                 * i = 1,temp[1] = 3 
                 * i = 2,temp[2] = 5
                 * i = 3,temp[3] = 9
                */
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

/*
 * Here is an explanation of the principle of implementation: 
 * First, a list needs to be used to store the weight ratio. Then you need a list of cards. This is our most intuitive understanding. 
 * But converting the distance into the model we want requires some processing.
 * We abstract an array Counter for counting the number of card draws.
 * When making the initial value, the value we fill in is 4, but it is wrong to write three, it all depends on the number of subscripts of the corresponding card is 4.
 * Then if you fill in a number greater than 4, there is no problem.
 * In this way, except for the members with subscripts, the rest of the member numbers are 0. 
 * We sort through the existing weight array, and then find a counter temp_sum based on the last time accumulate on. You can get a new array.
 * This array is used to distinguish interval judgment value domains.
 */