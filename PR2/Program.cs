using System.IO;

public class Program
{
    private static string inputFilepath = "input.txt";
    private static string outputFilepath = "output.txt";
    private static string invalidFileContentErrorMsg = "input.txt must contains only two lines: first line contains one integer number N, second line contains N integer numbers. ";
    private static string fileNotFoundErrorMsg = "input.txt was not found. Put the file in the same directory as this .exe file. ";
    static void Main(string[] args)
    {
        string outputMsg;
        if (File.Exists(inputFilepath))
        {
            List<string> fileLies = new List<string>(System.IO.File.ReadAllLines(inputFilepath));
            List<int> numberSequence = new List<int>();
            bool isAllNumbersParsed = false;
            if (fileLies.Count == 2)
            {
                isAllNumbersParsed = true;
                foreach (string numberAsStr in fileLies[1].Split(' '))
                {
                    int parsedNumber;
                    isAllNumbersParsed &= Int32.TryParse(numberAsStr, out parsedNumber);
                    if (Int32.TryParse(numberAsStr, out parsedNumber))
                        numberSequence.Add(parsedNumber);
                    else
                    {
                        isAllNumbersParsed = false;
                        break;
                    }
                }
            }
            else
                outputMsg = invalidFileContentErrorMsg;
            if (isAllNumbersParsed && numberSequence.Count > 2)
            {
                int fineSum = 0;
                while (numberSequence.Count > 2)
                {
                    int indexOfChipestNum = findChipestNumberToRemove(numberSequence);
                    fineSum += calcCostOfRemoving(indexOfChipestNum, numberSequence);
                    numberSequence.RemoveAt(indexOfChipestNum);
                }
                outputMsg = fineSum.ToString();
            }
            else
                outputMsg = invalidFileContentErrorMsg;
        }
        else
            outputMsg = fileNotFoundErrorMsg;
        File.WriteAllText(outputFilepath, outputMsg);
    }

    private static int calcCostOfRemoving(int numIndex, List<int>numbers)
    {
        return numbers[numIndex] * (numbers[numIndex - 1] + numbers[numIndex + 1]);
    }

    private static int findChipestNumberToRemove(List<int> numbers)
    {
        int minCostOfRemoving = int.MaxValue;
        int indexOfChipestNum = 1;
        for (int numNo = 1; numNo < numbers.Count - 1; ++numNo)
        {
            int costOfRemoving = calcCostOfRemoving(numNo, numbers);
            if (costOfRemoving < minCostOfRemoving)
            {
                minCostOfRemoving = costOfRemoving;
                indexOfChipestNum = numNo;
            }
        }
        return indexOfChipestNum;
    }
}