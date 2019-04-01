using System;
using System.Collections.Generic;
using System.Linq;

public class StringListOutput
{

    /// <summary>
    /// Linuxのlsコマンドみたいにリスト表示します。
    /// </summary>
    /// <param name="list">コレクション</param>
    public static void WriteList(System.Collections.IEnumerable list)
    {
        //文字との隙間
        const int margin = 2;
        //文字列の配列に変換
        List<string> stringList = new List<string>();
        foreach (var item in list)
        {
            stringList.Add(item.ToString());
        }
        //横に並べられる数(列)の個数を数える
        int columnCount = Console.BufferWidth;
        int stringLength = ZenHanTextCounter.Count(stringList[0]) + margin;
        int listIndex = 1;
        while (listIndex < stringList.Count)
        {
            int tempColumnCount = 0;
            bool bufferOver = false;
            bool indexOver = false;
            while ( true )
            {
                if (stringLength > Console.BufferWidth)
                {
                    bufferOver = true;
                    break;
                }
                if (!(tempColumnCount < columnCount))
                {
                    break;
                }
                if (!(listIndex < stringList.Count))
                {
                    indexOver = true;
                    listIndex = 0;
                }
                stringLength += ZenHanTextCounter.Count(stringList[listIndex]) + margin;
                tempColumnCount++;
                listIndex++;
            }
            if (bufferOver)
            {
                tempColumnCount--;
            }
            if (listIndex < stringList.Count)
            {
                if (tempColumnCount < columnCount)
                {
                    listIndex = 0;
                }
                else if (indexOver)
                {
                    break;
                }
                stringLength = ZenHanTextCounter.Count(stringList[listIndex]) + margin;
                listIndex++;
            }
            columnCount = tempColumnCount;
        }
        //列が決定
        //二次元配列へ格納
        listIndex = 0;
        //行　列
        List<List<string>> string2DList = new List<List<string>>();
        while (listIndex < stringList.Count)
        {
            List<string> tempList = new List<string>();
            for (int i = 0; i < columnCount; i++)
            {
                if (listIndex < stringList.Count)
                {
                    tempList.Add(stringList[listIndex]);
                }
                else
                {
                    tempList.Add(null);
                }
                listIndex++;
            }
            string2DList.Add(tempList);
        }
        //各列の最大文字数を導き出す。
        List<int> columnMaxLen = new List<int>();
        columnMaxLen.Add(0);//一列目は、必ず先頭から始まる
        for (int i = 0; i < string2DList[0].Count; i++)
        {
            int tempMax = 0;
            for (int j = 0; j < string2DList.Count; j++)
            {
                if (!string.IsNullOrEmpty(string2DList[j][i]))
                {
                    int strLen = ZenHanTextCounter.Count(string2DList[j][i]);
                    if ( strLen > tempMax)
                    {
                        tempMax = strLen;
                    }
                }
            }
            columnMaxLen.Add(columnMaxLen.Last() + tempMax + margin);
        }
        //表示
        foreach (var row in string2DList)
        {
            for (int i = 0; i < row.Count; i++)
            {
                if (!string.IsNullOrEmpty(row[i]))
                {
                    Console.CursorLeft = columnMaxLen[i];
                    Console.Write(row[i]);
                }
            }
            Console.WriteLine();
        }
    }
}

