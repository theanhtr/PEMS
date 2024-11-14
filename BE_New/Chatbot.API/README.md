# facebook-messenger-Ecommerce-chat-bot
An online store chatbot uses Facebook Messenger Platform, building from scratch with Node.js Platform.

## Demo the bot:
- Feel free to test my bot👉:  https://m.me/techShopHaryphamdev
- The Facebook Page I embed that bot: https://www.facebook.com/techShopHaryphamdev

## What can this bot do:
- Showing categories
- Looking up order (with a custom web view from node.js server)
- Talking with "real" live agent (if the user request)
- Turn on/off the bot
- Restart the conversation.

### How to setup this bot for your own Facebook Page without any cost ?

#### 1. Clone this project
#### 2. Create a Heroku app
#### 3. Deploy this project to your Heroku app
#### 4. Create a Facebook Developer App, A Facebook Page (to embed this bot)
#### 5. Going to Facebook Developer App, add the Messenger Product, generate FACEBOOK_PAGE_ACCESS_TOKEN, config the webhook (default, the url for the webhook is: <the_domain_your_herokuapp>/webhook ) . 
#### Remember to update the config variables on Heroku as well.
#### 6. Enable the option: Build-in NLP on Messenger Product (Natural Laguage Processing) to make the bot understand the sentences with the meaning "greetings", "thanks" and "bye".
#### 7. Enjoy!

### Several errors you may encouter:
- Forgetting to update the environment variables on Heroku : go to "Settings option", then "Reveal Congfig Var"
- The bot only works with the admin account, doesn't reply the others Facebook accounts: need to be approved the "pages_messaging" permission. 
For detail, watch this video: https://youtu.be/0VRQRHnrGxg

### env varialbe config in machine
- /pems_env/.env