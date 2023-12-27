using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQLite;
using System.Reflection;
using Microsoft.Maui.Graphics;

namespace FinalProject;
public class DB
{
    private static string DBName = "ratings.db";
    public static SQLiteConnection conn;
    public static void OpenConnection()
    {
        string libFolder = FileSystem.AppDataDirectory;
        string fname = System.IO.Path.Combine(libFolder, DBName);
        if (File.Exists(fname))
        {
            conn = new SQLiteConnection(fname);
            //conn.DropTable<MovieDetails>(); conn.CreateTable<MovieDetails>();
            return;
        }
        conn = new SQLiteConnection(fname);
        conn.CreateTable<MovieDetails>();

    }
}