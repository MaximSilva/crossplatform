namespace Lab1_42
{
	public class Program
	{
		public static string InputFilePath = @"..\..\input.txt";
		public static string OutputFilePath = @"..\..\output.txt";

		static void Main(string[] args)
		{
			FileInfo outputFileInfo = new FileInfo(OutputFilePath);
			var lineWithAmountOfPartitions = File.ReadLines(InputFilePath).FirstOrDefault();
			if (lineWithAmountOfPartitions is not null && 
				int.Parse(lineWithAmountOfPartitions) >= 1 && 
				int.Parse(lineWithAmountOfPartitions) <= 256)
			{
				List<Tuple<decimal, decimal>> inputData = File.ReadLines(InputFilePath).Skip(1).
					Select(tpl =>
					{
						tpl = tpl.Replace('.', ',');
						int spaceIndex = tpl.IndexOf(' ');
						return new Tuple<decimal, decimal>(Decimal.Parse(tpl.Substring(0, spaceIndex)), Decimal.Parse(tpl.Substring(spaceIndex + 1)));
					}
					).ToList();

				decimal resultTime = 0;
				string resultRow = String.Empty;

				for (int i = 0; i < inputData.Count; i++)
				{
					if (inputData[i].Item1 > inputData[i].Item2)
					{
						resultTime += inputData[i].Item1;
						resultRow = Convert.ToString(i + 1) + ' ' + resultRow.Trim();
					}
					else
					{
						resultTime += inputData[i].Item2;
						resultRow += ' ' + Convert.ToString(i + 1);
					}
				}
				resultRow = resultRow.Trim();

				using (StreamWriter streamWriter = outputFileInfo.CreateText())
				{
					streamWriter.WriteLine(resultTime);
					streamWriter.WriteLine(resultRow);
				}
			}
			else
			{
				using (StreamWriter streamWriter = outputFileInfo.CreateText())
				{
					streamWriter.WriteLine("Out of range exception");
				}
			}
		}
	}
}