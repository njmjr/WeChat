using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack;
using ServiceStack.FluentValidation;
using WeChat.DomainService.Application.IService;
using WeChat.Utility;

namespace WeChat.DomainService.Application.Service
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationService()
        {
            TransientDependencies = new List<ITransientDependency>();
        }
        public List<ITransientDependency> TransientDependencies { get; set; }

        public virtual IDbTransaction OpenTx()
        {
            IDbConnection conn = null;
            IDbTransaction tx = null;
            for (int i = 0; i < TransientDependencies.Count; i++)
            {
                if (i == 0)
                {
                    tx = TransientDependencies[i].CreateTx(ref conn);
                }
                else
                {
                    TransientDependencies[i].SetDb(conn, tx);
                }
            }
            return tx;
        }

        public virtual void Dispose()
        {
            foreach (var repository in TransientDependencies)
            {
                repository.Dispose();
            }
        }

        /// <summary>
        /// 拼接输入字段生成记录流水号
        /// </summary>
        /// <param name="asn">ASN</param>
        /// <returns></returns>
        protected string GetId(string asn)
        {
            string nowTime = DateTime.Now.ToString("MMddHHmmss");
            return nowTime + asn.Substring(asn.Length - 8, 8);
        }

        protected void AddMessage(ResponseStatus responseStateus, string msg)
        {
            if (string.IsNullOrEmpty(responseStateus.Message))
            {
                responseStateus.Message = msg;
            }
            else
            {
                responseStateus.Message = responseStateus.Message + '|' + msg;
            }
        }

        /// <summary>
        /// 验证请求
        /// </summary>
        /// <typeparam name="T">请求类</typeparam>
        /// <typeparam name="TK">验证类</typeparam>
        /// <param name="request">请求DTO</param>
        /// <param name="validator">验证规则</param>
        /// <param name="ruleSet"></param> 
        protected void ValidRequest<T, TK>(T request, TK validator, string ruleSet = null)
            where TK : AbstractValidator<T>
        {
            var result = validator.Validate(request, ruleSet: ruleSet);
            if (!result.IsValid)
            {
                List<String> errors = result.Errors.Select(r => r.ToString()).ToList();
                throw new WeChatException("VAILD_ERROR", errors);
            }
        }
    }
}
