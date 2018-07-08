using MyDive.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static MyDive.Server.Enums;

namespace MyDive.Server.Log
{
    public class Logger
    {
        private static Logger s_Instance = null;
        private static object s_LoggerLock = new object();

        private Logger()
        {
        }

        public void Notify(string i_Msg, eLogType i_LogType, string i_LogData)
        {
            MyDiveEntities MyDiveDB = null;

            try
            {
                DateTime d = DateTime.Now.Date;
                MyDiveDB = new MyDiveEntities();
                MyDiveDB.stp_InsertLog(
                    (int)i_LogType,
                    i_Msg,
                    DateTime.Now.Date,
                    i_LogData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MyDiveDB != null)
                {
                    MyDiveDB.Dispose();
                }
            }
        }

        public static Logger Instance
        {
            get
            {
                if(s_Instance == null)
                {
                    lock (s_LoggerLock)
                    {
                        if(s_Instance == null)
                        {
                            s_Instance = new Logger();
                        }
                    }
                }

                return s_Instance;
            }
        }


    }
}
