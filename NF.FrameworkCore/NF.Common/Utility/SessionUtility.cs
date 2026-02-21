using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
   public class SessionUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 获取SessionUserId
        /// </summary>
        public  int?  GetSessionUserId()
        {
            return _session.GetInt32(StaticData.NFUserId);

        }
    }
}
