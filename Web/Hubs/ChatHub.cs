using Data.Models.Entities.Humans;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Hubs
{
    public class ChatHub : Hub
    {
        //TODO
        //Provide chat room abstraction. 
        //List of players inside, mods, kicks and bans, password
        //Put in data project, these have to exist for all consuming interfaces
        //Replace int room with ChatRoom room

        //TODO
        //Add list of default chatrooms
        //Add rooms that automatically join when doing other things, like joining a group or entering town.

        public ChatHub(GameWrapper wrapper)
        {

        }

        public void Join(int room)
        {
            //Scene / Instance has a chatroom
            //Entire Map (part of World)
            // World also has chatrooms.
        }

        public void Say(string message, int room)
        {

        }

        public void Yell(string message)
        {

        }

        public void Whisper(string message, Player player)
        {

        }

        public void Ignore(Player player)
        {

        }

        public void Unignore(Player player)
        {

        } 

        public void Kick(Player player, int room)
        {
            //Check that kicking player has mod
        }

        public void Create(string roomName, string password)
        {

        }

        public void Invite(Player player, int room)
        {

        }

        public void Environment(Player player, string message)
        {
            //When game needs to prompt player as the result of an event resolving 
        }

        public void System(string message)
        {
            //Broadcast to all players
        }
    }
}
