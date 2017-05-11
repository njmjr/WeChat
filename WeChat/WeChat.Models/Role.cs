/*******************************************************************************
* 创建描述：由T4引擎自动创建于 2015/10/12 9:40:54
*******************************************************************************/

using System;
using Dapper.Contrib.Extensions;
using WeChat.Models.Base;

namespace WeChat.Models
{
	///<summary>
    ///TD_M_ROLE  model实体类
	///</summary>
    [Table("TD_M_ROLE")]
    public class Role : IEntity
	{
		/// <summary>
        /// 获取或设置ROLENO
		/// </summary>
		[Key]
        public string ROLENO { get; set; }
		/// <summary>
		/// 获取或设置STAFFNAME
		/// </summary>
        public string ROLENAME { get; set; }
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

	