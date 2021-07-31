/* ==============================================================================
 * 功能描述：提取年、月、日、週各2碼
 * 編 寫 者：Hugo
 * 編寫日期：2021/5/5
 * ==============================================================================*/

using System;
using System.Globalization;

namespace Attendance
{
    class GetDateTime
    {   //attribute setting
        private string yy;
        private string mm;
        private string dd;
        private string ww;
        string datetime;

        public GetDateTime()
        {   //constructor
            datetime = DateTime.Now.ToString("yyyy/MM/dd");   //取得當天日期
            string[] arr = datetime.Split('/');               //字串分裂並以虛擬陣列儲存

            yy = arr[0];                //取得年份
            mm = arr[1];                //取得月2碼
            dd = arr[2];                //取得日2碼
            ww = GetWeek(datetime);     //依當天日期取得週別
        }

        public string Right(string str, int length)
        {   //method
            string result = str.Substring(str.Length - length, length);
            return result;
        }

        private string GetWeek(string datetime)
        {   //method
            string week = new TaiwanCalendar().GetWeekOfYear(Convert.ToDateTime(datetime),
                    System.Globalization.CalendarWeekRule.FirstDay, System.DayOfWeek.Sunday).ToString();

            if (week.Length == 1)
                week = "0" + week;   //週別若不足2碼, 第1碼補0

            return week;
        }

        public string getYear() { return yy; }

        public string getWeek() { return ww; }

        public string getMonth() { return mm; }

        public string getDay() { return dd; }
    }
}
