
namespace CalculatePayout
{
	internal class Combination
	{
		public int Qtd10Denomination { get; set; }
		public int Qtd50Denomination { get; set; }
		public int Qtd100Denomination { get; set; }

		internal void AddQtdDenomination(int denominationValue)
		{
			switch (denominationValue) 
			{
				case 10: Qtd10Denomination++; break;
				case 50: Qtd50Denomination++; break;
				case 100: Qtd100Denomination++; break;
			}

		}
	}
}
