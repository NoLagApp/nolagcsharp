namespace NoLagClient.Shared.Utilities;

public static class UnitSeparator
{
    public static byte[] ToUnitSeparator(IEnumerable<string> unitArray)
    {
        int byteLength = 0;

        // add space for separator
        var enumerable = unitArray as string[] ?? unitArray.ToArray();
        if (!string.IsNullOrEmpty(enumerable[0]) && !string.IsNullOrEmpty(enumerable[1]))
        {
            // total byte data
            byteLength = enumerable[0].Length + enumerable[1].Length;
            // add byte length for separator
            byteLength += 1;
        }
        else if (!string.IsNullOrEmpty(enumerable[0]))
        {
            byteLength = enumerable[0].Length;
        }

        byte[] unitData = new byte[byteLength];

        if (!string.IsNullOrEmpty(enumerable[0]) && !string.IsNullOrEmpty(enumerable[1]))
        {
            unitData = ToByteArray(unitData, enumerable[0], 0);
            unitData[enumerable[0].Length] = (byte)SeparatorEnumeration.Group.SeparatorAscii;
            unitData = ToByteArray(unitData, enumerable[1], enumerable[0].Length + 1);
        }
        else if (!string.IsNullOrEmpty(enumerable[0]))
        {
            unitData = ToByteArray(unitData, enumerable[0], 0);
        }

        return unitData;
    }

    private static byte[] ToByteArray(byte[] array, string input, int startIndex)
    {
        for (int i = 0; i < input.Length; i++)
        {
            array[startIndex + i] = (byte)input[i];
        }
        return array;
    }
}