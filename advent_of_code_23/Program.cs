using advent_of_code_23;

string data = "";

var dataConverted = Day2_2.getDataDay2(data);

long sum = 0;

foreach(var item in dataConverted)
{
    sum = sum + item;
}

Console.WriteLine(sum);



