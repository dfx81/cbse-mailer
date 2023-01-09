using System;
using System.Collections.Generic;
using MailUtils;
using MailScheduler;

class Program
{
    public static void Main(string[] args)
    {
        string smtpAddress = "smtp.ethereal.email";
        int smtpPort = 587;
        string email = "";
        string pass = "";

        if (args.Length < 2)
        {
            Console.WriteLine(
                "ERROR -1: Insufficient Arguments.\n\n" +
                "USAGE:\n" +
                "$ dotnet run --project Test/Test.csproj -- <email address> <email password>\n" +
                "$ dotnet run --project Test/Test.csproj -- <smtp address> <smtp port> <email address> <email password>"
            );

            Environment.Exit(-1);
        } else if (args.Length == 2)
        {
            email = args[0];
            pass = args[1];
        } else if (args.Length == 4)
        {
            smtpAddress = args[0];
            smtpPort = Int32.Parse(args[1]);
            email = args[2];
            pass = args[3];
        }

        // On 10/1/23:
        // Student 1 - 1 due
        // Student 2 - 2 due
        // Student 3 - 0 due
        // Student 4 - 1 due
        // Student 5 - 3 due
        string[][] testData = new string[][]
        {
            new string[] {"student1@example.com", "CBD", "2023, 1, 14"},
            new string[] {"student2@example.com", "KOK4", "2023, 1, 12"},
            new string[] {"student2@example.com", "CBD", "2023, 1, 14"},
            new string[] {"student1@example.com", "SE", "2023, 1, 22"},
            new string[] {"student3@example.com", "SE", "2023, 1, 22"},
            new string[] {"student4@example.com", "CBD", "2023, 1, 14"},
            new string[] {"student4@example.com", "SE", "2023, 1, 22"},
            new string[] {"student5@example.com", "CBD", "2023, 1, 14"},
            new string[] {"student5@example.com", "KOK2", "2023, 1, 12"},
            new string[] {"student5@example.com", "WEB", "2023, 1, 12"},
        };

        Mailer mailer = new Mailer(
            smtpAddress,
            smtpPort,
            email,
            pass
        );

        foreach (List<string> mailItem in Scheduler.CreateMailList(testData))
        {
            Console.WriteLine("{0} has {1} submissions due.", mailItem[0], mailItem.Count - 1);
            
            string message = "<p>You have <b>" + (mailItem.Count - 1) +
                "</b> submission/s due in the next <b>FIVE</b> days:</p><ul>";
            
            for (int i = 1; i < mailItem.Count; i++)
            {
                string[] metadata = mailItem[i].Split(";");
                message += "<li>" + metadata[0] + " due on " + Program.FormatDate(metadata[1]) + "</li>";
            }

            message += "</ul>";

            mailer.SendMail(
                "learning@uum.edu.my",
                mailItem[0],
                "Submissions Due",
                message
            );
        }
    }

    private static string FormatDate(string rawDateString)
    {
        string[] splitDate = rawDateString.Split(",");

        for (int i = 0; i < splitDate.Length; i++)
        {
            splitDate[i] = splitDate[i].Trim();
        }

        return splitDate[0] + "/" + splitDate[1] + "/" + splitDate[2];
    }
}