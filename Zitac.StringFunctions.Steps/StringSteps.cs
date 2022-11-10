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


}
