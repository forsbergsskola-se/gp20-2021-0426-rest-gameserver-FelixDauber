using System;
using System.Collections.Generic;
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
        //Get suggestions for repositories
        //Pick repository with 1 -> ....

        public Stack<string> lastSites = new Stack<string>();

        private string currentSite;
        
        public string Site
        {
            get
            {
                return currentSite;
            }
            set
            {
                lastSites.Push(currentSite);
                currentSite = value;
                //Load site
            }
        }
        
        public string[] SiteExtensions
        {
            get
            {
                return currentSite.Split("/"); //Need to cut off the HTTP part ahead of this...
            }
        }

        public void GoBackToLastSite()
        {
            currentSite = lastSites.Pop();
            //Load site
        }

        private const string githubSiteName = "https://github.com";
        private string userName = "";

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