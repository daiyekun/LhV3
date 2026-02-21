using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Model.Extend
{
    interface IEntity
    {
    }
    
   public interface ICreateUser
    {
        /// <summary>
        /// 创建人
        /// </summary>
       int CreateUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        UserInfor CreateUser { get; set; }

    }
    public interface IPrincipalUser
    {
        /// <summary>
        /// 创建人
        /// </summary>
        int? PrincipalUserId { get; set; }
      

    }

}
