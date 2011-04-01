using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CATALYST.Core.Actions.Config
{
	public interface IGetPathToEdmxFileAction
	{
		string Execute();
	}

	public class GetPathToEdmxFileAction : IGetPathToEdmxFileAction
	{
		public string Execute()
		{
			//return @"C:\_APPLICATION\FSHNTWEB\FSHNTWEB\FSHNTWEB.Data\Objects\DataModel.edmx";
			//return @"C:\_APPLICATION\CATALYST\CATALYST\CATALYST.Bootstrap\Model1.edmx";
			var state = new Core();
			return state.GetState().PathToEntityDataModel;
			//return @"C:\_APPLICATION\HLLMRKDI\HLLMRKDI\HLLMRKDI.Data\Objects\DataModel.edmx";
		}
	}
}
