<p align="center">
	<!--<img align="center" src="https://cdn.discordapp.com/attachments/838613584970776608/839187334515130408/neonitev2.png" alt="Logo" width="216" height="127">-->
</p>
<p align="center">FortniteDotNet is a simple and easy-to-use library used for interacting with Fortnite's HTTP and XMPP services. Features include interactions with parties and friends, general API data, and more.</p>

## Example
The below code demonstrates how you could use the XMPP feature of FortniteDotNet to create a lobby bot that updates the client upon commands sent in party chat.

```cs
internal class Program
{
	private static FortniteApi _api;
	
	private static XMPPClient _xmppClient;
	private static OAuthSession _authSession;
	
	private static void Main()
	{
		_api = new FortniteApi();
		Task.Run(async () =>
		{
			_authSession = await _api.AccountService.GenerateOAuthSession(GrantType.DeviceAuth, AuthClient.iOS, new()
			{
				{ "device_id", "" },
				{ "account_id", "" },
				{ "secret", "" }
			});
			
			new Thread(Xmpp).Start();
		
			await _authSession.InitParty(_xmppClient);
			await _xmppClient.CurrentParty.UpdatePrivacy(_authSession, new PartyPrivacy(Privacy.Public));

		}).ConfigureAwait(false).GetAwaiter().GetResult();
	}

	private static void Xmpp()
	{
		_xmppClient = new XMPPClient(_authSession);
		_xmppClient.OnGroupChatReceived += async (_, chat) =>
		{
			if (!chat.Body.StartsWith("!")) 
				return;

			var args = chat.Body.Remove(0, 1).Split(" ");
			var command = args.FirstOrDefault();
			var content = string.Join(" ", args.Skip(1));

			switch (command)
			{
				case "emote":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					await me.SetEmote(_xmppClient, content);
					break;
				}
				case "outfit":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					if (content.Contains(":"))
					{
						await me.SetOutfit(_xmppClient, content.Split(":")[0], content.Split(":")[1]);
						break;
					}

					await me.SetOutfit(_xmppClient, content);
					break;
				}
				case "backbling":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					if (content.Contains(":"))
					{
						await me.SetBackpack(_xmppClient, content.Split(":")[0], content.Split(":")[1]);
						break;
					}

					await me.SetBackpack(_xmppClient, content);
					break;
				}
				case "pickaxe":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					if (content.Contains(":"))
					{
						await me.SetPickaxe(_xmppClient, content.Split(":")[0], content.Split(":")[1]);
						break;
					}

					await me.SetPickaxe(_xmppClient, content);
					break;
				}
				case "banner":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					await me.SetBanner(_xmppClient, content.Split(":")[0], content.Split(":")[1]);
					break;
				}
				case "emoji":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					await me.SetEmoji(_xmppClient, content);
					break;
				}
				case "level":
				{
					var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
					await me.SetLevel(_xmppClient, Convert.ToInt32(content));
					break;
				}
			}
		};
		
		_xmppClient.Initialize().GetAwaiter().GetResult();
	}
}
```



## Contact
For any queries regarding FortniteDotNet, you can reach out to me on the following platforms:

Email: <a href="mailto:me@darkblade.dev">me@darkblade.dev</a>

Twitter: <a href="https://twitter.com/DarkbladeEU">@DarkbladeEU</a>

Or, you can open an issue and I will happily reply to the thread.