using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
public class MapData
{
    [Tooltip("CSVのデータを公開するための変数")]
    public int[][] data = null;
    /// <summary>
    /// このコンストラクタでCSVのデータを読み取る
    /// CSV側でデータの説明をしているので0列目は破棄する
    /// データはintの多重配列で公開する
    /// <param name="filePath">ファイルのパス</param>
    public MapData(string filePath)
    {
        if (!File.Exists(filePath))//パスの先に該当ファイルがない場合にエラーログ
        {
            Debug.LogError("!Warning! Your security clearance is not allowed to view this file.");
            return;
        }
        StreamReader reader = new StreamReader(filePath);
        string rows2Bdiscarded = reader.ReadLine(); //一行目破棄 
        while(!reader.EndOfStream)
        {
            int i = 0;
            var line = reader.ReadLine().Split(',').Select(x=>int.Parse(x)).ToArray(); //一行ごとに読み込み
            data[i]= line;
            i++;
        }
    }
    public enum MapElemantEntity
    {
        sleeperValue = 0,
        nakaiValue = 1,
        houseValue = 2,
        houseValueOnSolt = 3,
        houseValueInBaby = 4,
        houseValueInArrow = 5,
        houseValueDoubleType = 6,
    }
}