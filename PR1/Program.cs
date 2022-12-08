using System.IO;

public class Program
{
    private static string inputFilepath = "input.txt";
    private static string outputFilepath = "output.txt";
    private static string invalidFileContentErrorMsg = "input.txt must contain only two numbers N and M separated with one whitespace. Both of N and M must be integer values greater then 0. ";
    private static string fileNotFoundErrorMsg = "input.txt was not found. Put the file in the same directory as this .exe file. ";
    static void Main(string[] args)
    {
        string outputMsg;
        if (File.Exists(inputFilepath))
        {
            List<string> fileContent = new List<string>(System.IO.File.ReadAllText(inputFilepath).Split(' '));
            int leavesNum = 0;
            int mod = 0;
            if(fileContent.Count == 2)
            {
                bool isLeavesNumValid = Int32.TryParse(fileContent[0], out leavesNum);
                isLeavesNumValid &= leavesNum > 0;
                bool isModValid = Int32.TryParse(fileContent[1], out mod);
                isModValid &= mod > 0;
                if (isLeavesNumValid && isModValid)
                    outputMsg = (treeCombinations(leavesNum) % mod).ToString();
                else
                    outputMsg = invalidFileContentErrorMsg;
            }
            else
                outputMsg = invalidFileContentErrorMsg;
        }
        else
            outputMsg = fileNotFoundErrorMsg;
        File.WriteAllText(outputFilepath, outputMsg);
    }

    static private int treeCombinations(int leavesNum)
    {
        if (leavesNum < 5)
            return 1;
        return treeCombinations(leavesNum - 2) + treeCombinations(leavesNum - 3);
    }
}