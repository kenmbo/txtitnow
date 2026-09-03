using System.Text;

namespace TxtItNow;

internal sealed class TextFileEncoding
{
    public static TextFileEncoding Utf8WithoutBom { get; } = new(
        "UTF-8",
        new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true),
        [0xEF, 0xBB, 0xBF],
        writesByteOrderMark: false);

    public static TextFileEncoding Utf8WithBom { get; } = new(
        "UTF-8 with BOM",
        new UTF8Encoding(encoderShouldEmitUTF8Identifier: true, throwOnInvalidBytes: true),
        [0xEF, 0xBB, 0xBF],
        writesByteOrderMark: true);

    public static TextFileEncoding Utf16LittleEndian { get; } = new(
        "UTF-16 LE",
        new UnicodeEncoding(bigEndian: false, byteOrderMark: true, throwOnInvalidBytes: true),
        [0xFF, 0xFE],
        writesByteOrderMark: true);

    public static TextFileEncoding Utf16BigEndian { get; } = new(
        "UTF-16 BE",
        new UnicodeEncoding(bigEndian: true, byteOrderMark: true, throwOnInvalidBytes: true),
        [0xFE, 0xFF],
        writesByteOrderMark: true);

    public static TextFileEncoding Utf32LittleEndian { get; } = new(
        "UTF-32 LE",
        new UTF32Encoding(bigEndian: false, byteOrderMark: true, throwOnInvalidCharacters: true),
        [0xFF, 0xFE, 0x00, 0x00],
        writesByteOrderMark: true);

    public static TextFileEncoding Utf32BigEndian { get; } = new(
        "UTF-32 BE",
        new UTF32Encoding(bigEndian: true, byteOrderMark: true, throwOnInvalidCharacters: true),
        [0x00, 0x00, 0xFE, 0xFF],
        writesByteOrderMark: true);

    public static IReadOnlyList<TextFileEncoding> SupportedEncodings { get; } =
    [
        Utf8WithoutBom,
        Utf8WithBom,
        Utf16LittleEndian,
        Utf16BigEndian,
        Utf32LittleEndian,
        Utf32BigEndian
    ];

    private static IReadOnlyList<TextFileEncoding> ByteOrderMarkDetectionOrder { get; } =
    [
        Utf32BigEndian,
        Utf32LittleEndian,
        Utf8WithBom,
        Utf16BigEndian,
        Utf16LittleEndian
    ];

    private readonly byte[] byteOrderMark;

    private TextFileEncoding(
        string displayName,
        Encoding encoding,
        byte[] byteOrderMark,
        bool writesByteOrderMark)
    {
        DisplayName = displayName;
        Encoding = encoding;
        this.byteOrderMark = byteOrderMark;
        WritesByteOrderMark = writesByteOrderMark;
    }

    public string DisplayName { get; }

    private Encoding Encoding { get; }

    private bool WritesByteOrderMark { get; }

    public string Decode(byte[] fileBytes, int byteOrderMarkLength)
    {
        return Encoding.GetString(
            fileBytes,
            byteOrderMarkLength,
            fileBytes.Length - byteOrderMarkLength);
    }

    public byte[] Encode(string text)
    {
        byte[] encodedText = Encoding.GetBytes(text);

        if (!WritesByteOrderMark)
        {
            return encodedText;
        }

        byte[] fileBytes = new byte[byteOrderMark.Length + encodedText.Length];
        byteOrderMark.CopyTo(fileBytes, 0);
        encodedText.CopyTo(fileBytes, byteOrderMark.Length);
        return fileBytes;
    }

    public int GetMatchingByteOrderMarkLength(ReadOnlySpan<byte> fileBytes)
    {
        return fileBytes.StartsWith(byteOrderMark)
            ? byteOrderMark.Length
            : 0;
    }

    public static bool TryDetectFromByteOrderMark(
        ReadOnlySpan<byte> fileBytes,
        out TextFileEncoding? encoding,
        out int byteOrderMarkLength)
    {
        foreach (TextFileEncoding candidate in ByteOrderMarkDetectionOrder)
        {
            int candidateLength = candidate.GetMatchingByteOrderMarkLength(fileBytes);

            if (candidateLength > 0)
            {
                encoding = candidate;
                byteOrderMarkLength = candidateLength;
                return true;
            }
        }

        encoding = null;
        byteOrderMarkLength = 0;
        return false;
    }

    public override string ToString()
    {
        return DisplayName;
    }
}
