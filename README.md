<p align="center">
    <img align="center" src="https://i.ibb.co/3cwrNSJ/Fortnite-Dot-Net.png" alt="Background">
</p>
<p align="center">FortniteDotNet is a simple and easy-to-use library used for interacting with Fortnite's HTTP and XMPP services. Features include interactions with parties and friends, general API data, and more.</p>
<p align="center">
    <a href="https://github.com/DarkbladeEU/FortniteDotNet/stargazers">
    	<img alt="Stars" src="https://img.shields.io/github/stars/DarkbladeEU/FortniteDotNet" >
    </a>
    <a href="https://github.com/DarkbladeEU/FortniteDotNet/issues">
    	<img alt="Issues" src="https://img.shields.io/github/stars/DarkbladeEU/FortniteDotNet" >
    </a>
    <a href="https://github.com/DarkbladeEU/FortniteDotNet/blob/master/LICENSE">
    	<img alt="License" src="https://img.shields.io/github/license/DarkbladeEU/FortniteDotNet" >
    </a>
</p>

## Example
The below code demonstrates how you could use the XMPP feature of FortniteDotNet to create a lobby bot that updates the client upon commands sent in party chat.
I've commented the example code so you can have some sort of understanding of what it's doing. Please also note that this is just an example, and there's plenty more that you can do with FortniteDotNet, however I won't be going into full depths until I create documentation for it.

```cs
internal class Program 
{
    private static FortniteApi _api;

    // We use fields so we can access these in the Xmpp method.
    private static XMPPClient _xmppClient;
    private static OAuthSession _authSession;

    private static void Main() 
    {
        _api = new FortniteApi();
        Task.Run(async () => 
        {
            // Creates an OAuth session for the iOS client using the device_auth grant type.
            _authSession = await _api.AccountService.GenerateOAuthSession(GrantType.DeviceAuth, AuthClient.iOS, new() 
            {
                {"device_id", ""}, 
	        {"account_id", ""}
	        {"secret", ""}
            });

            // Starts XMPP-related operations on a new thread.
            new Thread(Xmpp).Start();

            // Creates a party for the XMPP client using the OAuth session we generated earlier.
            await _authSession.InitParty(_xmppClient);
            
            // Updates the XMPP client's current party privacy to public using the OAuth session we generated earlier.
            await _xmppClient.CurrentParty.UpdatePrivacy(_authSession, new PartyPrivacy(Privacy.Public));

        }).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    private static void Xmpp() 
    {
        // Sets up the XMPP client.
        _xmppClient = new XMPPClient(_authSession);
        
        // We're using the OnGroupChatReceived event handler to listen for any messages sent in the chat of the client's current party.
        _xmppClient.OnGroupChatReceived += async (_, chat) => 
        {
            // If the body doesn't start with !, return.
            if (!chat.Body.StartsWith("!"))
                return;

            // Get the arguments by removing the ! and splitting the body by a space.
            var args = chat.Body.Remove(0, 1).Split(" ");
	    
            // Get the command (the first argument)
            var command = args.FirstOrDefault();
	    
            // Get the content (every string after the argument)
            var content = string.Join(" ", args.Skip(1));
            
            // Get the client from the list of its current party's members.
            var me = _xmppClient.CurrentParty.Members.FirstOrDefault(x => x.Id == _authSession.AccountId);
            
            // Use a switch case on the command (this is better practice than if, else etc.)
            switch (command) 
            {
                // If the command is emote...
                case "emote": 
                {
                    // Set the client's emote to the content variable. Example: "!emote floss" would set the client's emote to Floss.
                    await me.SetEmote(_xmppClient, content);
                    break;
                }
                case "outfit": 
                {
                    // If the content contains :, this implies the user wants to apply a variant.
                    if (content.Contains(":")) 
                    {
                        // The SetOutfit method has an optional parameter, which we're using here. Example: "!outfit Skull Trooper:Purple Glow" would set the client's outfit to Skull Trooper, and the outfit's active variant to the Purple Glow variant.
                        await me.SetOutfit(_xmppClient, content.Split(":")[0], content.Split(":")[1]);
                        break;
                    }
    
                    // Otherwise, just set the outfit without a variant.
                    await me.SetOutfit(_xmppClient, content);
                    break;
                }
                case "banner": 
                {
                    // Set the client's banner to the content variables. Example: "!banner BRSeason01:DefaultColor02" would set the client's banner icon to the Battle Bus banner, and the client's banner color to red.
                    await me.SetBanner(_xmppClient, content.Split(":")[0], content.Split(":")[1]);
                    break;
                }
            }
        };

        // Initialize the XMPP client. This method connects us to Epic Games' XMPP services and starts listening for messages.
        _xmppClient.Initialize().GetAwaiter().GetResult();
    }
}
```

## Contact
For any queries regarding FortniteDotNet, you can reach out to me on the following platforms:

Email: <a href="mailto:me@darkblade.dev">me@darkblade.dev</a>

Twitter: <a href="https://twitter.com/DarkbladeEU">@DarkbladeEU</a>

Or, you can open an issue and I will happily reply to the thread.
