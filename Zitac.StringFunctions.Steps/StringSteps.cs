using DecisionsFramework.Design.Flow;
using System.Globalization;
using System.Text;

namespace Zitac.StringFunctions.Steps;

[AutoRegisterMethodsOnClass(true, "Data", "Text")]
public class StringSteps
{
    public static string RemoveAccentsAndDiacritics(string text){     
    StringBuilder sbReturn = new StringBuilder();     
    var arrayText = text.Normalize(System.Text.NormalizationForm.FormD).ToCharArray();  
   foreach (char letter in arrayText){     
        if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)  
        sbReturn.Append(letter);
    }
    byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(sbReturn.ToString());
    string asciiStr = Encoding.UTF8.GetString(tempBytes);
    return asciiStr;
} 
public static string DecodeDockerLog(byte[] input)
{
    int offset = 0;
    var output = new List<byte>();

    while (offset < input.Length)
    {
        // Check if there's at least 8 bytes left for the header
        if (offset + 8 > input.Length)
            throw new InvalidOperationException("Unexpected end of data, header incomplete.");

        // Extract the size of the data frame from the header bytes 4-7 (big-endian)
        int dataSize = BitConverter.ToInt32(new[] { input[offset + 7], input[offset + 6], input[offset + 5], input[offset + 4] });

        // Check if there's enough data left for the data content
        if (offset + 8 + dataSize > input.Length)
            throw new InvalidOperationException("Unexpected end of data, content incomplete.");

        // Copy the content data to the output, skipping the header
        for (int i = 0; i < dataSize; i++)
        {
            output.Add(input[offset + 8 + i]);
        }

        // Move the offset to the next frame
        offset += 8 + dataSize;
    }

    return Encoding.UTF8.GetString(output.ToArray());
}

}


