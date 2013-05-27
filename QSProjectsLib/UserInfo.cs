using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace QSProjectsLib
{
	public class UserInfo
	{
		public string Name, Login;
		public int id;
		public bool admin;
		public Dictionary<string, bool> Permissions;

		public UserInfo ()
		{

		}

		public bool TestUserExistByLogin(bool CreateNotExist)
		{
			QSMain.OnNewStatusText("Проверка наличия пользователя в базе...");
			try
			{
				string sql = "SELECT COUNT(*) AS cnt FROM users WHERE login = @login";
				MySqlCommand cmd = new MySqlCommand(sql, QSMain.connectionDB);
				cmd.Parameters.AddWithValue("@login", Login);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Read();
				bool Exist = false;
				if (rdr["cnt"].ToString() != "0")
					Exist = true;
				rdr.Close();
				
				if( CreateNotExist && !Exist)
				{
					bool FirstUser = false;
					sql = "SELECT COUNT(*) AS cnt FROM users";
					cmd = new MySqlCommand(sql, QSMain.connectionDB);
					rdr = cmd.ExecuteReader();
					rdr.Read();
					if (rdr["cnt"].ToString() == "0")
						FirstUser = true;
					rdr.Close();
					QSMain.OnNewStatusText("Создаем пользователя");
					sql = "INSERT INTO users (login, name, " + QSMain.AdminFieldName + ") " +
						"VALUES (@login, @login, @admin)";
					cmd = new MySqlCommand(sql, QSMain.connectionDB);
					cmd.Parameters.AddWithValue("@login", Login);
					cmd.Parameters.AddWithValue("@admin", FirstUser);
					cmd.ExecuteNonQuery();
					Exist = true;
				}
				return Exist;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				QSMain.OnNewStatusText("Ошибка проверки пользователя!");
				return false;
			}
		}
		
		public void UpdateUserInfoByLogin()
		{
			QSMain.OnNewStatusText("Чтение информации о пользователе...");
			try
			{
				string sql = "SELECT * FROM users WHERE login = @login";
				MySqlCommand cmd = new MySqlCommand(sql, QSMain.connectionDB);
				cmd.Parameters.AddWithValue("@login", Login);
				MySqlDataReader rdr = cmd.ExecuteReader();
				rdr.Read();

				Name = rdr["name"].ToString();
				id = rdr.GetInt32("id");
				admin = rdr.GetBoolean (QSMain.AdminFieldName);

				Permissions = new Dictionary<string, bool>();
				foreach( KeyValuePair<string, UserPermission> Right in QSMain.ProjectPermission)
				{
					string FieldName = Right.Value.DataBaseName;
					Permissions.Add (Right.Key, rdr.GetBoolean (FieldName));
				}

				rdr.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				QSMain.OnNewStatusText("Ошибка чтения информации о пользователе!");
			}	
		}

		public void ChangeUserPassword( Gtk.Window Parrent)
		{
			ChangePassword win = new ChangePassword();
			//win.ParentWindow = Parrent;
			win.TransientFor = Parrent;
			win.Show ();
			win.Run ();
			win.Destroy ();
		}
	}

	public class UserPermission
	{
		public string DataBaseName;
		public string DisplayName;
		public string Description;

		public UserPermission(string DataBaseName, string DisplayName, string Description)
		{
			this.DataBaseName = DataBaseName;
			this.DisplayName = DisplayName;
			this.Description = Description;
		}
	}
}