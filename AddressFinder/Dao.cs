using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Reflection.PortableExecutable;

public class Dao
{
    public const string DB_FILENAME = @"data.db";
    public const string DATETIME_FORMAT = @"yyyy-MM-dd HH:mm:ss";
    public const string DATE_FORMAT = @"yyyy-MM-dd";
    public const string TIME_FORMAT = @"HH:mm";
    public static SQLiteConnection Connection { get; }
    public static bool Encrypted;

    static Dao()
    {
        FileInfo fileInfo = new(DB_FILENAME);
        if (!fileInfo.Exists) throw new FileNotFoundException("Can not find database file - " + DB_FILENAME);
        string connectionstring = @"Data Source=" + DB_FILENAME + ";Version=3";
        SQLiteConnection con = new(connectionstring);
        con.Open();
        using (SQLiteCommand command = con.CreateCommand())
        {
            command.CommandText = "PRAGMA encoding";
            object result = command.ExecuteScalar();
        }
        Encrypted = false;
        Connection = con;
    }

    public static bool Close()
    {
        try
        {
            Connection.Close();
            Connection.Dispose();
            return true;
        }
        catch { }
        return false;
    }

    public static long CountAll(string type)
    {
        using SQLiteCommand command = Connection.CreateCommand();
        command.CommandText = $"SELECT COUNT(*) FROM {type}";
        using SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
            return (long)reader[0];
        return 0;
    }

    public static string? SelectAddress(string type, string id)
    {
        using SQLiteCommand command = Connection.CreateCommand();
        command.CommandText = $"SELECT address FROM {type} WHERE id=@id";
        command.Parameters.AddWithValue("@id", id);
        command.Prepare();
        using SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
            return (string)reader["address"];
        return null;
    }

    public static string? SelectMaxKey(string type)
    {
        using SQLiteCommand command = Connection.CreateCommand();
        command.CommandText = $"SELECT MAX(key) FROM {type}";
        using SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
            return (reader[0] as string) ?? null;
        return null;
    }

    public static int Insert(string type, string id, string address, string key)
    {
        using var command = Connection.CreateCommand();
        command.CommandText = $"INSERT INTO {type}(id, address, key) VALUES(@id, @address, @key)";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@address", address);
        command.Parameters.AddWithValue("@key", key);
        command.Prepare();
        return command.ExecuteNonQuery();
    }

    public static int Insert(string type, string id, string address, string key, string wif)
    {
        using var command = Connection.CreateCommand();
        command.CommandText = $"INSERT INTO {type}(id, address, key, wif) VALUES(@id, @address, @key, @wif)";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@address", address);
        command.Parameters.AddWithValue("@key", key);
        command.Parameters.AddWithValue("@wif", wif);
        command.Prepare();
        return command.ExecuteNonQuery();
    }

    //public static void Encrypt()
    //{
    //    connection.ChangePassword(DB_PASSWORD);
    //}

    //public static void Decrypt()
    //{
    //    connection.ChangePassword("");
    //}

    public static T? GetValue<T>(object obj)
    {
        Type type = typeof(T);
        if (obj == null || obj == DBNull.Value)
        {
            return default; // returns the default value for the type
        }
        else if (type == typeof(DateTime))
        {
            return (T)(Object)DateTime.ParseExact((string)obj, DATETIME_FORMAT, CultureInfo.CurrentCulture);
        }
        else
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }

    public static string ToDateString(DateTime dt)
    {
        return dt.ToString(DATE_FORMAT);
    }

    public static DateTime ParseDateString(string s)
    {
        return DateTime.ParseExact(s, DATE_FORMAT, CultureInfo.CurrentCulture);
    }

    public static string ToDateTimestring(DateTime dt)
    {
        return dt.ToString(DATETIME_FORMAT);
    }

    public static DateTime ParseDateTimeString(string s)
    {
        return DateTime.ParseExact(s, DATETIME_FORMAT, CultureInfo.CurrentCulture);
    }

}
