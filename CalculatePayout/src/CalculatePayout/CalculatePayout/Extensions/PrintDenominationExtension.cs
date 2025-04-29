using System.Text;

namespace CalculatePayout.Extensions
{
	internal static class PrintDenominationExtension
	{
		internal static void FormatPrintDenomination(this StringBuilder print, int qtdDenomination, string denomination)
		{
			if (qtdDenomination > 0)
			{
				if (print.Length > 0)
				{
					print.Append(" + ");
				}

				print.Append($"{qtdDenomination} X {denomination}");
			}
		}
	}
}
