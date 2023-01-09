# cbse-mailer

A simple mailer created for component-based development assignment. Created using .NET

## Components

1. ```Mailer.dll``` - Provides methods to send simple emails under namespace ```MailUtils```
2. ```MailScheduler.dll``` - Provides method to create a mailing list based on given data under namespace ```MailScheduler```

## Mailer.dll Interfaces:

### Constructor

```
public Mailer(string host, int port, string user, string pass)

- host : string = The address of the SMTP server
- port : int = The port used by the SMTP server
- user : string = The email address used to access the SMTP server
- pass : string = The password for the email used to access the SMTP server
```

### Methods

```
public void SendMail(string from, string to, string subject, string body)

- from : string = The sender's email address
- to : string = The recipient's email address
- subject : string = The subject of the email
- body : string = The body of the email
```

## MailScheduler.dll Interfaces:

### Methods

```
public static List<List<string>> CreateMailList(string[][] data)

- data: string[][] = The data to be used to construct the mailing list

RETURNS:
- A list of list of string containing the list of mails to be sent
```

### Data Format

The scheduler accepts data with the following formatting:

```
[
    ["target email address", "subject", "raw date string"],
    [...],
    [...],
] : string[][]
```

The raw date string used are in the following format:

```
"yyyy, mm, dd" : string
```

The mailing list returned from the ```CreateMailingList()``` method is in the following format:

```
[
    ["target email address", "raw metadata 1", ..., ...],
    [...],
    [...],
] : List<List<string>>

* Subsequent elements after the target's email address is always additional mail metadata
```

The raw metadata string used are in the following format:

```
"subject;yyyy, mm, dd" : string
```

## Running the Sample Program:

```
USAGE:
$ dotnet run --project Test/Test.csproj -- <email address> <email password>
$ dotnet run --project Test/Test.csproj -- <smtp address> <smtp port> <email address> <email password>
```