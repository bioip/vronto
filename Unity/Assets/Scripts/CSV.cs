﻿// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class contains the script to manage the CSV data structure for "data_final.csv" or "saved_data.csv"
/// </summary>
public class CSV : MonoBehaviour
{
	public TextAsset file;

    void Start()
    {
        Load(file);

    }
	public class Row
	{
		public string ID;
		public string X;
		public string Y;
		public string Z;
		public string Label;
		public string Placed;

	}

	List<Row> rowList = new List<Row>();
	bool isLoaded = false;

	public bool IsLoaded()
	{
		return isLoaded;
	}

	public List<Row> GetRowList()
	{
		return rowList;
	}

	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for(int i = 1 ; i < grid.Length ; i++)
		{
			Row row = new Row();
			row.ID = grid[i][0];
			row.X = grid[i][1];
			row.Y = grid[i][2];
			row.Z = grid[i][3];
			row.Label = grid[i][4];
			row.Placed = grid[i][5];

			rowList.Add(row);
		}
		isLoaded = true;
	}

	public int NumRows()
	{
		return rowList.Count;
	}

	public Row GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}

	public Row Find_ID(string find)
	{
		return rowList.Find(x => x.ID == find);
	}
	public List<Row> FindAll_ID(string find)
	{
		return rowList.FindAll(x => x.ID == find);
	}
	public Row Find_X(string find)
	{
		return rowList.Find(x => x.X == find);
	}
	public List<Row> FindAll_X(string find)
	{
		return rowList.FindAll(x => x.X == find);
	}
	public Row Find_Y(string find)
	{
		return rowList.Find(x => x.Y == find);
	}
	public List<Row> FindAll_Y(string find)
	{
		return rowList.FindAll(x => x.Y == find);
	}
	public Row Find_Z(string find)
	{
		return rowList.Find(x => x.Z == find);
	}
	public List<Row> FindAll_Z(string find)
	{
		return rowList.FindAll(x => x.Z == find);
	}
	public Row Find_Label(string find)
	{
		return rowList.Find(x => x.Label == find);
	}
	public List<Row> FindAll_Label(string find)
	{
		return rowList.FindAll(x => x.Label == find);
	}
	public Row Find_Placed(string find)
	{
		return rowList.Find(x => x.Placed == find);
	}
	public List<Row> FindAll_Placed(string find)
	{
		return rowList.FindAll(x => x.Placed == find);
	}

}