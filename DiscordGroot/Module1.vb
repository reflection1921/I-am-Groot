Imports Discord
Imports Discord.WebSocket
Imports Discord.Commands
Imports Newtonsoft.Json.Linq

Module Module1

    Private WithEvents _client As DiscordSocketClient
    Private token As String
    Private channel_name As String

    Sub Main()
        Dim jObj As JObject = JObject.Parse(IO.File.ReadAllText("settings.json"))
        token = jObj("token").ToString
        channel_name = jObj("channel").ToString

        StartAsync.GetAwaiter.GetResult()
    End Sub

    Public Async Function StartAsync() As Task
        _client = New DiscordSocketClient()
        Await _client.LoginAsync(TokenType.Bot, token)
        Await _client.StartAsync()
        Await Task.Delay(-1)
    End Function

    Private Function LogAsync(ByVal log As LogMessage) As Task Handles _client.Log
        Console.WriteLine(log.ToString())
        Return Task.CompletedTask
    End Function

    Private Async Function MessageRecievedAsync(s As SocketMessage) As Task Handles _client.MessageReceived
        If s Is Nothing Then Return
        If s.Author.IsBot Then Return
        Dim context = New SocketCommandContext(_client, s)

        If s.Channel.Name = channel_name Then
            Await context.Channel.SendMessageAsync("I am Groot.")
        End If

    End Function

End Module
