using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    public class Explorer
    {
        //Github structure...
        //Main page -> gitHub.com
        //User name -> / + "user name"
        //Project name -> / + "project name"
        
        
        //What does this github explorer seek to do...
        //Ask for a user name to explore

        public string userName = "";

        public void Start()
        {
            SelectUserFromInput();
        }

        #region User Related
        public void SelectUserFromInput()
        {
            Console.WriteLine("Select user");
            while (true)
            {
                var input = Console.ReadLine();

                if (AttemptUserConnection(input)) break;
            }
        }
        
        public bool AttemptUserConnection(string userName)
        {
            //Checks if the user is a viable pick
            //If the user is viable -> Assign user as the new user and return true
            //Else -> return false & throw error message
            return false;
        }
        #endregion
    }
}