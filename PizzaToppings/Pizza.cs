using System;
using System.Windows;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PizzaToppings
{
    /// <summary>
    /// Class used to deserialize JSON toppings
    /// </summary>
    public class Toppings
    {
        public List<String> toppings;
    }

    public static class Pizza
    {
        public static Dictionary<String, String> Toppings { get; }

        public static Dictionary<String, String> GetToppings()
        {
        #region Variables
            // Create a Dictionary to store the topping and count
            Dictionary<String, String> toppingDictionary = new Dictionary<string, string>();

            // Create a list to transfer all of the toppings from each pizza into one list
            List<String> toppingList = new List<string>();

            WebClient jsonClient = new WebClient();
        #endregion

            try
            {
            #region Deserialize
                // Get JSON File using the "JsonSource" key in the App.config file
                String ingredientsJson = jsonClient.DownloadString(ConfigurationSettings.AppSettings["JsonSource"]);

                // Deserialize
                var serializer = new JavaScriptSerializer();
                var pizzas = serializer.Deserialize<List<Toppings>>(ingredientsJson);
                #endregion

                #region Sort And Count Toppings
                // Create a single list of toppings
                //pizzas.ForEach(x => x.toppings.ForEach(y => toppingList.Add(y)));
                
                pizzas.ForEach(x => toppingList.Add(String.Join(", ", x.toppings.ToArray())));

                // Sort and count
                toppingDictionary = (from tempTableA in toppingList
                                     group tempTableA by tempTableA into tempTableB
                                     select new
                                     {
                                         Topping = tempTableB.Key,
                                         Count = tempTableB.Count()
                                     } into tempTableA
                                     orderby tempTableA.Count descending, tempTableA.Topping
                                     select tempTableA).ToDictionary(x => x.Topping.ToString(), x => x.Count.ToString());
            #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    @"An error occured while trying to deserialize the json file from:" +
                    $"\r\n{ConfigurationSettings.AppSettings["JsonSource"]}" +
                    $"\r\nERROR: {e.Message} ");
                throw;
            }

            return toppingDictionary;

        }
    }
}
