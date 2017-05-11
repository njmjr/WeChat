/*******************************************************************************
* 创建描述：由T4引擎自动创建于 2015/10/12 9:40:54
*******************************************************************************/

using System;
using Dapper.Contrib.Extensions;
using WeChat.Models.Base;

namespace WeChat.Models
{
	///<summary>
	///TD_M_INSIDEDEPART  model实体类
	///</summary>
	[Table("TD_M_INSIDEDEPART")]
	public class InsideDepart :IEntity
	{
		/// <summary>
		/// 获取或设置DEPARTNO
		/// </summary>
		public string DEPARTNO { get; set; }
		/// <summary>
		/// 获取或设置DEPARTNAME
		/// </summary>
		public string DEPARTNAME { get; set; }
		/// <summary>
		/// 获取或设置UPDATESTAFFNO
		/// </summary>
		public string UPDATESTAFFNO { get; set; }
		/// <summary>
		/// 获取或设置UPDATETIME
		/// </summary>
		public DateTime UPDATETIME { get; set; }
		/// <summary>
		/// 获取或设置REMARK
		/// </summary>
		public string REMARK { get; set; }
		/// <summary>
		/// 获取或设置USETAG
		/// </summary>
		public string USETAG { get; set; }
	}
}

	