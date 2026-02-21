using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
   
        /// <summary>
        /// 微信分页对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class PageWxInfo<T>
            where T : class, new()

        {
            /// <summary>
            /// 页码
            /// </summary>
            private int pgIndex;
            /// <summary>
            /// 每页条数
            /// </summary>
            private int pgSize;
            /// <summary>
            /// 总记录数
            /// </summary>
            private int rowCount;
            /// <summary>
            /// 数据
            /// </summary>
            private IList<T> pgList;
           

            public virtual int PgIndex { get => pgIndex <= 0 ? 1 : pgIndex; set => pgIndex = value; }
            public virtual int PgSize { get => pgSize <= 0 ? 15 : pgSize; set => pgSize = value; }
            public virtual int RowCount { get => rowCount; set => rowCount = value; }
            public virtual IList<T> Pglist { get => pgList; set => pgList = value; }
           
            public PageWxInfo()
            {


            }
            public PageWxInfo(int _pgIndex, int _pgSize)
            {
                pgIndex = _pgIndex;
                pgSize = _pgSize;
            }


        }

    }

