using AutoMapper;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.BLL
{
    public class EventUtility
    {
        public event Action<ContractInfo,ContractInfoHistory>  ContHistoryEvent;
        /// <summary>
        /// 执行添加表以后的后续动作
        /// </summary>
        /// <param name="contract">当前合同对方</param>
        /// <param name="contractInfoHistory">合同历史</param>
        public void ExceContHistoryEvent(ContractInfo contract, ContractInfoHistory contractInfoHistory)
        {
            this.ContHistoryEvent?.Invoke(contract, contractInfoHistory);
        }
        /// <summary>
        /// 发票事件
        /// </summary>
        public event Action<ContInvoice> ContInvoiceEvent;
        /// <summary>
        /// 发票事件
        /// </summary>
        /// <param name="contInvoice">发票对象</param>
        public void ExceContInvoiceEvent(ContInvoice contInvoice)
        {
            this.ContInvoiceEvent?.Invoke(contInvoice);
        }

        /// <summary>
        /// 招标事件
        /// </summary>
        public event Action<TenderInfor> TenderInforEvent;
        /// <summary>
        /// 招标事件
        /// </summary>
        /// <param name="tenderInfor">招标对象</param>
        public void ExceTenderInforEvent(TenderInfor tenderInfor)
        {
            this.TenderInforEvent?.Invoke(tenderInfor);
        }
        public event Action<Inquiry> InquiryEvent;
        public void ExceInquiryEvent(Inquiry inquiry)
        {
            this.InquiryEvent?.Invoke(inquiry);
        }
        public event Action<Questioning>QuestioningEvent;
        public void ExceQuestioningEvent(Questioning questioning)
        {
            this.QuestioningEvent?.Invoke(questioning);
        }
    }
}
