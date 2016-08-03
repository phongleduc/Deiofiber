using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deiofiber.Common
{
    public class ContractUpdateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            CommonList.AutoExtendContract();
        }
    }
}