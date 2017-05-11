/*******************************************************************************
* 创建描述：由T4引擎自动创建于 2015/10/12 9:40:54
*******************************************************************************/

using Dapper.Contrib.Extensions;
using WeChat.Models.Base;

namespace WeChat.Models
{
	///<summary>
	///TD_M_ROLEPOWER  model实体类
	///</summary>
	[Table("TD_M_ROLEPOWER")]
	public class RolePower :IEntity
	{
		/// <summary>
		/// 获取或设置ROLENO
		/// </summary>
		[Key]
		public string ROLENO { get; set; }
		/// <summary>
		/// 获取或设置POWERCODE
		/// </summary>
		[Key]
		public string POWERCODE { get; set; }
		/// <summary>
		/// 获取或设置POWERTYPE
		/// </summary>
		public string POWERTYPE { get; set; }
		/// <summary>
		/// 获取或设置REMARK
		/// </summary>
		public string REMARK { get; set; }
	}
}

	