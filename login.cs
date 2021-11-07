using System;

namespace CSHARP
{
  
  static class login
  {
      //PROPERTIES
      static public bool Player1LoggedIn
      {
          get; set;
      }
      static public bool Player2LoggedIn
      {
          get; set;
      }

      //FIELDS
      static string[] usernames = new string[] { "U1", "U2", "U3" };
      static string[] passwords = new string[] { "P1", "P2", "P3" };

      //METHODS
      static public bool auth(string enteredUsername, string enteredPassword)
      {
          int usernamepos = Array.IndexOf(usernames,enteredUsername);
          if (usernamepos==-1)
          {
              return false;
          }
          else
          {
              if (enteredPassword==passwords[usernamepos])
              {
                  return true;
              }
              else
              {
                  return false;
              }
          }
      }

  }
}