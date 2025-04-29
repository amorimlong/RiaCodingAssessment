// See https://aka.ms/new-console-template for more information
using CalculatePayout;
using CalculatePayout.Extensions;
using System.Text;

Console.WriteLine("Welcome to the Payout Calculate");


var combinations = new List<Combination>();
var amounts = new List<int>{30,50,60,80,140,230,370,610,980};

foreach (var amout in amounts)
{
	combinations = new List<Combination>();
	Console.WriteLine($"------------------------------------------------------ ");
	Console.WriteLine($"Calculate payout for {amout} EUR ");
	Console.WriteLine($"------------------------------------------------------ ");
	CalcCombinations(amout);
	PrintCombination();
	Console.WriteLine($"------------------------------------------------------ ");
}


void CalcCombinations(int value)
{
	CalcCombinationOf10(value, null);
	CalcCombinationOf50(value, null);
	CalcCombinationOf100(value, null);
}


void PrintCombination()
{
	foreach (var combination in combinations)
	{
		var printDenomination = new StringBuilder();

		printDenomination.FormatPrintDenomination(combination.Qtd100Denomination, "100");
		printDenomination.FormatPrintDenomination(combination.Qtd50Denomination, "50");
		printDenomination.FormatPrintDenomination(combination.Qtd10Denomination, "10");

		Console.WriteLine(printDenomination.ToString());
	}
}


void CalcCombinationOf100(int value, Combination combination)
{
	if (combination is null)
	{
		combination = new Combination();
		combinations.Add(combination);
	}

	if (value >= 100)
	{
		combination.Qtd100Denomination++;
		value = value - 100;
		CalcCombinationOf10(value, combination);

		if (value >= 50)
		{
			var combination50 = new Combination();
			combination50.Qtd100Denomination = combination.Qtd100Denomination;
			combinations.Add(combination50);
			CalcCombinationOf50(value, combination50);
		}

		if (value >= 100) 
		{
			var combination100 = new Combination();
			combination100.Qtd100Denomination = combination.Qtd100Denomination;
			combinations.Add(combination100);
			CalcCombinationOf100(value, combination100);
		}
	}

	return;
}

void CalcCombinationOf50(int value, Combination combination)
{

	if(combination is null)
	{
		combination = new Combination();
		combinations.Add(combination);
	}

	
	if (value >= 50)
	{
		combination.Qtd50Denomination++;

		value = value - 50;
		CalcCombinationOf10(value, combination);

		if (value >= 50) 
		{
			var combination50 = new Combination();
			combination50.Qtd100Denomination = combination.Qtd100Denomination;
			combination50.Qtd50Denomination = combination.Qtd50Denomination;
			combinations.Add(combination50);
			CalcCombinationOf50(value, combination50);
		}
	}

	return;
}

void CalcCombinationOf10(int value, Combination combination)
{

	if (combination is null)
	{
		combination = new Combination();
		combinations.Add(combination);
	}

	if (value >= 10)
	{
		combination.Qtd10Denomination++;
		value = value - 10;
		CalcCombinationOf10(value, combination);
	}

	return;
}