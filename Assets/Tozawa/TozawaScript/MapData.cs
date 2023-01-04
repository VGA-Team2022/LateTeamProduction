using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
public class MapData
{
    [Tooltip("CSVのデータを公開するための変数")]
    public List<int[]> data = new List<int[]>();
    /// <summary>
    /// このコンストラクタでCSVのデータを読み取る
    /// CSV側でデータの説明をしているので0列目は破棄する
    /// データはintの多重配列で公開する
    /// <param name="filePath">ファイルのパス</param>
    public MapData(string filePath)
    {
        TextAsset file = Resources.Load<TextAsset>(filePath);
        if (!file)//パスの先に該当ファイルがない場合にエラーログ
        {
            Debug.LogError("!Warning! Your security clearance is not allowed to view this file.");
            return;
        }
        StringReader reader = new StringReader(file.text);
        reader.ReadLine(); //一行目破棄 
        string rows2Bdiscarded;
        while((rows2Bdiscarded = reader.ReadLine()) != null)
        {
            var line = rows2Bdiscarded.Split(',').Select(x=>int.Parse(x)).ToArray(); //一行ごとに読み込み
            data.Add(line);
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