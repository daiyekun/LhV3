using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Model.Models
{

    /// <summary>
    /// 数据库连接对象，使用配置文件加注入方式
    /// 注意：如果成行实体Models重新生成时，需要注释此方法protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    /// </summary>
    //public partial  class NFDBContext: DbContext
    //{
        //public NFDBContext(DbContextOptions<NFDBContext> options)
        //   : base(options)
        //{

        //}
   // }
}
