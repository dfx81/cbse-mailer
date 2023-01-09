namespace MailScheduler;

using System;
using System.Collections.Generic;

public class Scheduler
{
    public static List<List<string>> CreateMailList(string[][] data)
    {
        List<List<string>> mailList = new List<List<string>>();

        foreach (string[] item in data)
        {
            bool exists = false;

            foreach (List<string> mail in mailList)
            {
                if (mail[0].Equals(item[0]))
                {
                    if (Scheduler.InDueRange(item[2], 5))
                    {
                        mail.Add(item[1] + ";" + item[2]);
                    }

                    exists = true;
                    break;
                }
            }

            if (exists || !Scheduler.InDueRange(item[2], 5))
            {
                continue;
            }

            List<string> mailTarget = new List<string>();

            mailTarget.Add(item[0]);
            mailTarget.Add(item[1] + ";" + item[2]);

            mailList.Add(mailTarget);
        }

        return mailList;
    }

    private static bool InDueRange(string rawDateString, int range)
    {
        string[] date = rawDateString.Split(",");

        DateTime today = DateTime.Now;
        DateTime due = new DateTime(
            Int32.Parse(date[0]),
            Int32.Parse(date[1]),
            Int32.Parse(date[2])
        );

        int diff = (due - today).Days;

        if (diff <= range)
        {
            return true;
        }

        return false;
    }
}
