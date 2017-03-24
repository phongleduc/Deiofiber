using Deiofiber.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deiofiber
{
    public partial class DailyBatchProcessing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Lock on the readonly object.
                if (!Singleton.IsRunningBatch())
                {
                    Singleton.CreateSingletonFile();

                    Logger.Log("Backup database start");
                    CommonList.BackUp();
                    Logger.Log("Backup database end");

                    Logger.Log("Auto extend contract start");
                    CommonList.AutoExtendContract();
                    Logger.Log("Auto extend contract end");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error Entry at: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                Singleton.DeleteSingletonFile();
            }
        }
    }
}