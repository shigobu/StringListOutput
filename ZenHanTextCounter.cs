/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016 DOBON! <http://dobon.net>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

/// <summary>
/// 全角は二文字、半角は一文字として文字数を数える関数を提供する。
/// 他、全角・半角を判定する関数を提供する。
/// 参考URL　https://dobon.net/vb/dotnet/string/ishiragana.html
/// </summary>
class ZenHanTextCounter
{
    /// <summary>
    /// 全角を２、半角を１と数える。
    /// </summary>
    /// <param name="str">数える文字列</param>
    /// <returns>全角を２文字、半角を１文字と数える文字数</returns>
    internal static int Count(string str)
    {
        int count = 0;
        foreach (char c in str)
        {
            if (IsZenkaku(c))
            {
                count += 2;
            }
            else
            {
                count++;
            }
        }
        return count;
    }
	/// <summary>
	/// 指定した Unicode 文字が、半角かどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c が半角である場合は true 。それ以外は false。</returns>
	internal static bool IsZenkaku(char c)
    {
        return IsHiragana(c) || IsFullwidthKatakana(c) || IsKanji(c) || IsFullwidthDigit(c) || IsUpperLatin(c) || IsLowerLatin(c);
    }
	/// <summary>
	/// 指定した Unicode 文字が、ひらがなかどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c がひらがなである場合は true。それ以外の場合は false。</returns>
	internal static bool IsHiragana(char c)
    {
        //「ぁ」～「より」までと、「ー」「ダブルハイフン」をひらがなとする
        return ('\u3041' <= c && c <= '\u309F')
            || c == '\u30FC' || c == '\u30A0';
    }
	/// <summary>
	/// 指定した Unicode 文字が、全角カタカナかどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c が全角カタカナである場合は true。それ以外の場合は false。</returns>
	internal static bool IsFullwidthKatakana(char c)
    {
        //「ァ」から「コト」までと、カタカナフリガナ拡張と、
        //濁点と半濁点を全角カタカナとする
        //中点と長音記号も含む
        return ('\u30A1' <= c && c <= '\u30FF')
            || ('\u31F0' <= c && c <= '\u31FF')
            || ('\u3099' <= c && c <= '\u309C');
    }
	/// <summary>
	/// 指定した Unicode 文字が、漢字かどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c が漢字である場合は true。それ以外の場合は false。</returns>
	internal static bool IsKanji(char c)
    {
        //CJK統合漢字、CJK互換漢字、CJK統合漢字拡張Aの範囲にあるか調べる
        return ('\u4E00' <= c && c <= '\u9FCF')
            || ('\uF900' <= c && c <= '\uFAFF')
            || ('\u3400' <= c && c <= '\u4DBF');
    }
	/// <summary>
	/// 指定した Unicode 文字が、０ から ９ までの数字かどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c が数字である場合は true。それ以外の場合は false。</returns>
	internal static bool IsFullwidthDigit(char c)
    {
        return '０' <= c && c <= '９';
    }
	/// <summary>
	/// 指定した Unicode 文字が、英字の大文字かどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c が英字の大文字である場合は true。
	/// それ以外の場合は false。</returns>
	internal static bool IsUpperLatin(char c)
    {
        //全角英字の大文字の時はTrue
        return 'Ａ' <= c && c <= 'Ｚ';
    }
	/// <summary>
	/// 指定した Unicode 文字が、英字の小文字かどうかを示します。
	/// </summary>
	/// <param name="c">評価する Unicode 文字。</param>
	/// <returns>c が英字の小文字である場合は true。
	/// それ以外の場合は false。</returns>
	internal static bool IsLowerLatin(char c)
    {
        //全角英字の小文字の時はTrue
        return 'ａ' <= c && c <= 'ｚ';
    }
}