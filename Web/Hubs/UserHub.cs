using Microsoft.AspNetCore.SignalR;
using Web.Models;

namespace Web.Hubs
{
    public class UserHub : Hub
    {
        //Todo: Investigate securestring by pumping chars through websockets directly into securestring
        public void Login(Credentials credentials)
        {
           //Make async call to some UserRepo
           //var user = await userRepository.GetUser(credentials);
           //var result = await signinManager.SignIn(user);
           //Clients.Client(Context.ConnectionId).InvokeAsync("Login", result);
           //Client should now know to redirect using HttpGet if it wants to. We don't decide, we are not it's dad.

        }

        //Todo: What do we provide here? Should be same object as ordinary .net auth session
        public void Logout(object session)
        {
            //Make async call to some service
            //var result = await signinManager.SignOut(session);
            //Clients.Client(Context.ConnectionId).InvokeAsync("Logout", result);
            //Client should now know to redirect using HttpGet if it wants to. We don't decide, we are not it's dad.
        }
    }
}
