using advent_of_code_23;

string data = "";
var dataConverted = DataConverter_day1.getData(data);

long sum = 0;

foreach(var item in dataConverted)
{
    sum = sum + item;
}

Console.WriteLine(sum);



