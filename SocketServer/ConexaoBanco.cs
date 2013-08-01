using System;
using MySql.Data.MySqlClient;

public class ConexaoBanco
{

	MySqlConnection conn = null;
	MySqlTransaction tr = null;

	public void conectar()
	{
		string cs = @"server=localhost;userid=root;password=;database=hvs";

		try
		{
			conn = new MySqlConnection(cs);
			conn.Open();

		} catch (MySqlException ex)
		{
			Console.WriteLine("Error: {0}",  ex.ToString());

		}
	}
	
	public void desconectar(){
		try
		{
			conn.Close();
		} catch (MySqlException ex)
		{
			try
			{
				if(tr != null) tr.Rollback();

			} catch (MySqlException ex1)
			{
				Console.WriteLine("Error: {0}",  ex1.ToString());
			}

			Console.WriteLine("Error: {0}",  ex.ToString());
		}
	}
	
	public bool executar(string comando){
		MySqlCommand cmd = new MySqlCommand();
		try
		{
			
			tr = conn.BeginTransaction();
			
			cmd.Connection = conn;
			cmd.Transaction = tr;

			cmd.CommandText = comando;
			cmd.ExecuteNonQuery();

			tr.Commit();
		} catch (MySqlException ex)
		{
			try
			{
				tr.Rollback();

			} catch (MySqlException ex1)
			{
				Console.WriteLine("Error: {0}",  ex1.ToString());
			}
			
			Console.WriteLine(cmd.CommandText);
			Console.WriteLine("Error: {0}",  ex.ToString());
			return false;
		}
		
		return true;

	}
}