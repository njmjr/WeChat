/*******************************************************************************
* 创建描述：由T4引擎自动创建于 2015/10/12 9:40:54
*******************************************************************************/

using System;
using Dapper.Contrib.Extensions;
using WeChat.Models.Base;

namespace WeChat.Models
{
	///<summary>
	///TD_M_INSIDESTAFF  model实体类
	///</summary>
	[Table("TD_M_INSIDESTAFF")]
	public class InsideStaff :IEntity
	{
		/// <summary>
		/// 获取或设置STAFFNO
		/// </summary>
		[Key]
		public string STAFFNO { get; set; }
		/// <summary>
		/// 获取或设置STAFFNAME
		/// </summary>
		public string STAFFNAME { get; set; }
		/// <summary>
		/// 获取或设置OPERCARDPWD
		/// </summary>
		public string OPERCARDPWD { get; set; }
		/// <summary>
		/// 获取或设置DEPARTNO
		/// </summary>
		public string DEPARTNO { get; set; }
		/// <summary>
		/// 获取或设置DIMISSIONTAG
		/// </summary>
		public string DIMISSIONTAG { get; set; }
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
		/// 获取或设置LAST_ACTIVE_TIME
		/// </summary>
		public DateTime LAST_ACTIVE_TIME { get; set; }
		/// <summary>
		/// 获取或设置TOKEN
		/// </summary>
		public string TOKEN { get; set; }
	}
}

	