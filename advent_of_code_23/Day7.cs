
using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_23
{
    internal class Day7
    {

        public static void Solution(List<string> lines)
        {
            StringBuilder numberBuffer = new StringBuilder();
            List<(string, int)> hands = new List<(string, int)>();
            List<string> handsBids = new List<string>();

            foreach (string line in lines)
            {
                SaveNumbers(line, handsBids, numberBuffer);
            }


            hands = GroupHandsBids(handsBids);

            List<(string, int)> fiveOfAKind = new List<(string, int)>();
            List<(string, int)> fourOFAKind = new List<(string, int)>();
            List<(string, int)> fullHouse = new List<(string, int)>();
            List<(string, int)> threeOfAKind = new List<(string, int)>();
            List<(string, int)> twoPair = new List<(string, int)>();
            List<(string, int)> onePair = new List<(string, int)>();
            List<(string, int)> hightCard = new List<(string, int)>();


            HandsByType(hands, fiveOfAKind, fourOFAKind, fullHouse, threeOfAKind, twoPair, onePair, hightCard);


        }
        private static void HandsByType(
            List<(string, int)> hands,
            List<(string, int)> fiveOfAKind,
            List<(string, int)> fourOFAKind,
            List<(string, int)> fullHouse,
            List<(string, int)> threeOfAKind,
            List<(string, int)> twoPair,
            List<(string, int)> onePair,
            List<(string, int)> hightCard)
        { 

            List<int> repetitions = new List<int>();
            int sum = 0;
            foreach (var hand in hands)
            {
                repetitions = new List<int>();
                for (var i = 0; i < hand.Item1.Length; i++)
                {
                    sum = 0;
                    for (var j = 0; j < hand.Item1.Length; j++)
                    {
                        if (hand.Item1[i] == hand.Item1[j])
                        {
                            sum++;
                        }
                    }
                    repetitions.Add(sum);
                }

                int sumTotal = 0;
                foreach (var item in repetitions)
                {
                    sumTotal += item;
                }


                if (sumTotal == 25)
                {
                    fiveOfAKind.Add((hand.Item1, hand.Item2));
                }
                if (sumTotal == 17)
                {
                    fourOFAKind.Add((hand.Item1, hand.Item2));
                }
                if (sumTotal == 13)
                {
                    fullHouse.Add((hand.Item1, hand.Item2));
                }
                if (sumTotal == 11)
                {
                    threeOfAKind.Add((hand.Item1, hand.Item2));
                }
                if (sumTotal == 9)
                {
                    twoPair.Add((hand.Item1, hand.Item2));
                }
                if (sumTotal == 7)
                {
                    onePair.Add((hand.Item1, hand.Item2));
                }
                if (sumTotal == 5)
                {
                    hightCard.Add((hand.Item1, hand.Item2));
                }
            }


        }


        private static void SaveNumbers(string line, List<string> hands, StringBuilder letterDigitBuffer)
        {
            letterDigitBuffer = new StringBuilder();
            foreach (Char c in line)
            {
                if (Char.IsLetterOrDigit(c) )
                {
                    letterDigitBuffer.Append(c);
                }
            }
            if (letterDigitBuffer.Length != 0) { hands.Add(letterDigitBuffer.ToString()); }
        }

        private static List<(string, int)> GroupHandsBids(List<string> handsBids)
        {
            string hand;
            int bid = 0;

            Tuple<string, int> tup = new Tuple<string, int>(string.Empty, 0);
            List<(string, int)> numbersByTypeInTupleList = new List<(string, int)>();

            for (int i = 0; i < handsBids.Count; i ++)
            {

                hand = handsBids[i].Substring(0,5);

                int handBidLength = handsBids[i].Length;

                bid = Int32.Parse(handsBids[i].Substring(5, handBidLength - hand.Length));


                tup = new Tuple<string, int>(hand, bid);
                numbersByTypeInTupleList.Add((tup.Item1, tup.Item2));
                
            }



            return numbersByTypeInTupleList;
        }
    }
}
