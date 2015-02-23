using UnityEngine;
using System.Collections;

public class CmdLine{

	private string _fileName;
	private bool _record;
	private string[] _cmdlArgs;
	private bool _dataExists;

	public bool DataExists
	{
		get { return _dataExists; }
	}

	public string FileName 
	{ 
		get { return _fileName; }
	}

	public bool Record
	{
		get { return _record; }
	}

	// Use this for initialization
	public CmdLine()
	{
		_dataExists = false;
	}

	public void Parse()
	{
		try
		{
			_cmdlArgs = System.Environment.GetCommandLineArgs();
			var rec = System.Array.FindAll(_cmdlArgs, s => s.Equals("-record"));
			var fileFlagIndex = System.Array.FindIndex(_cmdlArgs, s => s.Equals("-file"));
			if (fileFlagIndex == -1) 
				return;
			var filename = _cmdlArgs[fileFlagIndex+1];
	
			_record = rec.Length > 0;
			_fileName = filename;
			_dataExists = true;
		}
		catch(System.Exception)
		{
			_dataExists = false;
		}

	}
}
