using System;

public class JSONDatabaseService
{
	private string _dataBasePath = "databse.json";
	private JSONDatabaseService()
	{

	}
	private JSONDatabaseService _instance;
	public JSONDatabaseService instance 
	{ 
		get 
		{ 
			if (_instance != null) 
				_instance = new JSONDatabaseService();
			
			return _instance; 
		} 
	}
}
