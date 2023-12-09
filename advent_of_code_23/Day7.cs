
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace advent_of_code_23
{
    internal class Day7
    {

        public static void Solution(List<string> lines)
        {
            StringBuilder numberBuffer = new StringBuilder();
            List<(string, int, int)> hands = new List<(string, int, int)>();
            List<string> handsBids = new List<string>();

            foreach (string line in lines)
            {
                SaveNumbers(line, handsBids, numberBuffer);
            }


            hands = GroupHandsBids(handsBids);

            List<(string, int, int)> fiveOfAKind = new List<(string, int, int)>();
            List<(string, int, int)> fourOFAKind = new List<(string, int, int)>();
            List<(string, int, int)> fullHouse = new List<(string, int, int)>();
            List<(string, int, int)> threeOfAKind = new List<(string, int, int)>();
            List<(string, int, int)> twoPair = new List<(string, int, int)>();
            List<(string, int, int)> onePair = new List<(string, int, int)>();
            List<(string, int, int)> hightCard = new List<(string, int, int)>();


            HandsByType(hands, fiveOfAKind, fourOFAKind, fullHouse, threeOfAKind, twoPair, onePair, hightCard);

            string[] ranks = {"A", "K", "Q", "J", "T", "9", "8", "7", "6", "5", "4", "3", "2" };

            OrderByRanks(fiveOfAKind, fourOFAKind, fullHouse, threeOfAKind, twoPair, onePair, hightCard, ranks);

        }

        private static void OrderByRanks(
            List<(string, int, int)> fiveOfAKind,
            List<(string, int, int)> fourOFAKind,
            List<(string, int, int)> fullHouse,
            List<(string, int, int)> threeOfAKind,
            List<(string, int, int)> twoPair,
            List<(string, int, int)> onePair,
            List<(string, int, int)> hightCard,
            string[] ranks)
        {
            List<(string, int)> fiveOfAKindCopy = new List<(string, int)> ();
            List<(string, int)> fourOFAKindCopy = new List<(string, int)> ();
            List<(string, int)> fullHouseCopy = new List<(string, int)> ();
            List<(string, int)> threeOfAKindCopy = new List<(string, int)> ();
            List<(string, int)> twoPairCopy = new List<(string, int)> ();
            List<(string, int)> onePairCopy = new List<(string, int)> ();
            List<(string, int)> hightCardCopy = new List<(string, int)> ();

            TranslatorToNumbers(fiveOfAKind, fiveOfAKindCopy, ranks);
            TranslatorToNumbers(fourOFAKind, fourOFAKindCopy, ranks);
            TranslatorToNumbers(fullHouse, fullHouseCopy, ranks);
            TranslatorToNumbers(threeOfAKind, threeOfAKindCopy, ranks);
            TranslatorToNumbers(twoPair, twoPairCopy, ranks);
            TranslatorToNumbers(onePair, onePairCopy, ranks);
            TranslatorToNumbers(hightCard, hightCardCopy, ranks);

            OrderNumbersByRank(fiveOfAKindCopy, fiveOfAKind);
            OrderNumbersByRank(fourOFAKindCopy, fourOFAKind);
            OrderNumbersByRank(fullHouseCopy, fullHouse);
            OrderNumbersByRank(threeOfAKindCopy, threeOfAKind);
            OrderNumbersByRank(twoPairCopy, twoPair);
            OrderNumbersByRank(onePairCopy, onePair);
            OrderNumbersByRank(hightCardCopy, hightCard);

            List<(string, int, int)> orderedRanks = new List<(string, int, int)>();

            FinalOrder(fiveOfAKind, orderedRanks);
            FinalOrder(fourOFAKind, orderedRanks);
            FinalOrder(fullHouse, orderedRanks);
            FinalOrder(threeOfAKind, orderedRanks);
            FinalOrder(twoPair, orderedRanks);
            FinalOrder(onePair, orderedRanks);
            FinalOrder(hightCard, orderedRanks);    
  

            long total = 0;
            int multiply = 1;
            for (int i = orderedRanks.Count - 1; i >= 0; i--)
            {
                total += orderedRanks[i].Item2 * multiply;
                multiply++;
            }
            Console.WriteLine(total);
        }

        private static void FinalOrder(List<(string, int, int)> mainList, List<(string, int, int)> orderedRanks)
        {
            mainList.Sort((x, y) => x.Item3.CompareTo(y.Item3));
            foreach (var item in mainList)
            {
                orderedRanks.Add(item);
            }
        }

        private static void OrderNumbersByRank(List<(string, int)> copyOfListOfRanks, List<(string, int, int)> mainList)
        {
            
            for (int k = 0; k < copyOfListOfRanks.Count; k++)
            {
                int hexToNumber = int.Parse(copyOfListOfRanks[k].Item1, System.Globalization.NumberStyles.HexNumber);
                Tuple<string, int, int> tup = new Tuple<string, int, int>(mainList[k].Item1, mainList[k].Item2, hexToNumber);
                //mainList.RemoveAt(k);
                mainList[k] = ((tup.Item1, tup.Item2, tup.Item3));

            }
        }

        private static void TranslatorToNumbers(List<(string, int, int)>  ranksOfHands, 
            List<(string, int)> ranksOfHandsCopy, string[] ranks)
        {
            foreach (var hand in ranksOfHands)
            {
                StringBuilder handCopy = new StringBuilder();

                foreach (var c in hand.Item1)
                {
                    for (int i = 0; i < ranks.Length; i++)
                    {
                        if (c.ToString() == ranks[i].ToString())
                        {
                            
                            string hexValue = i.ToString("X");
                            handCopy.Append(hexValue);
                        }
                    }
                }
                ranksOfHandsCopy.Add((handCopy.ToString(), hand.Item2));
            }
        }



        private static void HandsByType(
            List<(string, int, int)> hands,
            List<(string, int, int)> fiveOfAKind,
            List<(string, int, int)> fourOFAKind,
            List<(string, int, int)> fullHouse,
            List<(string, int, int)> threeOfAKind,
            List<(string, int, int)> twoPair,
            List<(string, int, int)> onePair,
            List<(string, int, int)> hightCard)
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
                    fiveOfAKind.Add((hand.Item1, hand.Item2, 0));
                }
                if (sumTotal == 17)
                {
                    fourOFAKind.Add((hand.Item1, hand.Item2, 0));
                }
                if (sumTotal == 13)
                {
                    fullHouse.Add((hand.Item1, hand.Item2, 0));
                }
                if (sumTotal == 11)
                {
                    threeOfAKind.Add((hand.Item1, hand.Item2, 0));
                }
                if (sumTotal == 9)
                {
                    twoPair.Add((hand.Item1, hand.Item2, 0));
                }
                if (sumTotal == 7)
                {
                    onePair.Add((hand.Item1, hand.Item2, 0));
                }
                if (sumTotal == 5)
                {
                    hightCard.Add((hand.Item1, hand.Item2, 0));
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

        private static List<(string, int, int)> GroupHandsBids(List<string> handsBids)
        {
            string hand;
            int bid = 0;

            Tuple<string, int> tup = new Tuple<string, int>(string.Empty, 0);
            List<(string, int, int)> numbersByTypeInTupleList = new List<(string, int, int)>();

            for (int i = 0; i < handsBids.Count; i ++)
            {

                hand = handsBids[i].Substring(0,5);

                int handBidLength = handsBids[i].Length;

                bid = Int32.Parse(handsBids[i].Substring(5, handBidLength - hand.Length));


                tup = new Tuple<string, int>(hand, bid);
                numbersByTypeInTupleList.Add((tup.Item1, tup.Item2, 0));
                
            }



            return numbersByTypeInTupleList;
        }
    }
}
